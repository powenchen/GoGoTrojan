using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private Transform[] pathArray;
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        pathArray = GetComponentsInChildren<Transform>();
        for (int i = 1; i < pathArray.Length-1; ++i)
        {
            Gizmos.DrawLine(pathArray[i].position,pathArray[i+1].position);
        }


        for (int i = 1; i < pathArray.Length ; ++i)
        {
            Gizmos.DrawWireSphere(pathArray[i].position,1);
        }
    }

    // Use this for initialization
    void Start () {
        pathArray = GetComponentsInChildren<Transform>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 startPositionOfPath()
    {

        pathArray = GetComponentsInChildren<Transform>();
        return pathArray[1].position;
    }

    public Quaternion startRotationOfPath()
    {

        pathArray = GetComponentsInChildren<Transform>();
        return Quaternion.FromToRotation(Vector3.forward, pathArray[2].position - pathArray[1].position);
    }

    public Vector3 endPositionOfPath()
    {

        pathArray = GetComponentsInChildren<Transform>();
        return pathArray[pathArray.Length - 2].position;
    }

    public Quaternion endRotationOfPath()
    {

        pathArray = GetComponentsInChildren<Transform>();
        return Quaternion.FromToRotation(Vector3.forward, pathArray[pathArray.Length - 2].position - pathArray[pathArray.Length - 3].position);
    }

    public Transform[] GetPathTransform()
    {
        Transform[] pathT = new Transform[pathArray.Length];
        for (int i = 0; i < pathArray.Length; ++i)
        {
            pathT[i] = pathArray[i].transform;
        }
        return pathT;
    }
}
