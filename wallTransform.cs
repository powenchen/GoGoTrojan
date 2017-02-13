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

    public void changeTransForm(Vector3 position, Quaternion rotation, float scale, float width)
    {
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = new Vector3(scale, transform.localScale.y, width);
    }
}
