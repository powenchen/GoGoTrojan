using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour {
    private bool isOver = false;
    public StatImageManager statImage;
    // Use this for initialization
    void Start () {

        statImage.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (isOver )
        {
            statImage.gameObject.SetActive(true);
            if (!Car.gameIsOver)
            {
                Car.gameIsOver = true;
            }
            return;
        }
        if (FindObjectOfType<PlayerController>().GetComponent<Car>().getHP() == 0)
        {
            statImage.isWin = false;
            isOver = true;
            return;

        }
        if (FindObjectsOfType<AIScript>().Length == 0)
        {
            statImage.isWin = true;
            isOver = true;
            return;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (!isOver)
        {
            if (other.GetComponent<AIScript>() != null) //lose
            {
                statImage.isWin = false;
                isOver = true;

            }
            else if (other.GetComponent<PlayerController>() != null) // win
            {
                statImage.isWin = true;
                isOver = true;

            }
        }
    }
    
    
}
