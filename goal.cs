using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goal : MonoBehaviour {
    public Text goalTxt;
	// Use this for initialization
	void Start () {
        goalTxt.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            goalTxt.text = "YOU WIN!!";

        }
    }
}
