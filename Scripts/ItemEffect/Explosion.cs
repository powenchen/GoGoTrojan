using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public GameObject explosion;
	public float radius = 5.0f;
	public float power = 10000000.0f;
	public float uplift = 300000.0f;
    //private Transform explosionLocation;

	Transform topLevelParent ( Transform childTransform ){
		if (childTransform.parent != null) {
			return topLevelParent (childTransform.parent);
		} else {
			return childTransform;
		}

	}

	// Use this for initialization
	void Start () {

	}
	
	void OnTriggerEnter(Collider other)
	{
		Vector3 explosionPos = transform.position;
        Quaternion explosionRotation = transform.rotation;
		Transform topLevelTransform = topLevelParent (other.GetComponent<Transform> ());
		Rigidbody rb = topLevelTransform.GetComponent<Rigidbody>();
		GameObject topLevelObject = topLevelTransform.gameObject;
        Debug.Log(other.name);

        if (rb != null && topLevelObject.layer == 9)
        { //Car layer
            Debug.Log(rb.name);
            //Debug.Log ("Hey!");	
			Instantiate(explosion, other.transform.position, other.transform.rotation);
			topLevelObject.SendMessage("TakeDamage", 20);
            rb.AddExplosionForce(power, explosionPos, radius, uplift);
            StartCoroutine(SetStableAfterTime(1, topLevelObject, explosionPos, explosionRotation));
            GetComponent<Renderer>().enabled = false;
            

        }

	}

    IEnumerator SetStableAfterTime(float time, GameObject topLevelObject, Vector3 explosionPos, Quaternion explosionRotation)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        topLevelObject.transform.position = explosionPos;
        topLevelObject.transform.rotation = explosionRotation;
        Rigidbody rb = topLevelObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        
        gameObject.SetActive(false);
        Destroy(this);
    }
}
