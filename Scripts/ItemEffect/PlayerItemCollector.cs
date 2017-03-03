using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemCollector : MonoBehaviour {

	public Text coinCountText;
	public Button ItemBtn;
	public Text ItemBtnText;

	// we have total 8 kinds of items and effects
	// 1, missle 		-> HP - 30										射出擊中敵人扣血
	// 2, land mine		-> HP - 30										敵人經過上方扣血
	// 3, oil spill		-> spin?										原地打滑
	// 4, glue			-> speed slows down								速度變慢
	// 5, shield		-> items can't affect the player for 5 seconds	防護罩
	// 6, sprint		-> speed increase								衝刺
	// 7, lightning		-> all player HP - 20							閃電
	// 8, time freeze	-> all player stop for 3 seconds				時間停止器, 畫面變灰階
	private int numOfItems = 8;

	// number of coins
	private int coinCount;

	// if play doesn't collect an item, the default itemValue = 0;
	private int itemValue = 0;

	// Use this for initialization
	void Start () {
		numOfItems = 8;
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
			case 2:
				ItemBtnText.text = "land mine";
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
				ItemBtnText.text = "sprint";
				break;
			case 7:
				ItemBtnText.text = "lightning";
				break;
			case 8:
				ItemBtnText.text = "time freeze";
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
