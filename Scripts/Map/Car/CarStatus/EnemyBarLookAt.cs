using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarLookAt : MonoBehaviour {

	public Transform mainCameraTrans;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		this.transform.LookAt (mainCameraTrans.position);
	}
}
