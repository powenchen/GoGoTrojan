using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public Camera mainCamera;
    public Camera backGroundCamera;
    public const int stageNum = 2;
    public const int maxRacerNum = 4;
    public GameObject miniMap;
    public GameObject[] characterList;
    public GameObject[] miniMarkList;

    public CourseManager[] courseList = new CourseManager[stageNum];
    public Material[] skyBoxes = new Material[stageNum];
    public Light dayLight, nightLight;
    private int skyBoxIdx;

    private Car[] racers = new Car[maxRacerNum];
    private int racersLength = 0;

    // for debug
    public int[] carinitDebug;
    public int courseinitDebug;
    public int skyBoxInitDebug;
    private float maxHPDebug = 100;
    private float maxMPDebug = 100;
    private float maxATKDebug = 100;
    
    public bool debugFlag = false;


    // Use this for initialization; used for debug
    void OnDrawGizmos () {
        if (debugFlag)
        {
            racersLength = 0;
            skyBoxIdx = skyBoxInitDebug;
            setCourse(courseinitDebug);
            for (int i = 0; i < carinitDebug.Length; ++i)
            {
                // set car(i==0) as player
                initCharacter(courseinitDebug, carinitDebug[i], maxHPDebug, maxMPDebug, maxATKDebug, i, (i == 0));
            }
            //there are carinitDebug.Length cars; init them in time stop skill users' enemy lists 
            initTimeStopskillUsers();

            debugFlag = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initTimeStopskillUsers()
    {
        foreach (Car car in racers)
        {
            if (car != null)
            {
                if (car.gameObject.GetComponent<TimeStopSkill>() != null)
                {
                    if (racersLength >= 1)
                    {
                        car.gameObject.GetComponent<TimeStopSkill>().enemies = new GameObject[racersLength - 1];
                    }
                    int idx = 0;
                    for (int i = 0; i < racers.Length; ++i)
                    {
                        if (racers[i]!=null && !racers[i].Equals(car))
                        {
                            car.gameObject.GetComponent<TimeStopSkill>().enemies[idx++] = racers[i].gameObject;
                        }
                    }
                }
            }
        }
    }
    public void setCourse(int courseID)
    {
        
        for (int i = 0; i < courseList.Length; ++i)
        {
            if (i == courseID)
            {
                courseList[i].gameObject.SetActive(true);
            }
            else
            {
                courseList[i].gameObject.SetActive(false);
            }

        }
    }

    public void initCharacter(int courseID ,int charID, float maxHP, float maxMP, float attackPower, int racerNum, bool isPlayer = false)
    {
        Vector3 position = courseList[courseID].getStartPositions()[racerNum + 1];
        Quaternion rotation = Quaternion.Euler(0, courseList[courseID].getStartRotation().eulerAngles.y, 0);
        PathManager path = courseList[courseID].getPath();

        Car car = Instantiate(characterList[charID], position, rotation).GetComponent<Car>();
        miniMapMark mark = Instantiate(miniMarkList[charID], position, rotation).GetComponent<miniMapMark>();
        mark.transform.parent = miniMap.transform;
        racers[racerNum] = car;
        racersLength++;
        mark.transform.localPosition = new Vector3(0, 0, 0);
        mark.MyCar = car.transform;

        if (car.GetComponent<TimeStopSkill>() != null)
        {
            car.GetComponent<TimeStopSkill>().backGroundCamera = backGroundCamera;
            car.GetComponent<TimeStopSkill>().mainCamera = mainCamera;

        }

        if (isPlayer)//player
        {
            setCamera(car, skyBoxIdx);
            

            DestroyImmediate(car.gameObject.GetComponent<AIScript>()); // delete player car's ai script
            miniMap.GetComponent<MiniMapManager>().setCamFolowee(mark.transform);
        }
        else
        {

            DestroyImmediate(car.gameObject.GetComponent<PlayerController>());// delete ai car's player script
            foreach (Camera cam in car.gameObject.GetComponentsInChildren<Camera>())
            {
                // disable ai car's camera
                cam.gameObject.SetActive(false);
            }
            car.gameObject.GetComponent<AIScript>().path = path;
        }
    }



    private void setCamera(Car car, int skyBoxID)
    {
        mainCamera.transform.position = car.transform.position + new Vector3(0, 2, -6);
        backGroundCamera.transform.position = car.transform.position + new Vector3(0, 2, -6);
        mainCamera.transform.rotation = car.transform.rotation;
        backGroundCamera.transform.rotation = car.transform.rotation;

        mainCamera.GetComponent<UnityStandardAssets.Utility.SmoothFollow>().setTarget(car.transform);
        backGroundCamera.GetComponent<UnityStandardAssets.Utility.SmoothFollow>().setTarget(car.transform);
        if (backGroundCamera.GetComponent<Skybox>() != null)
        {
            backGroundCamera.GetComponent<Skybox>().material = skyBoxes[skyBoxID];
        }

        if (isNight(skyBoxID))
        {
            dayLight.enabled = false;
            nightLight.enabled = true;
        }
        else
        {
            dayLight.enabled = true;
            nightLight.enabled = true;
        }
    }

    private bool isNight(int skyBoxID)
    {
        if (skyBoxes[skyBoxID].name.StartsWith("Sunny"))
        {
            //Debug.Log("It's  not night for skybox(" + skyBoxes[skyBoxID].name + ")");
            return false;
        }

        //Debug.Log("It's night for skybox(" + skyBoxes[skyBoxID].name + ")");
        return true;
    }
}
