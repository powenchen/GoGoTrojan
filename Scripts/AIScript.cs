using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIScript : MonoBehaviour {
    public float maxSteer = 15.0f;
    public float maxMotorTorque = 5000.0f;
    public float maxBrakeTorque = 20000.0f;
    public float distThreshold = 3.0f;
    public float steerFilterConstant = 3.0f;
    public Text speedText;

    public float topSpeed = 100 * 1000 / 3600;//(100 km/h)
    public GameObject path;
    private Transform[] pathPoints;
    private int pathPointIdx = 1; // start from 1 since idx = 0 is parent component 

    public float speedThreshold = 1;
    public int stepsBelowThreshold = 15, stepsAboveThreshold = 12;
	public float  forwardStiffness = 5,sidewayStiffness = 10;

    public Vector3 com;
    public GameObject frontLeft, frontRight, rearLeft, rearRight;
    private Rigidbody rb;
    private WheelController flController, frController, rlController, rrController;
    

    // Use this for initialization
    void Start () {
        speedText.text = "AISpeed: 0 km/h";


        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
        if (path!=null)
        {
            pathPoints = path.GetComponentsInChildren<Transform>();
        }

        flController = frontLeft.GetComponent<WheelController>();
        frController = frontRight.GetComponent<WheelController>();
        rlController = rearLeft.GetComponent<WheelController>();
        rrController = rearRight.GetComponent<WheelController>();

        flController.ConfigureWheelSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        flController.ConfigureFriction(forwardStiffness, sidewayStiffness);
        frController.ConfigureWheelSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        frController.ConfigureFriction(forwardStiffness, sidewayStiffness);
        rlController.ConfigureWheelSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        rlController.ConfigureFriction(forwardStiffness, sidewayStiffness);
        rrController.ConfigureWheelSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        rrController.ConfigureFriction(forwardStiffness, sidewayStiffness);

    }

    private void Update()
    {
        float speed = Mathf.Round((rb.velocity.magnitude * 3600 / 1000) * 10) / 10f;
        speedText.text = "AISpeed: " + speed + " km/h";
    }

    private void FixedUpdate()
    {
        if (pathPointIdx < pathPoints.Length)
        {
            Vector3 steerVector = transform.InverseTransformPoint(new Vector3(
                                                        pathPoints[pathPointIdx].position.x,
                                                        transform.position.y,
                                                        pathPoints[pathPointIdx].position.z
                                                        ));
            float newSteer = maxSteer * (steerVector.x / steerVector.magnitude);

            flController.ApplySteer(newSteer);
            frController.ApplySteer(newSteer);
            
            float newMotorTorque = maxMotorTorque * (1 - Mathf.Abs(steerVector.x / steerVector.magnitude));
            flController.ApplyThrottle(newMotorTorque);
            frController.ApplyThrottle(newMotorTorque);
            rlController.ApplyThrottle(newMotorTorque);
            rrController.ApplyThrottle(newMotorTorque);
            

            Vector3 distToPoint = transform.position - pathPoints[pathPointIdx].position;
            if (distToPoint.magnitude < distThreshold || steerVector.z < 0)
            {
               // Debug.Log("passed point" + pathPointIdx + "[" + pathPoints[pathPointIdx].position.x + "," + pathPoints[pathPointIdx].position.y + "," + pathPoints[pathPointIdx].position.z + "]");
                ++pathPointIdx;
            }
        }
        else
        {
            flController.ApplyBrake(maxBrakeTorque);
            frController.ApplyBrake(maxBrakeTorque);
            rlController.ApplyBrake(maxBrakeTorque);
            rrController.ApplyBrake(maxBrakeTorque);
        }
        if (rb.velocity.magnitude > topSpeed)
        {
            float slowDownRatio = rb.velocity.magnitude / topSpeed;
            rb.velocity /= slowDownRatio;
        }
    }


    public void speedDebuff(float debuffRatio)
    {
        topSpeed /= debuffRatio;
    }

    public void removeDebuff(float debuffRatio)
    {
        topSpeed *= debuffRatio;
    }


}
