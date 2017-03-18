using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByTrap : MonoBehaviour {

    public float rotateAngle = 720.0f;
    public float rotateTime = 1.0f;
	private bool isActive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Transform topLevelParent(Transform childTransform)
    {
        if (childTransform.parent != null)
        {
            return topLevelParent(childTransform.parent);
        }
        else
        {
            return childTransform;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 posBeforeSpin;
        //Quaternion rotationBeforeSpin;
        Transform topLevelTransform = topLevelParent(other.GetComponent<Transform>());
        Rigidbody rb = topLevelTransform.GetComponent<Rigidbody>();
        GameObject topLevelObject = topLevelTransform.gameObject;

        Debug.Log(other.name);

        if (rb != null && topLevelObject.layer == 9 && isActive)
        { //Car layer
            Debug.Log(rb.name);
            //Debug.Log ("Hey!");	
            //StartCoroutine(spinForTime(rotateTime, topLevelTransform));
			isActive = false;
			StartCoroutine(rotateAngleInSecond(topLevelTransform, Vector3.up * rotateAngle, rotateTime));
			GetComponent<Renderer>().enabled = false;
        }

    }

    

	IEnumerator rotateAngleInSecond (Transform transform, Vector3 byAngles, float inTime) {
		
		float rotateFrames = inTime / Time.deltaTime;
		Debug.Log ("rotateFrames: " + rotateFrames);
		Debug.Log ("Time.deltaTime: " + Time.deltaTime);
		Vector3 step = byAngles / rotateFrames;
		Debug.Log ("step: " + step);
		//Collider collider = transform.GetComponent<Collider> ();
		//collider.enabled = false;
		for(int t = 0 ; t < rotateFrames ; t++)
		{
			transform.Rotate(step) ;
			yield return new WaitForEndOfFrame();
		}
		//collider.enabled = true;
		gameObject.SetActive(false);
		Destroy(this);
	}

}

