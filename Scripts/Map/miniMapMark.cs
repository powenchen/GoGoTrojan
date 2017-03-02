using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapMark : MonoBehaviour {
    public Transform MyCar;   
    

    // Use this for initialization
    private void OnDrawGizmos()
    {
        transform.position =
            new Vector3(MyCar.position.x, transform.position.y, MyCar.position.z);
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position =
            new Vector3(MyCar.position.x, transform.position.y, MyCar.position.z);
    }
}
