using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterPickUp : MonoBehaviour {

	public Button confirmText;
	public Button backText;
	public Button storeText;

	public static int characterPicked;

	// Use this for initialization
	void Start () {
		confirmText = confirmText.GetComponent<Button> ();
		backText = backText.GetComponent<Button> ();
		storeText = storeText.GetComponent<Button> ();
		characterPicked = 1;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Confirm() {
		Debug.Log ("In prevous scene, you picked route: " + MapPickUp.routePicked);
		Debug.Log ("In this scene, you have picked character: " + characterPicked);
		SceneManager.LoadScene ("CarPickUp");
	}

	public void Goback() {
		SceneManager.LoadScene ("MapPickUp");
	}

	public void Store() {
		//SceneManager.LoadScene ();
	}
}
