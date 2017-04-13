using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsGainedText : MonoBehaviour {
	public int debug = -1;
    private float speed = 100;
    public float totalTime = 3;
    public float myCoinNumber = 0;
	// Use this for initialization
	void Start () {
        myCoinNumber = 0;
        if (debug != -1)
        {
            StaticVariables.coinNumber = debug;
        }

        speed = StaticVariables.coinNumber / totalTime;
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Coins Gained: "+ Mathf.RoundToInt(myCoinNumber);
        //Debug.Log("myCoinNumber = " + myCoinNumber);
        if (myCoinNumber < StaticVariables.coinNumber)
        {
            float coinsGrow = speed * Time.deltaTime;
            myCoinNumber = Mathf.Min(myCoinNumber + coinsGrow, StaticVariables.coinNumber);
        }
    }


}
