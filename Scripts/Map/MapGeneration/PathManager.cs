using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private Transform[] pathArray;
    public GameObject checkPointPrefab;
    public CheckPointManager checkPointManager;
    private int checkPointDensity = 5;
    private void OnDrawGizmos()
    {
        if (checkPointManager == null)
        {
            Debug.Log(name + " is initializing");
            //GetComponentInSibling
            checkPointManager = GetComponentInParent<CourseManager>().GetComponentInChildren<CheckPointManager>();
        }

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
        int dist = 0;
        pathArray = GetComponentsInChildren<Transform>();
        for (int i = 1; i < pathArray.Length - 1; ++i)
        {
            //distribute ${checkPointDensity} checkpoints between pathArray[i] & pathArray[i+1]
            for (int j = 0; j < checkPointDensity; ++j)
            {
                Vector3 spawnPos = (checkPointDensity - j) * pathArray[i].transform.position / (checkPointDensity) +
                    (j) * pathArray[i + 1].transform.position / (checkPointDensity);
                Quaternion spawnRot = Quaternion.FromToRotation(Vector3.forward, pathArray[i+1].position - pathArray[i].position);
                GameObject checkPointObj = Instantiate(checkPointPrefab, spawnPos, spawnRot, checkPointManager.transform);
                checkPointObj.GetComponent<CarCheckPoint>().ranking = GetComponentInParent<CourseManager>().ranking;
                checkPointObj.GetComponent<CarCheckPoint>().dist = (dist++);
            }
        }
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
}
