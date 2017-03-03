using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceSummary : MonoBehaviour {

	public Text description;
	public Text grade;
	private int rank;
	private int coin;
	private string time;


	// Use this for initialization
	void Start () {
		coin = PlayerPrefs.GetInt ("TotalCoins");
		rank = PlayerPrefs.GetInt("Ranking");
		time = PlayerPrefs.GetString ("Time");

		if (rank == 1) {
			grade.text = "A";
			description.text = "CONGRATULATIONS!\nYou were the FIRST ONE who reached the classroom!! Professor was very happy and decided to give you a BIG A!!";
		} else {
			grade.text = "B";
			description.text = "UNFORTUNATELY...\nYou were NOT the first one who reached the classroom... As a result, you are not assigned an A... Try again!!";
		}
		description.text += "\nYou've eared " + coin + " coins during the race. You can go to store to make upgration!";

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToMainMenu () {
		SceneManager.LoadScene ("MainMenu");
	}

	public void ToStore() {
		//SceneManager.LoadScene ("");
	}

	public void PlayAgain() {
		SceneManager.LoadScene ("Race");
	}

	public void QuitGame() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
