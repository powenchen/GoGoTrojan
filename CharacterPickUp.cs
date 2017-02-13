using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterPickUp : MonoBehaviour {

	public Button confirmText;
	public Button backText;
	public Button storeText;

	public static int whichCharacter = 0;

	// Use this for initialization
	void Start () {
		confirmText = confirmText.GetComponent<Button> ();
		backText = backText.GetComponent<Button> ();
		storeText = storeText.GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Confirm() {
		Debug.Log ("in CharacterPickUp; you picked up character: " + whichCharacter);
		Debug.Log ("in CharacterPickUp: you picked up car: " + CarPickUp.whichCar);
		SceneManager.LoadScene ("CarPickUp");
	}

	public void Goback() {
		//SceneManager.LoadScene ();
	}

	public void Store() {
		//SceneManager.LoadScene ();
	}
}
