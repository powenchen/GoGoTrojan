using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTransform : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeTransForm(Vector3 position, Quaternion rotation, float x, float y, float z)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = new Vector3(x, y, z);
    }
}
