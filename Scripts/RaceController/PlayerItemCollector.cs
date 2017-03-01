using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemCollector : MonoBehaviour {

	public Text coinCountText;
	public Button ItemBtn;
	public Text ItemBtnText;

	// we have total 6 kinds of items and effects
	//	1, missle		-> 射出擊中敵人扣血
	//	2, land mine	-> 敵人碰到扣血
	//	3, oil spill	-> 打滑
	//	4, glue			-> 速度變慢一段時間
	//	5, shield		-> 不受技能道具影響
	//	6, lightning	-> 閃電劈所有敵人

	private int numOfItems = 2;

	// number of coins
	private int coinCount;

	// if play doesn't collect an item, the default itemValue = 0;
	private int itemValue = 0;

	// Use this for initialization
	void Start () {
		
		coinCountText.text = "coin x 0";
		coinCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// we have 4 kinds of collectable pickups
	// 1. coin
	// 2. heart
	// 3. poison
	// 4. box (randomly generate items)

	void OnTriggerEnter(Collider other) 
	{
		
		if (other.gameObject.CompareTag ("Coin"))
		{
			other.gameObject.SetActive (false);
			coinCount++;
			coinCountText.text = "coin x " + coinCount.ToString();
		}

		if (other.gameObject.CompareTag ("Heart")) 
		{
			other.gameObject.SetActive (false);

			// implement addHP(amount);
			GameObject.Find ("RegularC").SendMessage("AddHealth", 20);
		}

		if (other.gameObject.CompareTag ("Poison")) 
		{
			other.gameObject.SetActive (false);

			// implement minusHP(amount);
			GameObject.Find ("RegularC").SendMessage("TakeDamage", 50);
		}

		if (other.gameObject.CompareTag ("Arrow")) {
			other.gameObject.SetActive (false);

			ItemBtn.interactable = true;

			int randomValue = Random.Range(1, numOfItems + 1);

			SetItemValue (randomValue);

			switch (randomValue) {
				
			case 1:
				ItemBtnText.text = "missle";
				break;
//			case 2:
//				ItemBtnText.text = "land mine";
//				break;
			case 2:
				ItemBtnText.text = "lightning";
				break;
			case 3:
				ItemBtnText.text = "oil spill";
				break;
			case 4:
				ItemBtnText.text = "glue";
				break;
			case 5:
				ItemBtnText.text = "shield";
				break;
			case 6:
				ItemBtnText.text = "lightning";
				break;
			}
		}


	}

	void SetItemValue(int value) {
	
		itemValue = value;
	}

	public int GetItemValue() {

		return itemValue;
	}

	public int GetCoinNumber() {
	
		return coinCount;
	}
}
