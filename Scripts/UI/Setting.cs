using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour {

	public Slider musicSlider;
	public GameObject confirmPanel;
	public Button aboutBtn;
	public Button resetBtn;
	public Button yesBtn;
	public Button noBtn;
	public Button backBtn;

	//	public AudioSource musicSource;

	void Start() {
        musicSlider.value = AudioListener.volume;

        enableBtns ();
		confirmPanel.SetActive (false);

	}

	void Update () {
        //		musicSource.volume = musicSlider.value;
    }

    public void OnChangeVolume()
    {
        AudioListener.volume = musicSlider.value;
        StaticVariables.SetVolume(musicSlider.value);
        Save.saveState();
    }

	public void gameReset() {
		disableBtns ();
		confirmPanel.SetActive (true);
	}

	public void yes() {
		Load.initialize (true);
		confirmPanel.SetActive (false);
		enableBtns ();
	}

	public void no() {
		confirmPanel.SetActive (false);
		enableBtns ();
	}

	private void enableBtns() {
		musicSlider.interactable = true;
		aboutBtn.interactable = true;
		resetBtn.interactable = true;
		backBtn.interactable = true;
	}

	private void disableBtns() {
		musicSlider.interactable = false;
		aboutBtn.interactable = false;
		resetBtn.interactable = false;
		backBtn.interactable = false;
	}
}