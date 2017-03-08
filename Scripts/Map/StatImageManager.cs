﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatImageManager : MonoBehaviour {
    // public Button playAgain;
    //public Button leave;
    public PlayerItemCollector collector;
    public bool isWin;

    public Text coinText;
    public Text timeText;
    public Text winText;
    public Text rankText;
    public Text rightBtnText;
    public Text leftBtnText;
    private int totalCarNum;
    private bool pauseFlag = false;

   // public StatBtnOnClick statBtn;
    // Use this for initialization
    void Start ()
    {
        totalCarNum = FindObjectsOfType<Car>().Length;
    }
	
	// Update is called once per frame
	void Update () {
        if (pauseFlag)
        {
            rightBtnText.text = "Resume";
            leftBtnText.text = "Leave";
            coinText.text = "";
            timeText.text = "";
            winText.text = " PAUSE ";
            rankText.text = "";

            return;
        }
        rightBtnText.text = "Continue";
        leftBtnText.text = "Play Again";
        
        coinText.text = "Coins get: " + collector.GetCoinNumber().ToString();
        timeText.text = "Time: " + GetComponentInParent<PlayerTimer>().GetTotalTime();
        PlayerPrefs.SetString("TotalTime", GetComponentInParent<PlayerTimer>().GetTotalTime());
        winText.text = isWin ? "YOU WIN!" : "YOU LOSE!";
        rankText.text = "Rank: "+ GetComponentInParent<rankingSystem>().getMyRank().ToString() + "/"  + totalCarNum.ToString();
    }

    public void setPauseFlag(bool flag)
    {
        pauseFlag = flag;
    }
}