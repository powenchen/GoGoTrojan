using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goal : MonoBehaviour {
    public Text goalTxt;

    private float lifetime = 3.0f;
    private bool isOver = false;
    // Use this for initialization
    void Start () {
        goalTxt.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (isOver && goalTxt!=null)
        {
            Destroy(goalTxt.gameObject, lifetime);
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player") && !isOver)
        {
            goalTxt.text = "YOU WIN!!";
            isOver = true;

        }
        if (other.CompareTag("Enemy") && !isOver)
        {
            goalTxt.text = "YOU LOSE!!";
            isOver = true;

        }
    }
}
