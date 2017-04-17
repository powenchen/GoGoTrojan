using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour {
    private PathManager path;
    private StartPointManager startPoint;
    private GoalManager goalPoint;
    public RankingSystem ranking;
    public GameObject myRoad;
    private float coinProb = 0.1f;
    private float itemProb = 0.05f;
    private float obsProb = 0.02f;
    private float coinMargin = 7;
    private float itemMargin = 7;

    public GameObject checkPointPrefab;
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;
    public GameObject lightPrefab;
    public GameObject itemPrefab;
    public Transform coinParent;
    public Transform itemParent;


    private Transform[] checkPointTrans;
    // Use this for initialization
    void OnDrawGizmos () {
        InitPositions();
    }

    private void Start()
    {
        InitPositions();
        InitChekpoints();
        InitPickup();
        InitStreetLights();
        InitObstacles();
    }

    private void InitChekpoints()
    {
        int dist = 0;
        CheckPointManager checkPointManager = GetComponentInChildren<CheckPointManager>();
        Transform[] pathTrans = GetComponentInChildren<PathManager>().GetPathTransform();
        for (int i = 1; i < pathTrans.Length - 1; ++i)
        {
            //distribute ${checkPointDensity} checkpoints between pathArray[i] & pathArray[i+1]
            //checkPointDensity = # of checkpoints between path[i] and path [i+1]
            float checkPointDensity = (pathTrans[i].position - pathTrans[i + 1].position).magnitude/6;
            for (int j = 0; j < checkPointDensity; ++j)
            {
                Vector3 spawnPos = (checkPointDensity - j) * pathTrans[i].position / (checkPointDensity) +
                    (j) * pathTrans[i+1].position / (checkPointDensity);
                Quaternion spawnRot = Quaternion.FromToRotation(Vector3.forward, pathTrans[i+1].position - pathTrans[i].position);
                GameObject checkPointObj = Instantiate(checkPointPrefab, spawnPos, spawnRot, checkPointManager.transform);
                checkPointObj.GetComponent<CarCheckPoint>().ranking = GetComponentInParent<CourseManager>().ranking;
                checkPointObj.GetComponent<CarCheckPoint>().dist = (dist++);
            }
        }
        Debug.Log("There are " + dist + "checkpoints");
        checkPointTrans = GetComponentInChildren<CheckPointManager>().GetCheckPointTrans();
    } 

    void InitPositions()
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

    public Quaternion GetStartRotation()
    {
        path = GetComponentInChildren<PathManager>();
        return path.startRotationOfPath();
    }

    public Vector3[] GetStartPositions()
    {
        startPoint = GetComponentInChildren<StartPointManager>();
        if (startPoint == null)
        {
            Debug.Log("there are no starts in " + name);
        }
        return startPoint.startPositions();
    }

    public PathManager GetPath()
    {
        return path;
    }

    void InitPickup()
    {
        InitCoins();
        InitItems();
    }

    void InitCoins()
    {
        for (int i = 10; i < checkPointTrans.Length; ++i)
        {
            if (Random.value < coinProb)
            {
                int rng = Random.Range(-1, 2);
                Vector3 offset = (rng) * coinMargin * checkPointTrans[i].right ;
                for (int j = i; j < i + 10 && j< checkPointTrans.Length; ++j)
                {
                    Vector3 pos = checkPointTrans[j].position + offset + checkPointTrans[j].up;
                    Quaternion rot = checkPointTrans[j].rotation;
                    Instantiate(coinPrefab, pos, rot, coinParent);
                }
                i = i+10;
            }
        }
    }

    void InitItems()
    {
        for (int i = 10; i < checkPointTrans.Length; ++i)
        {
            if (Random.value < itemProb)
            {
                int rng = Random.Range(0, 2);
                Vector3 offset = (rng - 0.5f) * itemMargin * checkPointTrans[i].right;
                Vector3 pos = checkPointTrans[i].position + offset + checkPointTrans[i].up;
                Quaternion rot = checkPointTrans[i].rotation;
                Instantiate(itemPrefab, pos, rot, itemParent);
            }
            //else if(Random.value < 2*itemProb)
        }

    }

    void InitStreetLights()
    {
        float lightMargin = 10;
        for (int i = 10; i < checkPointTrans.Length; i+=20)
        {
            Vector3 offset = lightMargin * checkPointTrans[i].right;
            Vector3 pos = checkPointTrans[i].position + offset;
            Quaternion rot = Quaternion.Euler(checkPointTrans[i].rotation.eulerAngles + new Vector3(-90,0,180));
            Instantiate(lightPrefab, pos, rot, itemParent);

            if (i + 10 < checkPointTrans.Length)
            {
                offset = lightMargin * checkPointTrans[i + 10].right * -1;
                pos = checkPointTrans[i + 10].position + offset;
                rot = Quaternion.Euler(checkPointTrans[i + 10].rotation.eulerAngles + new Vector3(-90, 0, 0));
                Instantiate(lightPrefab, pos, rot, itemParent);
            }
        }

    }

    void InitObstacles()
    {
        for (int i = 10; i < checkPointTrans.Length; i++)
        {
            if (Random.value < obsProb)
            {
                Vector3 pos = checkPointTrans[i].position;
                Quaternion rot = checkPointTrans[i].rotation;// Quaternion.Euler(checkPointTrans[i].rotation.eulerAngles + new Vector3(-90, 0, 180));
                Instantiate(obstaclePrefab, pos, rot, transform);
            }
            
        }

    }

}
