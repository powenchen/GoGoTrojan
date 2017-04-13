using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPassive : Passive
{

	// Use this for initialization
	void Start () {
        carStatus = GetComponent<CarStatus>();
        carStatus.receivedExpModifier += 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
