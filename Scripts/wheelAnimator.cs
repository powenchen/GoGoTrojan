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
		transform.Rotate(Vector3.down * Time.deltaTime * wheel.rpm / 60f * 360f);
    }
}
