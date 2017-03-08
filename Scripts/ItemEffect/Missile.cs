using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	public float speed;  // default value = 70

	public float damageValue; // default value = 50

	public GameObject explosion;

    
//	private bool alreadyPut = false;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Translate (0, 0, speed * Time.deltaTime);
		Destroy (gameObject, 5);
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
            Debug.Log("before missile HP = " + other.GetComponent<Car>().getHP());
            Instantiate(explosion, other.transform.position, other.transform.rotation);

            other.GetComponent<Car>().decreaseHP(damageValue);

            Debug.Log("after missile HP = " + other.GetComponent<Car>().getHP());
            Destroy(gameObject);
        }
        
	}

	/*void OnTriggerExit(Collider other) {

		// this item works only after player put this item
		alreadyPut = true;
	}*/
}
