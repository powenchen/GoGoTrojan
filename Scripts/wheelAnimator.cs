using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelAnimator : MonoBehaviour {
    private WheelCollider wheel;
	// Use this for initialization
	void Start () {
        wheel = GetComponent<WheelCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * Time.deltaTime * wheel.rpm / 60f * 360f);
        Debug.Log(wheel.rpm / 60f * 360f * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(90,270, -wheel.steerAngle);
    }
}
