using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowDown : MonoBehaviour {
    public float slowDownRatio = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("triggered!!! tag = "+other.tag);
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            other.transform.root.GetComponent<Car>().speedDebuff(slowDownRatio);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")||other.CompareTag("Player"))
        {
            other.transform.root.GetComponent<Car>().removeDebuff(slowDownRatio);
        }
    }
}
