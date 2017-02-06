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
        if (other.CompareTag("Enemy") )
        {
            other.transform.root.GetComponent<AIScript>().speedDebuff(slowDownRatio);
        }
        else if(other.CompareTag("Player"))
        {
            other.transform.root.GetComponent<mover>().speedDebuff(slowDownRatio);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            other.transform.root.GetComponent<AIScript>().removeDebuff(slowDownRatio);
        }
        else if (other.CompareTag("Player"))
        {
            other.transform.root.GetComponent<mover>().removeDebuff(slowDownRatio);
        }
    }
}
