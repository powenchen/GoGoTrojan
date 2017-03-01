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
		if(MapPickUp.routePicked == 1) { SceneManager.LoadScene("race"); }
		else if(MapPickUp.routePicked == 2) { SceneManager.LoadScene("MainMenu"); }

	}

	public void GoBack() {
		SceneManager.LoadScene("CharacterPickUp");
	}

	public void Store() {
		//SceneManager.LoadScene("CharacterPickUp");
	}

}
