using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform followee;
    
	// Use this for initialization
	void Start () {
        transform.position = 
            new Vector3 (
            followee.position.x,
            transform.position.y,
            followee.position.z
            );

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position =
            new Vector3(
            followee.position.x,
            transform.position.y,
            followee.position.z
            );

    }
}
