using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public float rotateAngles = 720.0f;
    public float rotateTime = 1.0f;
    public float lifetime = 2.0f;

    // Use this for initialization
    void Start () {
		StartCoroutine(rotateAngleInSecond(transform, Vector3.up * rotateAngles, rotateTime));

        Destroy(gameObject.GetComponent<Rotate>(), lifetime);
    }
	
	// Update is called once per frame
	IEnumerator rotateAngleInSecond (Transform transform, Vector3 byAngles, float inTime) {
		

		//Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles) ;
		//Debug.Log ("toAngle: " + toAngle);
		float rotateFrames = inTime / Time.deltaTime;
		Vector3 step = byAngles / rotateFrames;
		for(int t = 0 ; t < rotateFrames ; t++)
		{
			transform.Rotate(step) ;
			yield return null ;
		}

	}
}
