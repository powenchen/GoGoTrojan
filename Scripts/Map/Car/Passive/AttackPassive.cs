using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPassive : Passive
{

	// Use this for initialization
	void Start () {
        carStatus = GetComponent<CarStatus>();
        carStatus.attackModifier += 1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
