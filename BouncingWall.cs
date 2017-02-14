using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingWall : MonoBehaviour {
    private Rigidbody rb;
    public float torque = 10000;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 25;
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.CompareTag("Wall"))
            {
                Vector3 localContactPoint = transform.InverseTransformPoint(contact.point);
                Vector3 localContactNormal = transform.InverseTransformDirection(contact.normal);
                Debug.Log("normal = " + localContactNormal.ToString());
                if (localContactPoint.x > 0)
                {
                    rb.AddTorque(-transform.up * torque * Mathf.Abs(localContactNormal.z), ForceMode.Acceleration);
                }
                else
                {
                    rb.AddTorque(transform.up * torque * Mathf.Abs(localContactNormal.z), ForceMode.Acceleration);
                }

            }
            else
                Debug.Log("bounced with " + contact.otherCollider.tag);
        }

    }
}
