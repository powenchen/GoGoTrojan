using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceSummary : MonoBehaviour {

    public GameObject originalUI;
    public GameObject loadUI;
    public Text description;
	public Text grade;
	private int rank;
	private int coin;
	private string time;
    
    public GameObject[] characters;

    public int debugChar = -1;
    public bool isStartDebug = false;
    // Use this for initialization
    void Awake () {
        if (isStartDebug)
        {
            Load.initialize();
            StaticVariables.GetCharacterAttribute(StaticVariables.characterID);
        }
        
        if (debugChar != -1)
        {
            StaticVariables.characterID = debugChar;
        }
        for (int i =0;i< characters.Length;++i)
        {
            if (i == StaticVariables.characterID)
            {
                characters[i].SetActive(true);
            }
        }
        /*
		int playerID = PlayerPrefs.GetInt ("PlayerID");

		if(playerID == 0) {
			Destroy (character2);
		} else if(playerID == 1) {
			Destroy (character1);
		}
        */
        coin = StaticVariables.coinNumber;//PlayerPrefs.GetInt ("Coins");
		rank = StaticVariables.ranking;
        time = StaticVariables.raceTimeStr;//PlayerPrefs.GetString ("TotalTime");
        Debug.Log("rank = "+rank);    
        /*
		if (rank == 1) {
			grade.text = "A";
			description.text = "CONGRATULATIONS!\nYou were the FIRST ONE who reached the classroom!! You spent " + time + " to reach the classroom. Professor was very happy and decided to give you a BIG A!!";
		} else {
			grade.text = "B";
			description.text = "UNFORTUNATELY...\nYou were NOT the first one who reached the classroom... You spent " + time + " to reach the classroom. As a result, you are not assigned an A... Try again!!";
		}
		description.text += "\nYou've eared " + coin + " coins during the race. You can go to store to make upgration!";
        */


    }
	
	// Update is called once per frame
	void Update () {

        if (StaticVariables.ranking == 1)
        {
            grade.text = "YOU  WIN!!";
        }
        else
        {

            grade.text = "FAILED ... ";
        }

    }

    public void ToMainMenu () {
		SceneManager.LoadScene ("MainMenu");
	}

	public void ToStore() {
		//SceneManager.LoadScene ("");
	}

	public void PlayAgain() {
        originalUI.SetActive(false);
        loadUI.SetActive(true);
        SceneManager.LoadSceneAsync ("LA_large");
	}

	public void QuitGame() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
