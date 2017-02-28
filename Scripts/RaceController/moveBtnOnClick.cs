using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveBtnOnClick : MonoBehaviour {

	public Button MoveBtn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void moveButtonOnClick () {

		MoveBtn.interactable = false;
		GameObject.Find ("RegularC").SendMessage("CDUsed");
		//		PlayerCoolDownValue cdv = GameObject.FindGameObjectWithTag ("MyCar").GetComponent<PlayerCoolDownValue> ();
		//		cdv.CDUsed ();
		GameObject.Find ("RegularC").SendMessage("NitroStart");

	}
}
