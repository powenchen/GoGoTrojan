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
    private MiniMapMark[] marks = new MiniMapMark[maxRacerNum];
    public int racersLength = 0;

    /*public int[] carinitDebug;
    public int courseinitDebug;
    public int skyBoxInitDebug;
    private float maxHPDebug = 100;
    private float maxMPDebug = 100;
    private float maxATKDebug = 100;
    
    public bool debugFlag = false;*/

    public itemBtnOnClick itemBtn;
    public moveBtnOnClick moveBtn;
    public StatImageManager statImage;
    private float basicTopSpeed = 27;

    private AudioSource audio;



    // for debug
    [Range(-1,6)]
    public int courseDebug = -1;
    public Vector2[] carListsForDebug;
    public Vector2[] charListsForDebug;
    public float moneyWinDEBUG = -1;
    public float moneyLoseDEBUG = -1;
    public float expWinDEBUG = -1;
    public float expLoseDEBUG = -1;

    void Start()
    {
        audio = GetComponentInChildren<AudioSource>();
        resetMap();
        StaticVariables.ResetVariables();
        skyBoxIdx = StaticVariables.skyBoxID;
        if (courseDebug >= 0 && courseDebug < 7)
        {
            StaticVariables.mapID = courseDebug;
        }
        setCourse(StaticVariables.mapID);

        //TODO - generate real code

        initCharacter(StaticVariables.mapID, StaticVariables.carID, StaticVariables.characterID, 0, true);

        for (int i = 0; i < carListsForDebug.Length; ++i)
        {
            initCharacter(StaticVariables.mapID, (int)carListsForDebug[i].x, (int)charListsForDebug[i].x, i+1, false, (int)carListsForDebug[i].y, (int)charListsForDebug[i].y);
        }

        
        

        //there are carinitDebug.Length cars; init them in time stop skill users' enemy lists 
        initTimeStopskillUsers();
        
    }
    // Update is called once per frame
    void Update () {
        if (StaticVariables.musicStartFlag && !audio.isPlaying)
        {
            audio.Play();
        }
        racersLength = countRacers();
	}

    public void resetMap()
    {
        audio.Stop();
        foreach (Car car in racers)
        {
            if (car != null)
            {
                Destroy(car.gameObject);
            }
        }
        foreach (MiniMapMark mark in marks)
        {
            if (mark != null)
            {
                Destroy(mark.gameObject);
            }
        }
        racersLength = 0;
    }

    public int countRacers()
    {
        int ret = 0;
        foreach (Car car in racers)
        {
            if (car != null)
            {
                ret++;
            }
        }
        return ret;
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
        //disable all course except for 'courseID' course
        for (int i = 0; i < courseList.Length; ++i)
        {
            courseList[i].gameObject.SetActive(false);
            courseList[i].myRoad.SetActive(false);
        }

        courseList[courseID].gameObject.SetActive(true);
        courseList[courseID].myRoad.SetActive(true);
    }

    public void initCharacter(int courseID , int carId,int charID, int racerNum, bool isPlayer, int carLV = -1, int charLV = -1)
    {
        Debug.Log("generating char = " + carId);
        Vector3 position = courseList[courseID].GetStartPositions()[racerNum + 1];
        Quaternion rotation = Quaternion.Euler(0, courseList[courseID].GetStartRotation().eulerAngles.y, 0);
        PathManager path = courseList[courseID].GetPath();

        Car car = Instantiate(characterList[carId], position, rotation).GetComponent<Car>();
        car.myCharID = charID;
        //car.GetComponentInChildren<EnemyBarLookAt>().mainCameraTrans = backGroundCamera.transform;
        CarStatus carStatus = car.GetComponent<CarStatus>();
        MiniMapMark mark = Instantiate(miniMarkList[carId], position, rotation).GetComponent<MiniMapMark>();
        //InitCarStatus(carStatus, carId, charID);
        if (charID != 0)
        {
            DestroyImmediate(car.GetComponent<FlameSkill>());
        }
        if (charID != 1)
        {
            DestroyImmediate(car.GetComponent<CoinAttackSkill>());
        }
        if (charID != 2)
        {
            DestroyImmediate(car.GetComponent<ShieldSkill>());
        }
        if (charID != 3)
        {
            DestroyImmediate(car.GetComponent<TimeStopSkill>());
        }
        if (charID != 4)
        {
            DestroyImmediate(car.GetComponent<N2OSkill>());
        }
        if (charID != 5)
        {
            DestroyImmediate(car.GetComponent<SpearSkill>());
        }


        //init car params
        // return [hp, mp, speed,CD, attack, defense]
        if (isPlayer)
        {
            carLV = (int)StaticVariables.GetCurrentCarLevel(carId);
            charLV = (int)StaticVariables.GetCurrentCharLevel(charID);
        }
        List<float> carAbility = StaticVariables.GetCurrentCarAttribute(carId,carLV);
        List<float> charAbility = StaticVariables.GetCurrentCharAttribute(charID,charLV);
        carStatus.HPInitialize(carAbility[0]+charAbility[0]);
        carStatus.MPInitialize(carAbility[1] + charAbility[1]);
        carStatus.setTopSpeed(basicTopSpeed * (1 + ((carAbility[2] + charAbility[2] )/ 1000f)));//speed = 100pt = 1.1times
        carStatus.skillCDInitialize(carAbility[3] + charAbility[3]);
        carStatus.attackInitialize(carAbility[4] + charAbility[4]);
        carStatus.defenseBase = carAbility[5] + charAbility[5];
        if (isPlayer)//set cards on player
        {
            for (int i = 0; i < StaticVariables.saveData["cars"][carId]["slots"].list.Count; ++i)
            {
                if (StaticVariables.saveData["cars"][carId]["slots"][i].n != -1)
                {
                    string cardAttr = StaticVariables.GetCarSlotAttribute(carId, i);
                    int cardID = (int)StaticVariables.saveData["cars"][carId]["slots"][i].n;
                    carStatus.SetCardOnCar(cardAttr, cardID);
                }
            }
            
        }


        mark.transform.parent = miniMap.transform;
        marks[racerNum] = mark;
        racers[racerNum] = car;
        racersLength++;
        mark.transform.localPosition = new Vector3(0, 0, 0);
        mark.MyCar = car.transform;
        //car.GetComponentInChildren<EnemyBarLookAt>().mainCameraTrans = backGroundCamera.transform;

        if (car.GetComponent<TimeStopSkill>() != null)
        {
            car.GetComponent<TimeStopSkill>().backGroundCamera = backGroundCamera;
            car.GetComponent<TimeStopSkill>().mainCamera = mainCamera;

        }

        if (isPlayer)//player
        {
            statImage.collector = car.GetComponent<ItemCollector>();
            setCamera(car, skyBoxIdx);

            itemBtn.setCollector( car.GetComponent<ItemCollector>());
            moveBtn.setPlayerCar(car);

            DestroyImmediate(car.gameObject.GetComponent<AIScript>()); // delete player car's ai script
            miniMap.GetComponent<MiniMapManager>().setCamTarget(mark.transform);
        }
        else // AI
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
        float h = 2.3f, d = 3.8f;
        mainCamera.transform.position = car.transform.position + (h) * car.transform.up - d* car.transform.forward;
        backGroundCamera.transform.position = mainCamera.transform.position;
        /* mainCamera.transform.rotation =  Quaternion.Euler(new Vector3(
             car.transform.rotation.eulerAngles.x,
             car.transform.rotation.eulerAngles.y - 30,
             car.transform.rotation.eulerAngles.z));*/
        mainCamera.transform.rotation = car.transform.rotation;
        backGroundCamera.transform.rotation = mainCamera.transform.rotation;

        Transform target = car.transform;
        mainCamera.transform.LookAt(target);
        backGroundCamera.transform.LookAt(target);
        mainCamera.GetComponent<EnemyBarLookAt>().setTarget(target);
        backGroundCamera.GetComponent<EnemyBarLookAt>().setTarget(target);

        mainCamera.GetComponent<EnemyBarLookAt>().SetHeightDist(h,d);
        backGroundCamera.GetComponent<EnemyBarLookAt>().SetHeightDist(h, d);
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
