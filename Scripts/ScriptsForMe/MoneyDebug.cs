using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDebug : MonoBehaviour {
    public int coins;
    public bool debug = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (debug)
        {
            StaticVariables.SetTotalCoins( coins);
            debug = false;
        }
    }
}
