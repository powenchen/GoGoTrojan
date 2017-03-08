using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour {

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
            if (other.GetComponent<TimeStopSkill>() != null && other.GetComponent<TimeStopSkill>().isSkillUsing())
            {
                return;
            }
            other.GetComponent<Car>().speedDebuff();
            Destroy(gameObject);
        }

	}
    /*
	void OnTriggerExit(Collider other) {

		// this item works only after player put this item
		alreadyPut = true;
	}*/
}
