using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : TrapWeapons
{
    private float lifetime = 2;// destroy in 3 sec
    public float damageValue = 0.2f;
	// Use this for initialization
	void Start () {
        GetComponentInParent<CarStatus>().isAttackedBy(attacker, damageValue);
        Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
