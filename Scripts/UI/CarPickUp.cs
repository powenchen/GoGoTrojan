using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarPickUp : MonoBehaviour {

	public Button confimrText;
	public Button backText;
	public Button storeText;

	public static int whichCar = 0;

	// Use this for initialization
	void Start () {
		confimrText = confimrText.GetComponent<Button> ();
		backText = backText.GetComponent<Button> ();
		storeText = storeText.GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Confirm() {
		SceneManager.LoadScene("Race");
	}

	public void GoBack() {
		Debug.Log ("in CarPickUp; you picked up character: " + CharacterPickUp.whichCharacter);
		Debug.Log ("in CarPickUp: you picked up car: " + whichCar);
		SceneManager.LoadScene("CharacterPickUp");
	}

	public void Store() {
		//SceneManager.LoadScene("CharacterPickUp");
	}

}
