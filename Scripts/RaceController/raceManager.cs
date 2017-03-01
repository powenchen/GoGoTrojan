using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raceManager : MonoBehaviour {

	public Button gameOverBtn;
	public Image StatImage;
//	public PlayerItemCollector player;
	public Text CoinCountText;
	public Text RankText;
	public Text TimeRecordText;
	public Text WinLoseText;
	public Text TotalScoreText;

	private int coinCount;
	private int totalCoinCount;
	private string position;
	private string winOrLose;
	private bool isDead;

	PlayerItemCollector playerIC;
	carCheckPoint rank;
	PlayerTimer ptimer;
	PlayerHealthValue playerHealth;


	void Awake() 
	{
		playerIC = GetComponent<PlayerItemCollector> ();
		rank = GetComponent<carCheckPoint> ();
		ptimer = GetComponent<PlayerTimer> ();
		playerHealth = GetComponent<PlayerHealthValue> ();
	}



	// Use this for initialization
	void Start () {

		if(PlayerPrefs.HasKey("TotalCoins")){
			int score = PlayerPrefs.GetInt("TotalCoins");
			Debug.Log("score value:" + score);
		}else{
			Debug.Log("Non Existing Key");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameOver() {

		coinCount = playerIC.GetCoinNumber ();
		int rankInt = rank.GetCarPosition ();
		position = AddOrdinal(rankInt);
		isDead = playerHealth.isPlayerDead ();

		if (rankInt != 1 || isDead) {
			winOrLose = "YOU LOSE!";
		} 
		else {
			winOrLose = "YOU WIN!";
		}

		totalCoinCount = PlayerPrefs.GetInt ("TotalCoins") + coinCount;
		PlayerPrefs.SetInt("TotalCoins", totalCoinCount);
		Invoke ("SetGameOver", 3.0f);
	}


	public void SetGameOver() {
		CoinCountText.text = "coin x " + coinCount;
		RankText.text = "rank: " + position;
		TimeRecordText.text = "time: " + ptimer.GetTotalTime ();
		WinLoseText.text = winOrLose;
		TotalScoreText.text = "Total Score: " + PlayerPrefs.GetInt ("TotalCoins");
		StatImage.gameObject.SetActive(true);


	}


	// convert int to order. eg. 1 -> 1st, 2 -> 2nd.
	public static string AddOrdinal(int num)
	{
		if( num <= 0 ) return num.ToString();

		switch(num % 100)
		{
		case 11:
		case 12:
		case 13:
			return num + "th";
		}

		switch(num % 10)
		{
		case 1:
			return num + "st";
		case 2:
			return num + "nd";
		case 3:
			return num + "rd";
		default:
			return num + "th";
		}
	}
}
