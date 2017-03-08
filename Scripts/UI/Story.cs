using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour {

	public Button confirmText;
	public Button backText;

	// Use this for initialization
	void Start () {
		Debug.Log (PlayerPrefs.GetFloat("Player1_Health"));
		confirmText = confirmText.GetComponent<Button> ();
		backText = backText.GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Confirm() {
		SceneManager.LoadScene ("MapPickUp");
	}

	public void GoBack() {
		SceneManager.LoadScene ("MainMenu");
	}
}
