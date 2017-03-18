using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour {
    private PathManager path;
    private StartPointManager startPoint;
    public GameObject startPrefab;
    // Use this for initialization
    void Start () {
        Debug.Log("in start");
        startPoint = GetComponentInChildren<StartPointManager>();
        path = GetComponentInChildren<PathManager>();
        if (startPoint == null)
        {
            Quaternion startRotation = Quaternion.Euler(
                new Vector3(
                    0,
                    path.startRotationOfPath().eulerAngles.y,
                    0
                    )
                );
            Instantiate(startPrefab, path.startPositionOfPath(), startRotation).transform.parent = transform;

            startPoint = GetComponentInChildren<StartPointManager>();
        }
        //
        if (startPoint == null)
        {
            Debug.Log("course has no start point");
        }

    }
        
	// Update is called once per frame
	void Update () {
		
	}

    public Quaternion getStartRotation()
    {
        path = GetComponentInChildren<PathManager>();
        return path.startRotationOfPath();
    }

    public Vector3[] getStartPositions()
    {
        startPoint = GetComponentInChildren<StartPointManager>();
        if (startPoint == null)
        {
            Debug.Log("there are no starts in " + name);
        }
        return startPoint.startPositions();
    }

    public PathManager getPath()
    {
        return path;
    }
}
