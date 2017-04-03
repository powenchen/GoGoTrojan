using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopSkill : Skill
{
    public Camera backGroundCamera, mainCamera;
    private UnityStandardAssets.ImageEffects.ColorCorrectionRamp grayScaleScript;

    private float StopTime = 1;
    public float StopTimerCurr = 0;
    private bool isSkillReady = true;

    public GameObject[] enemies;
    
    
    // Use this for initialization
    void Start () {
        isSkillUsing = false;
        mainCamera.enabled = false;
        grayScaleScript = backGroundCamera.GetComponent< UnityStandardAssets.ImageEffects.ColorCorrectionRamp> ();
        grayScaleScript.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("stop test; position variation = " + Time.fixedDeltaTime * GetComponent<Car>().getTopSpeed() * transform.forward);
        if (isSkillUsing)
        {
            StopTimerCurr += Time.fixedDeltaTime;
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Car>().stopBySkill(true);
            }
            /*enemy.GetComponent<Rigidbody>().isKinematic = true;
            enemy.GetComponent<Car>().stopRunning();*/
            if (StopTimerCurr >= StopTime)
            {
                stopSkill();

            }
        }
    }

    private void changeGrayScaleLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
        foreach (Transform t in gameObject.GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }

    public override void stopSkill()
    {
        if (isSkillUsing)
        {
            isSkillReady = true;
            isSkillUsing = false;
            changeGrayScaleLayer("Default");
            mainCamera.enabled = false;
            grayScaleScript.enabled = false;


            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Car>().stopBySkill(false);

            }
            StopTimerCurr = 0;
        }
    }

    public override void activateSkill()
    {
        if (isSkillReady && GetComponent<AIScript>() != null)
        {
            CarCheckPoint[] points = FindObjectsOfType<CarCheckPoint>();
            Vector3 pos = new Vector3();
            Quaternion spawnRot = new Quaternion() ;
            int distCurr = FindObjectOfType<RankingSystem>().GetCarDist(GetComponent<Car>());
            int distNext = Mathf.Min(distCurr + 2, points.Length - 1);
            foreach (CarCheckPoint point in points)
            {
                if (point.dist == distNext)
                {
                    Vector3 posCenter = new Vector3(
                        point.transform.position.x,
                        point.transform.position.y -
                        point.transform.localScale.y / 2,
                        point.transform.position.z
                    );
                    Vector3 posLeft = posCenter - point.transform.right * 5f;
                    Vector3 posRight = posCenter + point.transform.right * 5f;
                    pos = posCenter;

                    string spawnStr = "center";
                    foreach (Car car in FindObjectsOfType<Car>())
                    {

                        Debug.Log("dist from center " + (car.transform.position - posCenter).magnitude);
                        if ((car.transform.position - posCenter).magnitude < 3f)
                        {
                            pos = posLeft;
                            spawnStr = "left";
                            foreach (Car car1 in FindObjectsOfType<Car>())
                            {
                                if ((car1.transform.position - posLeft).magnitude < 3f)
                                {
                                    spawnStr = "right";
                                    pos = posRight;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    spawnRot = point.transform.rotation;
                    Debug.Log("orig = "+ distCurr + " spawn to " + point.name +"("+spawnStr+ ") dist = "+ distNext);
                    break;
                }
            }

            
            transform.position = pos;
            transform.rotation = spawnRot;
            //FindObjectOfType<RankingSystem>().SetCarDist(GetComponent<Car>(), distNext);
            return;
        }
        if (isSkillReady && !isSkillUsing)
        {
            isSkillReady = false;
            isSkillUsing = true;

            changeGrayScaleLayer("quickSilver");
            mainCamera.enabled = true;
            grayScaleScript.enabled = true;
            StopTimerCurr = 0;
        }

    }
    
}
