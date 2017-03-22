using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStatus : MonoBehaviour {

    public float topSpeed = 100 * 1000 / 3600;//(100 km/h)
    public float topSpeedModifier = 1;

    public float currHP = 100;
    public float maxHP = 100;
    public float maxHPModifier = 1;
    public float hpRecover = 0; // recover how many points per second
    public float damageModifier = 1; // modifier of damage received


    public float currMP = 100;
    public float maxMP = 100;
    public float maxMPModifier = 1;
    public float mpRecover = 0; // recover how many points per second

    public float attackPower = 100;
    public float attackModifier = 1;

    public float skillCD = 10; // how many seconds to fully charge MP
    public float skillCDModifier = 1;


    private float speedDebuffTime = 2;
    private bool isSpeedDebuffing = false;
    private float speedDebuffTimer = 0;

    public ParticleSystem explosionOnDead;
    public GameObject stunAnime;


    // negative effects
    public bool isPoisoned = false;
    public float poisonTime = 5.0f; // TODO - fixed ??
    public float poisonTimer = 0.0f;
    public float poisonDPS = 0.0f;
    private CarStatus poisonAttacker;

    private bool isStunned = false;
    private float stunningTime = 1.0f;
    private float stunnedTimer = 0.0f;


    //card abilities

    public bool reflectAbility; // reflect poison, stun and 50% damage
    public int reviveAbility = 0; // could revive(full HP, MP, remove all debuffs) 'reviveAbility' times
    public ParticleSystem reviveAnime;

    public float poisonAbility = 0;
    public float poisonAbilityTime = 5;// TODO - fixed ??

    public bool stunAbility;

    public float globalAttackChance = 0;
    public float receivedExpModifier = 1;
    public float receivedCoinModifier = 1;

    public float lifeStealAbility = 0;
    public bool debug = false;
    private void Update()
    {
        if (debug)
        {
            Quaternion spwanRot = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(90,0,0));
            Instantiate(reviveAnime, transform.position, spwanRot, transform);
            debug = false;
        }
        if (isStunned)
        {
            stunnedTimer += Time.deltaTime;
            if (stunnedTimer > stunningTime)
            {
                isStunned = false;
                stunnedTimer = 0;
                GetComponent<Car>().stopBySkill(false);
            }
            GetComponent<Car>().stopBySkill(true);
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

            float damage = poisonDPS * Time.deltaTime;
            
            isAttackedBy(poisonAttacker, damage, true);
            
        }

        // destroy  car
        if (getHP() == 0)
        {
            if (reviveAbility > 0)
            {
                --reviveAbility;
                currHP = maxHP;
                currMP = maxMP;
                poisonTimer = poisonTime +1;
                stunnedTimer = stunningTime+1;
                Quaternion spwanRot = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(90, 0, 0));
                Instantiate(reviveAnime, transform.position, spwanRot, transform);
            }
            else
            {
                Instantiate(explosionOnDead, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        

        Car car = GetComponent<Car>();
        if (!car.stopFlag && !car.stoppedBySkill)
        {
            increaseMP(getMaxMP() * Time.deltaTime * (1 / getSkillCD())); // TODO - Modify this
        }
        
        if (isSpeedDebuffing)
        {
            speedDebuffTimer += Time.deltaTime;
            if (speedDebuffTimer >= speedDebuffTime)
            {
                removeSpeedDebuff();
            }
        }
    }

    public void HPInitialize(float point)
    {
        maxHP = currHP = point;
    }

    public float getMaxHP()
    {
        return maxHP;
    }

    public float getHP()
    {
        return currHP;
    }

    public void decreaseHP(float point)
    {
        currHP = Mathf.Clamp(currHP - point*damageModifier, 0, currHP);
    }

    public void increaseHP(float point)
    {
        currHP = Mathf.Clamp(currHP + point, currHP, maxHP);
    }

    public void MPInitialize(float maxPoint, float point = 0)
    {
        maxMP = maxPoint;
        currMP = point;
    }

    public float getMaxMP()
    {
        return maxMP;
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
        currMP = Mathf.Clamp(currMP + point, currMP, maxMP);
    }
    public void attackInitialize(float point)
    {
        attackPower = point;
    }

    public float getAttackPower()
    {
        return attackPower;
    }

    public void decreaseAttackPower(float point)
    {
        attackPower = Mathf.Max(attackPower - point, 0);
    }

    public void increaseAttackPower(float point)
    {
        attackPower = Mathf.Max(attackPower + point, 0);
    }

    public void skillCDInitialize(float point)
    {
        skillCD = point;
    }

    public float getSkillCD()
    {
        return skillCD;
    }

    public void decreaseSkillCD(float point)
    {
        skillCD = Mathf.Max(skillCD - point, 0.1f);
    }

    public void increaseSkillCD(float point)
    {
        skillCD += point;
    }

    public void setTopSpeed(float speed)
    {
        topSpeed = speed;
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

    public void isAttackedBy(CarStatus attacker, float damage,bool isPoisonAttack = false)
    {
        // check attacker abilities(poison, liseSteal, stun)
        // check self ability(reflect == true)
        float giveDamage = 0;//give attacker damage(if has reflect or give negative damage if attacker has lifeStealing)
        float receivedDamage = damage;
        if (attacker != null)
        {
            if (reflectAbility)
            {
                giveDamage = receivedDamage = damage / 2;

                if (attacker.poisonAbility > 0)
                {
                    attacker.isPoisoned = true;
                    attacker.poisonTimer = 0;
                    attacker.poisonAttacker = this;
                    attacker.poisonDPS = attacker.poisonAbility;
                }
                if (attacker.stunAbility)
                {
                    attacker.stunnedTimer = 0;
                    attacker.isStunned = true;
                    Transform spawnTrans = attacker.transform;
                    GameObject stunObj = Instantiate(stunAnime, spawnTrans.position + new Vector3(0, 1.5f, 0), spawnTrans.rotation, spawnTrans);
                    stunObj.GetComponent<StunningStar>().setLifetime(attacker.stunningTime);
                }
            }
            else if (!isPoisonAttack)
            {
                //check negative effect
                if (attacker.poisonAbility > 0)
                {
                    isPoisoned = true;
                    poisonTimer = 0;
                    poisonAttacker = attacker;
                    poisonDPS = attacker.poisonAbility;
                }
                if (attacker.stunAbility)
                {
                    stunnedTimer = 0;
                    isStunned = true;
                    GameObject stunObj = Instantiate(stunAnime, transform.position + new Vector3(0, 1.5f, 0), transform.rotation, transform);
                    stunObj.GetComponent<StunningStar>().setLifetime(stunningTime);
                }
            }

            if (attacker.lifeStealAbility > 0)
            {
                giveDamage -= receivedDamage * attacker.lifeStealAbility;
            }

            if (giveDamage > 0)
            {
                attacker.decreaseHP(giveDamage);
            }
            else if (giveDamage < 0)
            {
                attacker.increaseHP(-1 * giveDamage);
            }
        }

        decreaseHP(receivedDamage);


    }
}
