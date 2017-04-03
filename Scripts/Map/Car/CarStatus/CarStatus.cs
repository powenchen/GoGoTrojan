using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviour
{
    //basic status
    public float currHP = 100;
    public float maxHPBase = 100;
    public float maxHPModifier = 1;

    public float currMP = 100;
    public float maxMPBase = 100;
    public float maxMPModifier = 1;

    public float attackBase = 100;
    public float attackModifier = 1;

    public float topSpeedBase = 100 * 1000 / 3600;//(100 km/h)
    public float topSpeedModifier = 1;
    private const float INITIAL_SPEED_TIME_MODIFIER = 0.2f;
    public float topSpeedTimeModifier = INITIAL_SPEED_TIME_MODIFIER;
    private float accelerationRatio = 0.1f; // get to full speed in 1/(accelerationRatio) seconds; TODO - make it public?

    public float defenseBase = 100;
    public float defenseModifier = 1;

    public float skillCDBase = 10; // how many seconds to fully charge MP
    public float skillCDModifier = 1;
    
    // negative effects
    public bool isPoisoned = false;
    public float poisonTime = 5.0f; // TODO - fixed ??
    public float poisonTimer = 0.0f;
    public float poisonDPS = 0.0f;
    private CarStatus poisonAttacker;

    public bool isStunned = false;
    private float stunningTime = 1.0f;
    private float stunnedTimer = 0.0f;

    private float speedDebuffTime = 2;
    private bool isSpeedDebuffing = false;
    private float speedDebuffTimer = 0;

    //card abilities
    public float hpRecover = 0; // recover how many points per second
    public float mpRecover = 0; // recover how many points per second

    public float damageReduction = 0; // modifier of damage received
    public float armorPenetration = 0;
    public float criticalChance = 0.0f;
    private float criticalDamage = 0.5f;//TODO - fixed?
    public bool reflectAbility; // reflect poison, stun and 50% damage
    public int reviveAbility = 0; // how many times it could revive(full HP, MP, remove all debuffs)

    public float poisonAbility = 0;
    public float poisonAbilityTime = 5;// TODO - fixed ??

    public float lifeStealAbility = 0;

    public bool stunAbility;

    public float globalAttackChance = 0;

    public float receivedExpModifier = 1;
    public float receivedCoinModifier = 1;


    // Particles and prefabs
    public ParticleSystem reviveAnime;
    public ParticleSystem explosionOnDead;
    public GameObject stunAnime;
    public bool debug = false;


    private void Update()
    {
        topSpeedTimeModifier = Mathf.Clamp(topSpeedTimeModifier + accelerationRatio * Time.deltaTime, INITIAL_SPEED_TIME_MODIFIER, 1);
        if (debug)
        {
            Quaternion spwanRot = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(90, 0, 0));
            Instantiate(reviveAnime, transform.position, spwanRot, transform);
            debug = false;
        }
        if (isStunned)
        {
            if (GetComponentInChildren<StunningStar>() == null)
            {
                Instantiate(stunAnime, transform.position + new Vector3(0, 1.5f, 0), transform.rotation, transform);
            }
            stunnedTimer += Time.deltaTime;
            if (stunnedTimer > stunningTime)
            {
                isStunned = false;
                stunnedTimer = 0;
                GetComponent<Car>().stunned = false;
            }
            else
            {
                GetComponent<Car>().stunned = true;
            }
        }

        if (isPoisoned)
        {
            poisonTimer += Time.deltaTime;
            if (poisonTimer > poisonTime)
            {
                isPoisoned = false;
                poisonTimer = poisonTime = poisonDPS = 0;
                poisonAttacker = null;
            }
            //poison has no critical chance and cannot trigger other special effect(stun, poison and reflect)
            float damage = poisonDPS * Time.deltaTime;
            decreaseHP(damage, poisonAttacker);

        }

        


        Car car = GetComponent<Car>();
        if (car && !car.stopFlag && !car.stoppedBySkill)
        {
            increaseMP(getMaxMP() *  (Time.deltaTime  / getSkillCD())); // TODO - Modify this
        }

        if (isSpeedDebuffing)
        {
            speedDebuffTimer += Time.deltaTime;
            if (speedDebuffTimer >= speedDebuffTime)
            {
                removeSpeedDebuff();
            }
        }

        increaseHP(hpRecover * Time.deltaTime);
        increaseMP(mpRecover * Time.deltaTime);
    }

    public void decreaseTopSpeedModifier(float ratio)
    {
        //called when turn or collision
        //Debug.Log("decreaseTopSpeedModifier is called");
        topSpeedTimeModifier = Mathf.Clamp(topSpeedTimeModifier - ratio * accelerationRatio * Time.deltaTime, INITIAL_SPEED_TIME_MODIFIER, 1);

    }

    public void HPInitialize(float baseHP, float hpModifier = 1)
    {
        maxHPBase = baseHP;
        currHP = baseHP * hpModifier;
        maxHPModifier = hpModifier;
    }

    public float getMaxHP()
    {
        return maxHPBase * maxHPModifier;
    }

    public float getHP()
    {
        return currHP;
    }

    public void decreaseHP(float point, CarStatus attacker = null)
    {
        float decreasedAmount = Mathf.Min(point * Mathf.Max(0,1-damageReduction), currHP);

        // it is possible being attack by yourself if your poison ability is reflected
        if (attacker != null && attacker != this && attacker.lifeStealAbility > 0)
        {
            float lsAmount = decreasedAmount * attacker.lifeStealAbility;
            if (lsAmount > 0)
            {
                attacker.increaseHP(lsAmount);
            }
        }
        currHP = Mathf.Clamp(currHP - decreasedAmount, 0, currHP);
        // destroy  car
        if (currHP <= 0)
        {
            if (reviveAbility > 0)
            {
                --reviveAbility;
                currHP = getMaxHP();
                currMP = getMaxMP();
                poisonTimer = poisonTime + 1;
                stunnedTimer = stunningTime + 1;
                Quaternion spwanRot = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(90, 0, 0));
                Instantiate(reviveAnime, transform.position, spwanRot, transform);
            }
            else
            {
                Instantiate(explosionOnDead, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    public void increaseHP(float point)
    {
        currHP = Mathf.Clamp(currHP + point, currHP, getMaxHP());
    }

    public void MPInitialize(float baseMP, float mpModifier = 1)
    {
        maxMPBase = baseMP;
        currMP = 0;
        maxMPModifier = mpModifier;
    }

    public float getMaxMP()
    {
        return maxMPBase * maxMPModifier;
    }

    public float getMP()
    {
        return currMP;
    }

    public void decreaseMP(float point)
    {
        currMP = Mathf.Clamp(currMP - point, 0, currMP);
    }

    public void increaseMP(float point)
    {
        currMP = Mathf.Clamp(currMP + point, currMP, getMaxMP());
    }
    public void attackInitialize(float baseAtk, float modifier = 1)
    {
        attackBase = baseAtk;
        attackModifier = modifier;
    }

    public float getAttackPower()
    {
        return attackBase * attackModifier;
    }

    public void decreaseAttackPower(float point)
    {
        attackBase = Mathf.Max(attackBase - point, 0);
    }

    public void increaseAttackPower(float point)
    {
        attackBase = Mathf.Max(attackBase + point, 0);
    }

    public void skillCDInitialize(float basePoint, float modifier = 1)
    {
        skillCDBase = basePoint;
        skillCDModifier = modifier;
    }

    public float getSkillCD()
    {
        return skillCDBase * skillCDModifier;
    }

    public void decreaseSkillCD(float point)
    {
        skillCDBase = Mathf.Max(skillCDBase - point, 0.1f);
    }

    public void increaseSkillCD(float point)
    {
        skillCDBase += point;
    }

    public void setTopSpeed(float baseSpeed, float modifier = 1)
    {
        topSpeedBase = baseSpeed;
        topSpeedModifier = modifier;
    }

    public float getSpeed()
    {
        return topSpeedBase * topSpeedModifier * topSpeedTimeModifier;
    }

    public void speedDebuff()
    {
        topSpeedModifier = 0.5f;
        isSpeedDebuffing = true;
        speedDebuffTimer = 0;
    }

    public void removeSpeedDebuff()
    {
        topSpeedModifier = 1;
        isSpeedDebuffing = false;
        speedDebuffTimer = 0;
    }

    public float getAttack()
    {
        return attackBase * attackModifier;
    }

    public float getDefense()
    {
        return defenseBase * defenseModifier;
    }

    public float damageValue(CarStatus attacker, CarStatus defenser, float weaponModifier)
    {
        float atkFactor = attacker.getAttack() * weaponModifier;
        float defFactor = 1 + 0.01f * (Mathf.Max(defenser.getDefense() - attacker.armorPenetration, 0));

        bool isCritical = (Random.value < criticalChance);

        if (isCritical)
        {
            atkFactor *= (1 + criticalDamage);//CRITICAL
        }

        return atkFactor / defFactor;
    }

    public void isAttackedBy(CarStatus attacker, float weaponModifier = 1)
    {
        // check attacker abilities(poison, liseSteal, stun)
        // check self ability(reflect == true)
        float reflectedDamage = 0;//give attacker damage(if has reflect ability)
        float receivedDamage = damageValue(attacker, this, weaponModifier);
        //Debug.Log("receivedDamage = " + receivedDamage);

        CarStatus negativeEffectReciever = this;
        if (reflectAbility)
        {
            receivedDamage = receivedDamage / 2;
            reflectedDamage = receivedDamage;
            negativeEffectReciever = attacker;
        }

        if (attacker != null)
        {
            if (attacker.poisonAbility > 0)
            {
                negativeEffectReciever.isPoisoned = true;
                negativeEffectReciever.poisonTimer = 0;
                negativeEffectReciever.poisonAttacker = this;
                negativeEffectReciever.poisonDPS = attacker.poisonAbility;
            }
            if (attacker.stunAbility)
            {
                negativeEffectReciever.stunnedTimer = 0;
                negativeEffectReciever.isStunned = true;
                Transform spawnTrans = negativeEffectReciever.transform;
                GameObject stunObj = Instantiate(stunAnime, spawnTrans.position + new Vector3(0, 1.5f, 0), spawnTrans.rotation, spawnTrans);
                stunObj.GetComponent<StunningStar>().setLifetime(negativeEffectReciever.stunningTime);
            }

            if (reflectedDamage > 0)
            {
                //reflecting damage has no critical chance and cannot trigger other special effect(stun, poison and reflect)
                attacker.decreaseHP(reflectedDamage);
            }
        }
        
        decreaseHP(receivedDamage);

    }
}
