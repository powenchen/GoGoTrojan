using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHealthMonitor : MonoBehaviour {
	
	public float health;
	public float maxHealth;

	bool isDead (){
		if (health <= 0) {
			return true;
		} else {
			return false;
		}
	}

	void gameover(){
		Debug.Log ("Game Over");
		return;
	}

	// Use this for initialization
	void Start () {
		health = maxHealth;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead ()) {
			gameover ();
		}
	}
}
