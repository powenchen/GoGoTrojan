using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarPickUp : MonoBehaviour {

	public Button confimrText;
	public Button backText;
	public Button storeText;
	public Button nextCarText;
	public Button previousCarText;
	public Text carDetailTitle;
	public Text carDetailParameters;
	public Text carDetailRank;

	public CameraMover MyCameraMover;

	public static int whichCar;

	// Use this for initialization
	void Start () {
		confimrText = confimrText.GetComponent<Button> ();
		backText = backText.GetComponent<Button> ();
		storeText = storeText.GetComponent<Button> ();
		nextCarText = nextCarText.GetComponent<Button> ();
		previousCarText = previousCarText.GetComponent<Button> ();
		whichCar = 0;
		ChangeCarDetail(whichCar);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Confirm() {
		// Call Map API to set car
		PlayerPrefs.SetInt("CarID", whichCar);

		SceneManager.LoadScene("LA_large");

	}

	public void GoBack() {
		SceneManager.LoadScene("CharacterPickUp");
	}

	public void Store() {
		SceneManager.LoadScene("CharacterAttributes");
	}

	public void NextCar() {
		whichCar = 1;
		ChangeCarDetail (whichCar);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(1, 0.5f));
		nextCarText.enabled = false;
		previousCarText.enabled = true;
	}

	public void PreviousCar() {
		whichCar = 0;
		ChangeCarDetail (whichCar);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(0, 0.5f));
		nextCarText.enabled = true;
		previousCarText.enabled = false;
	}

	private void ChangeCarDetail(int carNumber) {
		float car1HP = PlayerPrefs.GetFloat ("CarOne_Health");
		float car1MP = PlayerPrefs.GetFloat ("CarOne_Mana");
		float car1Speed = PlayerPrefs.GetFloat ("CarOne_Speed");
		float car1Power = PlayerPrefs.GetFloat ("CarOne_Power");

		float car2HP = PlayerPrefs.GetFloat ("CarTwo_Health");
		float car2MP = PlayerPrefs.GetFloat ("CarTwo_Mana");
		float car2Speed = PlayerPrefs.GetFloat ("CarTwo_Speed");
		float car2Power = PlayerPrefs.GetFloat ("CarTwo_Power");


		switch (carNumber) {
		case 0:
			carDetailTitle.text = "U S C  C r u s i e r";
			carDetailParameters.text = "+" + car1HP + " pts\n+" + car1MP + " pts\n+" + car1Speed + " km/hr\n+" + car1Power + " N";
			carDetailRank.text = "A\nS\nA\nS";
			break;
		case 1:
			carDetailTitle.text = "D P S  P a t r o l";
			carDetailParameters.text = "+" + car2HP + " pts\n+" + car2MP + " pts\n+" + car2Speed + " km/hr\n+" + car2Power + " N";
			carDetailRank.text = "S\nA\nB\nA";
			break;
		default:
			break;
		}
	}

}
