using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {
    void OnDrawGizmos()
    {
        CarCheckPoint[] points = GetComponentsInChildren<CarCheckPoint>();
        for (int i = 0; i < points.Length; ++i)
        {
            points[i].dist = i;
        }


    }

	// Use this for initialization
	void Start () {

        CarCheckPoint[] points = GetComponentsInChildren<CarCheckPoint>();
        for (int i = 0; i < points.Length; ++i)
        {
            points[i].dist = i;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform[] GetCheckPointTrans()
    {
        CarCheckPoint[] points = GetComponentsInChildren<CarCheckPoint>();
        Transform[] ret = new Transform[points.Length];
        for (int i = 0; i < points.Length; ++i)
        {
            ret[i] = points[i].transform;
        }
        return ret;
    }
}
