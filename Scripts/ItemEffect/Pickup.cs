using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private int spinSpeed = 1;
    private int itemCategory;//unused
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * spinSpeed);
    }
}
