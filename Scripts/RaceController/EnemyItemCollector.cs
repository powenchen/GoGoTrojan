using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemCollector : MonoBehaviour {

//	bool ItemCollected = false;

	// we have total 6 kinds of items and effects
	//	1, missle		-> 射出擊中敵人扣血
	//	2, land mine	-> 敵人碰到扣血
	//	3, oil spill	-> 打滑
	//	4, glue			-> 速度變慢一段時間
	//	5, shield		-> 不受技能道具影響
	//	6, lightning	-> 閃電劈所有敵人
	private int numOfItems = 6;

	// if player doesn't collect an item, the default itemValue = 0;
	// when player get a item, the item value will be 1 ~ 6;
	private int itemValue = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (itemValue != 0) {
			Debug.Log ("enemy get item no." + itemValue);

			// call USEITEM()


			itemValue = 0;
		}
	}

	// we have 4 kinds of collectable pickups
	// 1. coin  								// But enemy doesn't eat coins
	// 2. heart
	// 3. poison
	// 4. box (randomly generate items)

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag ("Coin")) {
			other.gameObject.SetActive (false);
		}


		if (other.gameObject.CompareTag ("Heart")) {
			other.gameObject.SetActive (false);


			// implement addHP(amount);
			GameObject.Find ("RaceC").SendMessage("healDamage", 20);
		}

		if (other.gameObject.CompareTag ("Poison")) {
			other.gameObject.SetActive (false);

			// implement minusHP(amount);
			GameObject.Find ("RaceC").SendMessage("takeDamage", 50);
		}

		if (other.gameObject.CompareTag ("Arrow")) {
			other.gameObject.SetActive (false);

			int randomValue = Random.Range (1, numOfItems + 1);

			SetItemValue (randomValue);

//			switch (randomValue) {
//
//			case 1:
////				ItemBtnText.text = "missle";
//				break;
//			case 2:
////				ItemBtnText.text = "land mine";
//				break;
//			case 3:
////				ItemBtnText.text = "oil spill";
//				break;
//			case 4:
////				ItemBtnText.text = "glue";
//				break;
//			case 5:
////				ItemBtnText.text = "shield";
//				break;
//			case 6:
////				ItemBtnText.text = "lightning";
//				break;
//			}
		}
	}


	public void SetItemValue(int value) {

		itemValue = value;
	}
}
