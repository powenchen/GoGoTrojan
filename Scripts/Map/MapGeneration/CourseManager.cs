using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour {
    private PathManager path;
    private StartPointManager startPoint;
    private GoalManager goalPoint;
    public RankingSystem ranking;
    
    // Use this for initialization
    void OnDrawGizmos () {
        initPositions();
    }

    private void Start()
    {
        initPositions();
    }

    void initPositions()
    {
        path = GetComponentInChildren<PathManager>();
        startPoint = GetComponentInChildren<StartPointManager>();
        goalPoint = GetComponentInChildren<GoalManager>();
        if (startPoint != null && path != null && goalPoint != null)
        {
            Quaternion startRotation = Quaternion.Euler(
                new Vector3(
                    0,
                    path.startRotationOfPath().eulerAngles.y,
                    0
                    )
                );
            Quaternion goalRotation = Quaternion.Euler(
                new Vector3(
                    0,
                    path.endRotationOfPath().eulerAngles.y,
                    0
                    )
                );
            startPoint.transform.position = path.startPositionOfPath();
            startPoint.transform.rotation = startRotation;
            goalPoint.transform.position = path.endPositionOfPath();
            goalPoint.transform.rotation = goalRotation;
            return;
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
