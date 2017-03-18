using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    public Button btn;
    public StatImageManager statManager;
    public StatBtnOnClick statBtn;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClickPause()
    {
        statManager.gameObject.SetActive(true);
        statBtn.setPauseFlag(true);
        statManager.setPauseFlag(true);
        foreach (AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            audio.Pause();
        }
        Time.timeScale = 0;
    }
}
