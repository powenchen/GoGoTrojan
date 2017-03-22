using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : TrapWeapons
{
    private float lifetime = 3;// destroy in 3 sec
    private float damageValue = 20;
	// Use this for initialization
	void Start () {
        GetComponentInParent<CarStatus>().isAttackedBy(attacker, damageValue);
        Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
