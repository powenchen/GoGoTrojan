using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterPickUp : MonoBehaviour {

	public Button confirmText;
	public Button backText;
	public Button storeText;
	public Button nextCharacterText;
	public Button previousCharacterText;
	public Text title;
	public Text values;
	public Text letterGrade;

	public static int characterPicked;

	public CameraMover MyCameraMover; 

	// Use this for initialization
	void Start () {

		confirmText = confirmText.GetComponent<Button> ();
		backText = backText.GetComponent<Button> ();
		storeText = storeText.GetComponent<Button> ();
		nextCharacterText = nextCharacterText.GetComponent<Button> ();
		previousCharacterText = previousCharacterText.GetComponent<Button> ();
		characterPicked = 0;
		ChangeCharacterDetail (characterPicked);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Confirm() {
        //PlayerPrefs.SetInt ("PlayerID", characterPicked);
        StaticVariables.characterID = characterPicked;

        SceneManager.LoadScene ("CarPickUp");
	}

	public void Goback() {
		SceneManager.LoadScene ("MapPickUp");
	}

	public void Store() {
		SceneManager.LoadScene ("CharacterAttributes");
	}

	public void NextCharacter() {
		characterPicked = 1;
		ChangeCharacterDetail (characterPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(1, 0.5f));
		nextCharacterText.enabled = false;
		previousCharacterText.enabled = true;
	}

	public void PreviousCharacter() {
		characterPicked = 0;
		ChangeCharacterDetail (characterPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(0, 0.5f));
		nextCharacterText.enabled = true;
		previousCharacterText.enabled = false;
	}

	private void ChangeCharacterDetail(int characterNumber) {
		float player1HP = PlayerPrefs.GetFloat ("PlayerOne_Health");
		float player1MP = PlayerPrefs.GetFloat ("PlayerOne_Mana");
		float player1Speed = PlayerPrefs.GetFloat ("PlayerOne_Speed");
		float player1Power = PlayerPrefs.GetFloat ("PlayerOne_Power");

		float player2HP = PlayerPrefs.GetFloat ("PlayerTwo_Health");
		float player2MP = PlayerPrefs.GetFloat ("PlayerTwo_Mana");
		float player2Speed = PlayerPrefs.GetFloat ("PlayerTwo_Speed");
		float player2Power = PlayerPrefs.GetFloat ("PlayerTwo_Power");

		switch (characterNumber) {
		case 0:
			title.text = "V I T E R B I  S C H O O L  O F  E N G I N E E R I N G\nN e r d y";
			values.text = player1HP + " pts\n" + player1MP + " pts\n" + player1Power + " N\n" + player1Speed + " m/hr\n" + "N2O";
			letterGrade.text = "B\nC\nC\nC\nB";
			break;
		case 1:
			title.text = "S C H O O L  O F  C I N E M A T I C  A R T S\nS t a r";
			values.text = player2HP + " pts\n" + player2MP + " pts\n" + player2Power + " N\n" + player2Speed + " m/hr\n" + "Time Stop";
			letterGrade.text = "C\nB\nB\nB\nA";
			break;
		default:
			break;
		}
	}
}
