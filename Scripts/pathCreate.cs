using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathCreate : MonoBehaviour
{
    private Transform[] pathArray;
    private void OnDrawGizmos()
    {
        pathArray = GetComponentsInChildren<Transform>();
        for (int i = 1; i < pathArray.Length; ++i)
        {
            Gizmos.DrawLine(pathArray[i-1].position,pathArray[i].position);
        }

        foreach(Transform obj in pathArray)
        {
            Gizmos.DrawWireSphere(obj.position,1);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
