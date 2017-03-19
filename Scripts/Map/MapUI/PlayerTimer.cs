using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour {

	public Text timerText;
	public Text preTimerText;
	private float startTime;
	private float departTime;

	public string totalMinute;
	public string totalSecond;
	public string totalMiliSecond;

	int testTime;
	bool finished;
	bool canStartRacing;
    // Use this for initialization
    void Start () {
        
        timerText.color = Color.red;
		startTime = Time.time;
		preTimerText.text = "";
//		testTime = 1;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (StaticVariables.gameIsOver)
        {
            Finish();
        }
		if (finished) {
			return;
		}

		testTime = (int)(startTime - Time.time + 4);

		// when test time = 3, 2, 1
		if ( testTime >= 1 && testTime <=3) {
            StaticVariables.musicStartFlag = true;
			preTimerText.text = testTime.ToString ();

		} 
		else if (testTime == 0) {
			
			preTimerText.text = "GO!";
			departTime = Time.time;
		}
		else if(testTime < 0){
            //startrunning
            //Debug.Log("GAME START");
            StaticVariables.gameStarts = true;
            preTimerText.text = "";

			float timer = Time.time - departTime;
			totalMinute = ((int)timer / 60).ToString ("D2");
			totalSecond = ((int)timer % 60).ToString ("D2");
			totalMiliSecond = ((int)(timer * 100) % 100).ToString ("D2");
			timerText.text =totalMinute + ":" + totalSecond + ":" + totalMiliSecond;

            StaticVariables.raceTimeStr = totalMinute + ":" + totalSecond + ":" + totalMiliSecond;

        }
	}


	void Finish() {
		timerText.color = Color.yellow;
		finished = true;
        //stop all cars

	}

	/*public void DeathFinish() {
		timerText.color = Color.red;
		finished = true;
		GameObject.Find ("RegularC").SendMessage("setGameOver");
		GameObject.Find ("RaceC").SendMessage("setStopRacing");
		GameObject.Find ("RegularC").SendMessage("SetCDstops");
		GameObject.Find ("RegularC").SendMessage("GameOver");

	}*/

	/*public string GetTotalTime() {
		string result = totalMinute + ":" + totalSecond + ":" + totalMiliSecond;
		return result;
	}*/
}
