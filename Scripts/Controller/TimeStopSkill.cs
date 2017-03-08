using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopSkill : Skill
{
    public Camera backGroundCamera, mainCamera;
    private UnityStandardAssets.ImageEffects.ColorCorrectionRamp grayScaleScript;

    private float StopTime = 1;
    public float StopTimerCurr = 0;
    private bool skillUsing = false;
    private bool isSkillReady = true;

    public GameObject[] enemies;
    
    
    // Use this for initialization
    void Start () {
        mainCamera.enabled = false;
        grayScaleScript = backGroundCamera.GetComponent< UnityStandardAssets.ImageEffects.ColorCorrectionRamp> ();
        grayScaleScript.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("stop test; position variation = " + Time.fixedDeltaTime * GetComponent<Car>().getTopSpeed() * transform.forward);
        if (skillUsing)
        {
            StopTimerCurr += Time.fixedDeltaTime;
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Car>().stopRunning();
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
        if (skillUsing)
        {
            isSkillReady = true;
            skillUsing = false;
            changeGrayScaleLayer("Default");
            mainCamera.enabled = false;
            grayScaleScript.enabled = false;


            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Car>().startRunning();
            }
            StopTimerCurr = 0;
        }
    }

    public override void activateSkill()
    {
        if (isSkillReady && !skillUsing)
        {
            isSkillReady = false;
            skillUsing = true;

            changeGrayScaleLayer("quickSilver");
            mainCamera.enabled = true;
            grayScaleScript.enabled = true;
            StopTimerCurr = 0;
        }

    }

    public bool isSkillUsing()
    {
        return skillUsing;
    }
}
