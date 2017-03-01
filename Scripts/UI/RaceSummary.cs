using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSummary : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.GetInt ("TotalCoins");
		PlayerPrefs.GetString("Ranking");
		PlayerPrefs.GetString ("Time");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
