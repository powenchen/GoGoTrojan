using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : TrapWeapons
{
    private float damageValue = 1.5f;

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
            if (other.GetComponent<CarStatus>().Equals(attacker))
            {
                return;
            }
            // timestop mode is invincible
            if (other.GetComponent<TimeStopSkill>() != null && other.GetComponent<TimeStopSkill>().isSkillUsing)
            {
                return;
            }

            other.GetComponent<CarStatus>().isAttackedBy(attacker, damageValue);
            Destroy(gameObject);
        }


    }
}
