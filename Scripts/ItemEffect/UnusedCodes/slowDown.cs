using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//unused code- obsolete
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
        if (other.CompareTag("Enemy") || other.CompareTag("Player")) // todo
        {
            other.transform.root.GetComponent<CarStatus>().speedDebuff();
        }
    }

}
