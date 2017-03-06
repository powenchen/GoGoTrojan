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

    private Car[] racers;

    public int[] carinitDebug;
    public int courseinitDebug;
    public int skyBoxDebugInit;

    public bool debugFlag = false;
    // Use this for initialization; used for debug
    void OnDrawGizmos () {
        if (debugFlag)
        {
            Car[] racers = new Car[carinitDebug.Length];
            for (int i = 0; i < carinitDebug.Length; ++i)
            {
                Vector3 position = courseList[courseinitDebug].getStartPositions()[i+1];
                Quaternion rotation = Quaternion.Euler(0, courseList[courseinitDebug].getStartRotation().eulerAngles.y,0);
                //Debug.Log("init rotation = " + courseList[courseinitDebug].getStartRotation().eulerAngles.ToString());
                Car car = Instantiate(characterList[carinitDebug[i]], position, rotation).GetComponent<Car>();
                miniMapMark mark = Instantiate(miniMarkList[carinitDebug[i]], position, rotation).GetComponent<miniMapMark>();
                racers[i] = car;
                mark.transform.parent = miniMap.transform;
                mark.transform.localPosition = new Vector3(0, 0, 0);
                mark.MyCar = car.transform;

                if (car.GetComponent<TimeStopSkill>() != null)
                {
                    car.GetComponent<TimeStopSkill>().backGroundCamera = backGroundCamera;

                    car.GetComponent<TimeStopSkill>().mainCamera = mainCamera;

                }

                if (i == 0)//player
                {
                    mainCamera.GetComponent<UnityStandardAssets.Utility.SmoothFollow>().setTarget(car.transform);
                    backGroundCamera.GetComponent<UnityStandardAssets.Utility.SmoothFollow>().setTarget(car.transform);
                    setSkyBox(car, skyBoxDebugInit);
                    if (isNight(skyBoxDebugInit))
                    {
                        dayLight.enabled = false;
                        nightLight.enabled = true;
                    }
                    else
                    {
                        dayLight.enabled = true;
                        nightLight.enabled = true;
                    }

                    DestroyImmediate(car.gameObject.GetComponent<AIScript>());
                    miniMap.GetComponent<MiniMapManager>().setCamFolowee(car.transform);
                }
                else
                {

                    DestroyImmediate(car.gameObject.GetComponent<PlayerController>());
                    foreach (Camera cam in car.gameObject.GetComponentsInChildren<Camera>())
                    {
                        cam.gameObject.SetActive(false);
                    }
                    car.gameObject.GetComponent<AIScript>().path = courseList[courseinitDebug].getPath();
                }

            }

            //there are carinitDebug.Length cars 
            foreach (Car car in racers)
            {
                if (car.gameObject.GetComponent<TimeStopSkill>() != null)
                {
                    car.gameObject.GetComponent<TimeStopSkill>().enemies = new GameObject[carinitDebug.Length - 1];
                    int idx = 0;
                    for (int i = 0; i < carinitDebug.Length; ++i)
                    {
                        if (!racers[i].Equals(car))
                        {
                            car.gameObject.GetComponent<TimeStopSkill>().enemies[idx++] = racers[i].gameObject;
                        }
                    }
                }
            }
            debugFlag = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setRaceCourse(int courseID)
    {
        foreach (Car racer in racers)
        {
            if (racer != null && racer.gameObject.GetComponent<AIScript>() !=null)
            {
                racer.gameObject.GetComponent<AIScript>().path = courseList[courseID].gameObject.GetComponent<PathManager>();
            }
        }

        skyBoxIdx = courseID;

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

    public Car initCharacter(int charID, int skillID, float maxHP, float maxMP, float attackPower)
    {
        Car character = Instantiate(characterList[charID]).GetComponent<Car>();
        //TODO: set character.position to some point relate to course
        //TODO: instantiate minimap mark
        //TODO: set player mark index in minimap manager and set followee

        setSkyBox(character, skyBoxIdx);
        if (isNight(skyBoxIdx))
        {
            dayLight.enabled = false;
            nightLight.enabled = true;
        }
        else
        {
            dayLight.enabled = true;
            nightLight.enabled = false;
        }
        insertIntoRacersList(character);
        return character;
    }

    public Car initEnemy(int charID, float maxHP, float maxMP, float attackPower)
    {

        Car character = Instantiate(characterList[charID]).GetComponent<Car>();
        //TODO: set character.position to some point relate to course
        //TODO: destroy prefab's camera
        //TODO: instantiate minimap mark


        insertIntoRacersList(character);
        return character;
    }

    private void insertIntoRacersList(Car newRacer)
    {
        for (int i = 0; i < racers.Length; ++i)
        {
            if (racers[i] == null)
            {
                racers[i] = newRacer;
                return;
            }
        }

    }


    private void setSkyBox(Car character, int skyBoxID)
    {
        foreach (Camera cam in character.gameObject.GetComponentsInChildren<Camera>())
        {
            if (cam.GetComponent<Skybox>() != null)
            {
                cam.GetComponent<Skybox>().material = skyBoxes[skyBoxID];
            }
        }
    }

    private bool isNight(int skyBoxID)
    {
        if (skyBoxes[skyBoxID].name.StartsWith("Sunny"))
        {
            return false;
        }
        return true;
    }
}
