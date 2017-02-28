using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour {

	public GameObject thunderMissile;
	public GameObject bananaTrap;
	public Transform frontShotSpawn;
	public Transform backShotSpawn;
	public float speedBuffRatio;
	public float speedBuffTime;
	public float healPotion;
	public string currentItem;


	private bool hasItem = false;
	private bool speedBuffed = false;
	private float speedBuffTil;

	void heal(){
		float health = GetComponent<CarHealthMonitor>().health;
		float maxHealth = GetComponent<CarHealthMonitor>().health;
		if (health > maxHealth) {
			health = maxHealth;
		}
	}

	void bleed(){
		float health = GetComponent<CarHealthMonitor>().health;
		float maxHealth = GetComponent<CarHealthMonitor>().health;
		if (health <= 0) {
			health = 0;
		}
	}

	void accelerate(){
		Debug.Log ("accerlating");
		GetComponent<mover>().removeDebuff(speedBuffRatio);
		return;
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.layer == 8) 
		{
			other.gameObject.SetActive (false);
			switch (other.gameObject.tag)
			{
			case "heal":
				heal ();
				break;
			case "poison":
				bleed ();
				break;
			case "acceleration":
				speedBuffTil = Time.time + speedBuffTime;
				accelerate ();
				speedBuffed = true;
				break;
			default:
				hasItem = true;	
				currentItem = other.gameObject.tag;
				break;
			}
		}
	}

	void Update()
	{
		if (hasItem && Input.GetButton ("Fire1")) 
		{
			switch (currentItem) 
			{
			case "banana":
				Instantiate (bananaTrap, backShotSpawn.position, backShotSpawn.rotation);
				break;
			case "thunder":
				Instantiate (thunderMissile, frontShotSpawn.position, frontShotSpawn.rotation);
				break;
			default:
				break;
			}
			hasItem = false;
			currentItem = "";
		}

		if (speedBuffed && Time.time > speedBuffTil) 
		{
			GetComponent<mover> ().speedDebuff (speedBuffRatio);
			speedBuffed = false;
		}


	}
}
