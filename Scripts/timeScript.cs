using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeScript : MonoBehaviour {
    private Text timeText;
	// Use this for initialization
	void Start () {
        timeText = GetComponent<Text>();
        timeText.text = "00:00";
	}
	
	// Update is called once per frame
	void Update () {
        float minutes = Mathf.Round(Time.time / 60f);

        float seconds = Mathf.Round((Time.time % 60f)*10f)/10f;
        timeText.text = minutes.ToString()+":" + seconds.ToString();
	}
}
