using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsGainedText : MonoBehaviour {
	public int debug = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (debug != -1) {
			StaticVariables.coinNumber = debug;
		}
		GetComponent<Text>().text = "Coins Gained: " + StaticVariables.coinNumber;
	}
}
