using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : TrapWeapons { 

	public GameObject explosion;

    private float damageValue = 1.5f;

    private bool alreadyPut;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) 
	{
        if (other.GetComponent<Car>() != null)
        {
            // timestop mode is invincible
            if (other.GetComponent<TimeStopSkill>() != null && other.GetComponent<TimeStopSkill>().isSkillUsing)
            {
                return;
            }
            Instantiate(explosion, other.transform.position, other.transform.rotation);

            other.GetComponent<CarStatus>().isAttackedBy(attacker,damageValue);
            Destroy(gameObject);
        }
        

	}
    
}
