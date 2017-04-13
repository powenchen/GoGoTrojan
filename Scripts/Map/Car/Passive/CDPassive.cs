using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDPassive : Passive
{

	// Use this for initialization
	void Start () {
        carStatus = GetComponent<CarStatus>();
        carStatus.skillCDModifier = Mathf.Max(0.01f, carStatus.skillCDModifier-0.5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
