using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatImageManager : MonoBehaviour {
    // public Button playAgain;
    //public Button leave;
    public ItemCollector collector;
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

        totalCarNum = FindObjectsOfType<AIScript>().Length+1;
        coinText.text = "Coins get: " + StaticVariables.coinNumber.ToString();
        timeText.text = "Time: " + StaticVariables.raceTimeStr;
        //PlayerPrefs.SetString("TotalTime", GetComponentInParent<PlayerTimer>().GetTotalTime());
        int rank = Mathf.Clamp(StaticVariables.ranking, 1, totalCarNum);
        winText.text = (rank == 1) ? "YOU WIN!" : "YOU LOSE!";
        rankText.text = "Rank: "+ rank.ToString() + "/"  + totalCarNum.ToString();
    }

    public void setPauseFlag(bool flag)
    {
        pauseFlag = flag;
    }
}
