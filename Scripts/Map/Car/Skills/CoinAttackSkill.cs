using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAttackSkill : Skill
{
    public GameObject coinBullet;

    // Use this for initialization
    void Start () {
        isSkillUsing = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void stopSkill()
    {
    }

    public override void activateSkill()
    {
        CarStatus attacker = GetComponent<CarStatus>();
        float itemPutOffset = 5;
        Vector3 spawnPosition = transform.position + 2 * transform.forward * itemPutOffset;
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        GameObject weapon = Instantiate(coinBullet, spawnPosition, spawnRotation, transform);
        weapon.GetComponent<TrapWeapons>().attacker = attacker;


        spawnPosition = transform.position + 2 * transform.forward * itemPutOffset;
        spawnRotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y+30, 0));
        weapon = Instantiate(coinBullet, spawnPosition, spawnRotation, transform);
        weapon.GetComponent<TrapWeapons>().attacker = attacker;


        spawnPosition = transform.position + 2 * transform.forward * itemPutOffset;
        spawnRotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y - 30, 0));
        weapon = Instantiate(coinBullet, spawnPosition, spawnRotation, transform);
        weapon.GetComponent<TrapWeapons>().attacker = attacker;

        if (skillAudio == null)
        {
            return;
        }
        // play once
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = skillAudio;
        source.volume = 1;
        source.loop = false;
        source.Play();
    }
}
