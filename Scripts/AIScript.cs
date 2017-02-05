using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIScript : MonoBehaviour {
    public float maxSteer = 15.0f;
    public float motorTorque = 500.0f;
    public float brakeTorque = 20000.0f;
    public float distThreshold = 3.0f;
    public float steerFilterConstant = 3.0f;
    public Text speedText;

    public GameObject path;
    private Vector3 steerVector;
    private Transform[] pathPoints;
    private int pathPointIdx = 1; // start from 1 since idx = 0 is parent component 
    public WheelCollider flWheel, frWheel, rlWheel, rrWheel;

    public float speedThreshold = 1;
    public int stepsBelowThreshold = 15, stepsAboveThreshold = 12;
	public float  forwardStiffness = 5,sidewayStiffness = 10;

    public Vector3 com;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        speedText.text = "AISpeed: 0 km/h";


        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
        if (path!=null)
        {
            pathPoints = path.GetComponentsInChildren<Transform>();
        }
        flWheel.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        frWheel.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        rlWheel.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        rrWheel.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);

		ConfigureFriction ();

    }

	void ConfigureFriction()
	{
		WheelFrictionCurve fFriction = flWheel.forwardFriction, sFriction = flWheel.sidewaysFriction; 
		fFriction.stiffness = forwardStiffness;
		sFriction.stiffness = sidewayStiffness;

		flWheel.sidewaysFriction = sFriction;
		flWheel.forwardFriction = fFriction;
		frWheel.sidewaysFriction = sFriction;
		frWheel.forwardFriction = fFriction;
		rlWheel.sidewaysFriction = sFriction;
		rlWheel.forwardFriction = fFriction;
		rrWheel.sidewaysFriction = sFriction;
		rrWheel.forwardFriction = fFriction;
	}

    private void FixedUpdate()
    {
        
        float speed = Mathf.Round((rb.velocity.magnitude * 3600 / 1000) * 10) / 10f;
        speedText.text = "AISpeed: " + speed + " km/h";
        if (pathPointIdx < pathPoints.Length)
        {
            GetSteer();
            ApplyThrottle();
            Vector3 distToPoint = transform.position - pathPoints[pathPointIdx].position;
            if (distToPoint.magnitude < distThreshold || steerVector.z < 0)
            {
                Debug.Log("passed point" + pathPointIdx + "[" + pathPoints[pathPointIdx].position.x + "," + pathPoints[pathPointIdx].position.y + "," + pathPoints[pathPointIdx].position.z + "]");
                ++pathPointIdx;
            }
        }
        else
        {
            ApplyBrake(); 
        }
        
    }

    private void ApplyThrottle()
    {
        float steerFactor =  Mathf.Abs(steerVector.x / steerVector.magnitude);
        float newMotorTorque = motorTorque*(1-steerFactor);
        //Debug.Log("steerFactor = " + steerFactor);
        flWheel.motorTorque = frWheel.motorTorque = rlWheel.motorTorque = rrWheel.motorTorque = newMotorTorque;
        //Debug.Log("motortorque = "+ flWheel.motorTorque);
    }

    private void ApplyBrake()
    {
        flWheel.brakeTorque = frWheel.brakeTorque = rlWheel.brakeTorque = rrWheel.brakeTorque = brakeTorque;
    }

    private void GetSteer()
    {
        steerVector = transform.InverseTransformPoint(new Vector3(
                                                        pathPoints[pathPointIdx].position.x,
                                                        transform.position.y,
                                                        pathPoints[pathPointIdx].position.z
                                                        ));
        float newSteer = maxSteer * (steerVector.x / steerVector.magnitude);

//        float alpha = 1 / (1 + steerFilterConstant);
  //      newSteer = (1 - alpha) * flWheel.steerAngle + alpha * newSteer;
        flWheel.steerAngle = frWheel.steerAngle = newSteer;
        Debug.Log("steerVector = (" + steerVector.x + ","+ steerVector.y + ","+ steerVector.z+")");
    }
}
