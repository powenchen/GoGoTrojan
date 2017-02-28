using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemEffects : MonoBehaviour {


	public Text testText;

	// Use this for initialization
	void Start () {
		testText.text = "effect: ";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UseItem(int itemValue) {
	
		// 1, missle 		-> HP - 30										射出擊中敵人扣血
		// 2, land mine		-> HP - 30										敵人經過上方扣血
		// 3, oil spill		-> spin?										原地打滑
		// 4, glue			-> speed slows down								速度變慢
		// 5, shield		-> items can't affect the player for 5 seconds	防護罩
		// 6, sprint		-> speed increase								衝刺
		// 7, lightning		-> all player HP - 20							閃電
		// 8, time freeze	-> all player stop for 3 seconds				時間停止器, 畫面變灰階

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
			Sprint ();
			break;
		case 7:
			Lightning ();
			break;
		case 8:
			TimeFreeze ();
			break;
		}
	
	
	}

	public void Missle() {
		testText.text = "effect: Missle!";
	}

	public void LandMine() {
		testText.text = "effect: Land Mine!";
	}

	public void OilSpill() {
		testText.text = "effect: Oil Spill!";
	}

	public void Glue() {
		testText.text = "effect: Glue!";
	}

	public void Shield() {
		testText.text = "effect: Shield!";
	}

	public void Sprint() {
		testText.text = "effect: Sprint!";
	}

	public void Lightning() {
		testText.text = "effect: Lightning!";
	}

	public void TimeFreeze() {
		testText.text = "effect: Time Freeze!";
	}
}
