using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour {
    private bool isOver = false;
    private int status = 0; // 1=win, 0 = no result, -1 = lose
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player") && !isOver)
        {
            status = 1;
            isOver = true;

        }
        else if (other.CompareTag("Enemy") && !isOver)
        {
            status = -1;
            isOver = true;

        }
    }

    //status = 0 no result yet
    //status = 1 win
    //status = -1 lose
    public int getStatus()
    {
        return status;
    }
}
