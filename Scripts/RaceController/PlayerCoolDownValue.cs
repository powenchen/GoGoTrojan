using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoolDownValue : MonoBehaviour {

	public Slider CDSlider;
	public Button MoveBtn;
	public Text MoveBtnText;

	public int startingCD = 0;
	public int currentCD;
	public int CDspeed = 10;

	private bool CDstarts = false;


	void Awake () {

		currentCD = startingCD;
	}

	// Use this for initialization
	void Start () {

		CDSlider.value = currentCD;
	}
	
	// Update is called once per frame
	void Update () {

		if (CDstarts) {
			
			if (currentCD >= 100) {
				currentCD = 100;
				CDSlider.value = 100;
				MoveBtn.interactable = true;
				MoveBtnText.text = "Nitro";


			} 
			else {
			
				CDSlider.value += Time.deltaTime * CDspeed;
				currentCD = (int)CDSlider.value;
			}
		}
	}

	public void CDUsed() {
		CDSlider.value = 0;
		currentCD = 0;
	}

	public void SetCDstarts() {
		CDstarts = true;
	}

	public void SetCDstops() {
		CDstarts = false;
	}
}
