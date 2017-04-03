using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRecoverPassive : Passive
{
    private float passivePoint = 3;
	// Use this for initialization
	void Start () {
        carStatus = GetComponent<CarStatus>();
        carStatus.hpRecover += passivePoint;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
