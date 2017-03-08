using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour {

	public GameObject explosion;

	public float damageValue; // default value = 30

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
            Instantiate(explosion, other.transform.position, other.transform.rotation);

            other.GetComponent<Car>().decreaseHP(damageValue);
            Destroy(gameObject);
        }
        

	}
    
}
