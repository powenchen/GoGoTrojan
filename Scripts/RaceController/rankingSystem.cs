using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rankingSystem : MonoBehaviour {

	public carCheckPoint playerCar;
	public Text rankingText;

	// Use this for initialization
	void Start () {
		rankingText.text = "";
		InvokeRepeating ("GetRank", 0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetRank() {
		int rank = playerCar.GetCarPosition ();
		rankingText.text = AddOrdinal (rank);
	}

	// convert int to order. eg. 1 -> 1st, 2 -> 2nd.
	public static string AddOrdinal(int num)
	{
		if( num <= 0 ) return num.ToString();

		switch(num % 100)
		{
		case 11:
		case 12:
		case 13:
			return num + "th";
		}

		switch(num % 10)
		{
		case 1:
			return num + "st";
		case 2:
			return num + "nd";
		case 3:
			return num + "rd";
		default:
			return num + "th";
		}
	}
}
