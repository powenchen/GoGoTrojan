using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private int coinValue = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	public int getCoinValue () {
        return coinValue;
	}
}
