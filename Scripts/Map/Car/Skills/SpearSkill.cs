using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSkill : Skill {

    public GameObject[] spears;
    // Use this for initialization
    GameObject weaponInstance;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        isSkillUsing = (weaponInstance != null);
    }

    public override void stopSkill()
    {
        isSkillUsing = false;
    }

    public override void activateSkill()
    {
        float posRandom = 3;
        float angleRandom = 40;
        CarStatus attacker = GetComponent<CarStatus>();
        int myDist = FindObjectOfType<RankingSystem>().GetCarDist(GetComponent<Car>());
        for (int i = 0; i < 50; i+=5)
        {
            CarCheckPoint respawnPoint = findCheckPoint(myDist + i);
            if (respawnPoint == null)
            {
                return;
            }
            Vector3 spawnPos = new Vector3(
                respawnPoint.transform.position.x,
                respawnPoint.transform.position.y,
                respawnPoint.transform.position.z
                ) ;
            Vector3 posCenter = spawnPos+ new Vector3(posRandom * Random.value - 0.5f * posRandom, 0, posRandom * Random.value - 0.5f * posRandom);
            Quaternion spawnRotation = Quaternion.Euler(new Vector3(angleRandom * Random.value-0.5f* angleRandom, 0, angleRandom * Random.value - 0.5f * angleRandom));
            //Quaternion spawnRotation = Quaternion.Euler(new Vector3(180 + angleRandom * Random.value - 0.5f * angleRandom, 0, angleRandom * Random.value - 0.5f * angleRandom));
            int spearIdx = Random.Range(0, spears.Length);
            weaponInstance = Instantiate(spears[spearIdx], posCenter + new Vector3(0,-10,0), spawnRotation);
            weaponInstance.GetComponent<TrapWeapons>().attacker = attacker;
            weaponInstance.GetComponent<SpearTrap>().SetTarget(posCenter);

            Vector3 posLeft = spawnPos - respawnPoint.transform.right * 5f + new Vector3(posRandom * Random.value - 0.5f * posRandom, 0, posRandom * Random.value - 0.5f * posRandom);
            spawnRotation = Quaternion.Euler(new Vector3(angleRandom * Random.value - 0.5f * angleRandom, 0, angleRandom * Random.value - 0.5f * angleRandom));
            spearIdx = Random.Range(0, spears.Length);
            weaponInstance = Instantiate(spears[spearIdx], posLeft + new Vector3(0, -10, 0), spawnRotation);
            weaponInstance.GetComponent<TrapWeapons>().attacker = attacker;
            weaponInstance.GetComponent<SpearTrap>().SetTarget(posLeft);

            Vector3 posRight = spawnPos + respawnPoint.transform.right * 5f + new Vector3(posRandom * Random.value - 0.5f * posRandom, 0, posRandom * Random.value - 0.5f * posRandom);
            spawnRotation = Quaternion.Euler(new Vector3(angleRandom * Random.value - 0.5f * angleRandom, 0, angleRandom * Random.value - 0.5f * angleRandom));
            spearIdx = Random.Range(0, spears.Length);
            weaponInstance = Instantiate(spears[spearIdx], posRight + new Vector3(0, -10, 0), spawnRotation);
            weaponInstance.GetComponent<TrapWeapons>().attacker = attacker;
            weaponInstance.GetComponent<SpearTrap>().SetTarget(posRight);

            isSkillUsing = true;
        }
        

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

    private CarCheckPoint findCheckPoint(int dist)
    {
        CarCheckPoint[] checkpoints = FindObjectsOfType<CarCheckPoint>();
        if (checkpoints.Length == 0)
        {
            return null;
        }
        dist = Mathf.Clamp(dist, 0, checkpoints.Length - 1);
        foreach (CarCheckPoint point in checkpoints)
        {
            if (point.dist == dist)
            {
                return point;
            }
        }

        Debug.Log("Error in  findCheckPoint(" + dist + ")");
        return checkpoints[0];
    }
}
