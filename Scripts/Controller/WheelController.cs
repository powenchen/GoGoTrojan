using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    private WheelCollider wheel;
    // Use this for initialization
    void Start()
    {
        wheel = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * Time.deltaTime * wheel.rpm / 60f * 360f);
    }

    private void FixedUpdate()
    {
    }

   /* public void SlowDown(float debuff)
    {
        slowDownDebuff *= debuff;
        ApplyBrake(wheel.brakeTorque);
        Debug.Log("slow down " + wheel.brakeTorque);
    }*/

   

    public void ApplyBrake(float brakeTorque)
    {
        wheel.brakeTorque = brakeTorque;
    }

    public void ApplyThrottle(float motorTorque)
    {
        wheel.motorTorque = motorTorque;
    }

    public void ApplySteer(float steerAngle)
    {
        wheel.steerAngle = steerAngle;
    }

    public void ConfigureFriction(float forwardStiffness, float sideStiffness)
    {
        WheelFrictionCurve fFriction = wheel.forwardFriction, sFriction = wheel.sidewaysFriction;
        fFriction.stiffness = forwardStiffness;
        sFriction.stiffness = sideStiffness;

        wheel.sidewaysFriction = sFriction;
        wheel.forwardFriction = fFriction;

    }

    public void ConfigureWheelSubsteps(float speedThreshold, int stepsBelowThreshold, int stepsAboveThreshold)
    {
        wheel.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
    }


}
