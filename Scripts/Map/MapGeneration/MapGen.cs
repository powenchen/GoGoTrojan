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

    // for debug
    public int[] carinitDebug;
    public int courseinitDebug;
    public int skyBoxInitDebug;
    private float maxHPDebug = 100;
    private float maxMPDebug = 100;
    private float maxATKDebug = 100;
    
    public bool debugFlag = false;

    public itemBtnOnClick itemBtn;
    public moveBtnOnClick moveBtn;
    public StatImageManager statImage;
    private float basicTopSpeed = 27;

    // Use this for initialization; used for debug
    void OnDrawGizmos ()
    {
              
        if (debugFlag)
        {
            skyBoxIdx = skyBoxInitDebug;
            setCourse(courseinitDebug);
            for (int i = 0; i < carinitDebug.Length; ++i)
            {
               
                initCharacter(courseinitDebug, carinitDebug[i], maxHPDebug, maxMPDebug, maxATKDebug,100,100, i, (i == 0));
            }
            initTimeStopskillUsers();

            debugFlag = false;
        }
    }
    void Start()
    {
        resetMap();
        StaticVariables.resetVariables();
        skyBoxIdx = PlayerPrefs.GetInt("CourseID");
        setCourse(PlayerPrefs.GetInt("CourseID"));
        
        int id = PlayerPrefs.GetInt("PlayerID") + 2 * PlayerPrefs.GetInt("CarID");
        PlayerPrefs.SetInt("Coins", 0);
        float hp;
        float mp;
        float atk;
        float skillCD;
        float speed;
        if (PlayerPrefs.GetInt("PlayerID") == 0)
        {
            hp = PlayerPrefs.GetFloat("Player1_Health");
            atk = PlayerPrefs.GetFloat("Player1_Power");
            speed = PlayerPrefs.GetFloat("Player1_Speed");
            skillCD = PlayerPrefs.GetFloat("Player1_SkillCDR");
            mp = PlayerPrefs.GetFloat("Player1_Mana");

        }
        else
        {
            hp = PlayerPrefs.GetFloat("Player2_Health");
            atk = PlayerPrefs.GetFloat("Player2_Power");
            speed = PlayerPrefs.GetFloat("Player2_Speed");
            skillCD = PlayerPrefs.GetFloat("Player2_SkillCDR");
            mp = PlayerPrefs.GetFloat("Player2_Mana");
        }
        hp = 100;
        mp = 100;
        atk = 100;
        skillCD = 50;
        speed = 80;
        Debug.Log("CourseID = " + PlayerPrefs.GetInt("CourseID"));
        initCharacter(PlayerPrefs.GetInt("CourseID"), id, hp,mp, atk, speed, skillCD,0, true);

        initCharacter(PlayerPrefs.GetInt("CourseID"), Random.Range(0, 3), 100, 100, 100,150,50, 1, false);
        //initCharacter(PlayerPrefs.GetInt("CourseID"), Random.Range(0, 3), 100, 100, 100, 2, false);


        //there are carinitDebug.Length cars; init them in time stop skill users' enemy lists 
        initTimeStopskillUsers();
        
    }
    // Update is called once per frame
    void Update () {
        racersLength = countRacers();
	}

    public void resetMap()
    {
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

    public void initCharacter(int courseID ,int charID, float maxHP, float maxMP, float attackPower,float speed, float skillCD, int racerNum, bool isPlayer = false)
    {
        Debug.Log("generating char = " + charID);
        Vector3 position = courseList[courseID].getStartPositions()[racerNum + 1];
        Quaternion rotation = Quaternion.Euler(0, courseList[courseID].getStartRotation().eulerAngles.y, 0);
        PathManager path = courseList[courseID].getPath();

        Car car = Instantiate(characterList[charID], position, rotation).GetComponent<Car>();
        MiniMapMark mark = Instantiate(miniMarkList[charID], position, rotation).GetComponent<MiniMapMark>();

        //init car params
        car.attackInitialize(attackPower);
        car.HPInitialize(maxHP);
        car.MPInitialize(maxMP);
        car.skillCDInitialize(skillCD);
        car.setTopSpeed(basicTopSpeed * (1+(speed / 1000f)));//speed = 100pt = 1.1times

        mark.transform.parent = miniMap.transform;
        marks[racerNum] = mark;
        racers[racerNum] = car;
        racersLength++;
        mark.transform.localPosition = new Vector3(0, 0, 0);
        mark.MyCar = car.transform;
        car.GetComponentInChildren<EnemyBarLookAt>().mainCameraTrans = backGroundCamera.transform;

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
        mainCamera.transform.position = car.transform.position + 2* car.transform.up - 6* car.transform.forward;
        backGroundCamera.transform.position = mainCamera.transform.position;
        /* mainCamera.transform.rotation =  Quaternion.Euler(new Vector3(
             car.transform.rotation.eulerAngles.x,
             car.transform.rotation.eulerAngles.y - 30,
             car.transform.rotation.eulerAngles.z));*/
        mainCamera.transform.rotation = car.transform.rotation;
        backGroundCamera.transform.rotation = mainCamera.transform.rotation;

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
