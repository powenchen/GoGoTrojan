using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemEffects : MonoBehaviour {

	public GameObject missile;
	public GameObject landMine;
	public GameObject oilSpill;
	public GameObject glue;
//	public GameObject shield;
	public GameObject lightning;

	public GameObject frontSpawn;
	public GameObject backSpawn;

	public ParticleSystem shieldPS;

	private bool ShieldEquipped;

	public GameObject[] Players;


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void UseItem(int itemValue) {

		// we have total 6 kinds of items and effects
		//	1, missle		-> 射出擊中 敵人減速 & 扣血
		//	2, land mine	-> 敵人碰到減速 & 扣血
		//	3, oil spill	-> 打滑 & 減速
		//	4, glue			-> 速度變慢一段時間
		//	5, shield		-> 不受道具效果影響
		//	6, lightning	-> 閃電全範圍扣血

		switch (itemValue) {
		case 1:
			Missle ();
			break;
		case 2:
			LandMine ();
			break;
		case 3:
			OilSpill ();
			break;
		case 4:
			Glue ();
			break;
		case 5:
			Shield ();
			break;
		case 6:
			Lightning ();
			break;
		}


	}

	public void Missle() {

		// generate a missile

		Instantiate(missile,frontSpawn.transform.position,frontSpawn.transform.rotation);
	}

	public void LandMine() {

		Instantiate(landMine,backSpawn.transform.position,backSpawn.transform.rotation);
	}

	public void OilSpill() {

		Instantiate(oilSpill,transform.position,transform.rotation);
	}

	public void Glue() {

		Instantiate(glue,transform.position,transform.rotation);
	}

	public void Shield() {
		
		shieldPS.Play ();
		ShieldEquipped = true;
		Debug.Log ("USE SHIELD");
		Invoke ("UnequipShield", 3);
	}


	void UnequipShield() {
		Debug.Log ("NO SHIELD NOW");
		ShieldEquipped = false;
	}

	public bool IsShieldEquipped() {
	
		return ShieldEquipped;
	}


	public void Lightning() {

//		GameObject enemy = GameObject.Find ("RaceC");
//		Instantiate(lightning, enemy.transform.position, enemy.transform.rotation);
//		enemy.SendMessage("TakeDamage", 20);


		// attack all players!
		foreach (GameObject go in Players) {

			if (!go.tag.Equals(gameObject.tag)) {

				Instantiate(lightning, go.transform.position, go.transform.rotation);

//				itemEffects ie = go.GetComponent<itemEffects> ();
//				if (!ie.IsShieldEquipped ()) {
					go.SendMessage ("TakeDamage", 20);
//				}

			}
		}


	}
		
}
