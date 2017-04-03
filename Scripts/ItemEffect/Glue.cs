using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Glue : TrapWeapons
{

	private bool alreadyPut;

    private float damageValue = 0;
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
            other.GetComponent<CarStatus>().isAttackedBy(attacker, damageValue);
            other.GetComponent<CarStatus>().speedDebuff();
            Destroy(gameObject);
        }

	}
    /*
	void OnTriggerExit(Collider other) {

		// this item works only after player put this item
		alreadyPut = true;
	}*/
}
