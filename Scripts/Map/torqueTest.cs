using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torqueTest : MonoBehaviour {
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(" apply torque = " + (20000 * Input.GetAxis("Horizontal") * (new Vector3(0, 1, 0))).ToString());
        rb.AddTorque(20000 * Input.GetAxis("Horizontal")* (new Vector3(0,1,0)), ForceMode.Acceleration);
    }
}
