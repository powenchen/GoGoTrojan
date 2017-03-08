using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private Transform[] pathArray;
    private void OnDrawGizmos()
    {
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
        //Vector3 x = new Vector3(1, 0, 0), z = new Vector3(0, 0, 1), o = new Vector3(0, 0, 0);
        //Quaternion x_p = Quaternion.FromToRotation(x, o), x_n = Quaternion.FromToRotation(o, x);
        //Quaternion z_p = Quaternion.FromToRotation(z, o), z_n = Quaternion.FromToRotation(o,z);
        //Debug.Log("x_p" + x_p.eulerAngles.ToString() + "x_n" + x_n.eulerAngles.ToString() + "z_p" + z_p.eulerAngles.ToString() + "z_n" + z_n.eulerAngles.ToString());
        return Quaternion.FromToRotation(Vector3.forward, pathArray[2].position - pathArray[1].position);
    }

    public Vector3 endPositionOfPath()
    {

        pathArray = GetComponentsInChildren<Transform>();
        return pathArray[pathArray.Length - 1].position;
    }

    public Quaternion endRotationOfPath()
    {

        pathArray = GetComponentsInChildren<Transform>();
        return Quaternion.FromToRotation(Vector3.up, pathArray[pathArray.Length - 1].position - pathArray[pathArray.Length - 2].position);
    }
}
