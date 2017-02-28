using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raceManager : MonoBehaviour {

	public Button gameOverBtn;
	public Image StatImage;
	public PlayerItemCollector player;
	public Text coincountText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameOver() {
//		gameOverBtn.gameObject.SetActive (true);
		StatImage.gameObject.SetActive(true);
		coincountText.text = "coin: " + player.GetCoinNumber ();
	}
}
