using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class itemBtnOnClick : MonoBehaviour {

	public Button ItemBtn;
	public Text ItemBtnText;
	public PlayerItemCollector playerItemCollector;
	public Text test;


	public Button LeaveBtn;

	// Use this for initialization
	void Start () {
		if (ItemBtnText != null) {
			ItemBtnText.text = "empty";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void itemButtonOnClick () {

		ItemBtn.interactable = false;
		ItemBtnText.text = "empty";

		int itemValue = playerItemCollector.GetItemValue ();
		test.text = "itemValue = " + itemValue.ToString ();

		GameObject.Find ("RegularC").SendMessage("UseItem" , itemValue);


	}

	public void LeaveButtonOnClick() {
		SceneManager.LoadScene("CharacterPickUp");
	}
}
