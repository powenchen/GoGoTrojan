using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour {
    private bool isOver = false;
    public StatImageManager statImage;
    public bool debugIsOver = false;
    public bool setProgressIsOver = false;
    // Use this for initialization
    void Start () {

        statImage.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (isOver )
        {
            if (StaticVariables.ranking == 1)
            {
                if (!setProgressIsOver)
                {
                    setProgressIsOver = true;
                    Debug.Log("first clear debug = " + StaticVariables.GetProgress() + "; " + StaticVariables.mapID);
                    StaticVariables.firstClear = ((StaticVariables.GetProgress() != 6) && (StaticVariables.mapID == 6));
                    StaticVariables.SetProgress(StaticVariables.mapID);
                }
            }
            if (!debugIsOver)
            {
                debugIsOver = true;
                if (StaticVariables.ranking == 1)
                {
                    if (FindObjectOfType<MapGen>().expWinDEBUG != -1)
                    {
                        StaticVariables.expGained += FindObjectOfType<MapGen>().expWinDEBUG;
                    }

                    if (FindObjectOfType<MapGen>().moneyWinDEBUG != -1)
                    {
                        StaticVariables.coinNumber += (int)FindObjectOfType<MapGen>().moneyWinDEBUG;
                    }
                }
                else
                {
                    if (FindObjectOfType<MapGen>().expLoseDEBUG != -1)
                    {
                        StaticVariables.expGained += FindObjectOfType<MapGen>().expLoseDEBUG;
                    }

                    if (FindObjectOfType<MapGen>().moneyLoseDEBUG != -1)
                    {
                        StaticVariables.coinNumber += (int)FindObjectOfType<MapGen>().moneyLoseDEBUG;
                    }
                }
            }
            statImage.gameObject.SetActive(true);
            if (!StaticVariables.gameIsOver)
            {
                StaticVariables.gameIsOver = true;
            }
            return;
        }
        if (FindObjectOfType<PlayerController>() == null)
        {
            statImage.isWin = false;
            isOver = true;
            StaticVariables.ranking = 999;
            return;

        }
        if (FindObjectsOfType<AIScript>().Length == 0)
        {
            statImage.isWin = true;
            isOver = true;
            StaticVariables.ranking = 1;
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
