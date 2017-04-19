using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutButtonChange : MonoBehaviour
{
    public GameObject aboutCanvas;
    public GameObject settingCanvas;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //change back to setting
    public void OnClickBack()
    {
        settingCanvas.SetActive(true);
        aboutCanvas.SetActive(false);
    }

    //go to about
    public void OnClickAbout()
    {
        settingCanvas.SetActive(false);
        aboutCanvas.SetActive(true);
    }


    //close setting panel
    public void OnClickClose()
    {
        settingCanvas.SetActive(false);
        aboutCanvas.SetActive(false);

        foreach (Button btn in FindObjectsOfType<Button>())
        {
            btn.interactable = true;
        }
    }

    //enter setting panel
    public void OnClickSetting()
    {
        foreach (Button btn in FindObjectsOfType<Button>())
        {
            btn.interactable = false;
        }
        settingCanvas.SetActive(true);
        aboutCanvas.SetActive(false);
    }
}
