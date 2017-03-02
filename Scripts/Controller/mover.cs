using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mover : MonoBehaviour
{
    public Vector3 com = new Vector3(0,0,0);
    public float maxMotorTorque = 10000;
    public float maxBrakeTorque = 20000;
    public float maxSteerAngle = 10;
    public GameObject frontLeft, frontRight, rearLeft, rearRight;
    public Text speedText;
    public float topSpeed = 100 * 1000 / 3600;//(100 km/h)

    private bool isN2OReady  = true;
    private Rigidbody rb;
    private WheelController flController, frController, rlController, rrController;
    public float N2OPower = 2;

    public float N2OTime = 3;
    public ParticleSystem[] N2OParticles;
    
    

    //private float stuckSpeedThres = 3;
    private float N2OTimer = 0;
    private bool isN2OEmitting = false;

    // Use this for initialization 
    void Start()
    {


        rb = GetComponent<Rigidbody>();
        rb.centerOfMass =com;
        if (speedText != null)
        {
            speedText.text = "0 km/h";
        }
        flController = frontLeft.GetComponent<WheelController>();
        frController = frontRight.GetComponent<WheelController>();
        rlController = rearLeft.GetComponent<WheelController>();
        rrController = rearRight.GetComponent<WheelController>();
        
    }



    // Update is called once per frame 
    void Update()
    {
        if (transform.rotation.eulerAngles.x > 180)
        {
            transform.rotation = Quaternion.Euler(
                 Mathf.Clamp(transform.rotation.eulerAngles.x, 300, 360),
                 transform.rotation.eulerAngles.y,
                 transform.rotation.eulerAngles.z
                 );
        }
        else if (transform.rotation.eulerAngles.x <180)
        {
            transform.rotation = Quaternion.Euler(
                 Mathf.Clamp(transform.rotation.eulerAngles.x, 0, 60),
                 transform.rotation.eulerAngles.y,
                 transform.rotation.eulerAngles.z
                 );
        }

        if (transform.rotation.eulerAngles.z > 180)
        {
            transform.rotation = Quaternion.Euler(
                 transform.rotation.eulerAngles.x,
                 transform.rotation.eulerAngles.y,
                 Mathf.Clamp(transform.rotation.eulerAngles.z, 300, 360)
                 );
        }
        else if (transform.rotation.eulerAngles.z < 180)
        {
            transform.rotation = Quaternion.Euler(
                 transform.rotation.eulerAngles.x,
                 transform.rotation.eulerAngles.y,
                 Mathf.Clamp(transform.rotation.eulerAngles.z, 0, 60)
                 );
        }

        if (speedText != null)
        {
            speedText.text = Mathf.Round((rb.velocity.magnitude * 3600 / 1000) * 10) / 10f + " km/h";
        }
    }
    void FixedUpdate()
    {
        if (isN2OEmitting)
        {
            if (Time.time - N2OTimer < N2OTime)
            {
                rb.AddForce(transform.forward * N2OPower, ForceMode.Acceleration);
            }
            else
            {
                isN2OReady = true;
                isN2OEmitting = false;
                foreach (ParticleSystem N2O in N2OParticles)
                {
                    N2O.Stop();
                }
                N2OTimer = 0;

            }
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

    public void ApplyThrottle(float throttleFactor)
    {
        flController.ApplyThrottle(maxMotorTorque * throttleFactor);
        frController.ApplyThrottle(maxMotorTorque * throttleFactor);
        rlController.ApplyThrottle(maxMotorTorque * throttleFactor);
        rrController.ApplyThrottle(maxMotorTorque * throttleFactor);
    }

    public void ApplyBrake(float brakeFactor)
    {
        flController.ApplyBrake(maxBrakeTorque* brakeFactor);
        frController.ApplyBrake(maxBrakeTorque* brakeFactor);
        rlController.ApplyBrake(maxBrakeTorque* brakeFactor);
        rrController.ApplyBrake(maxBrakeTorque* brakeFactor);
    }

    public void ApplyN2O()
    {
        if(isN2OReady && !isN2OEmitting)
        {
            isN2OReady = false;
            isN2OEmitting = true;
            foreach (ParticleSystem N2O in N2OParticles)
            {
                N2O.Play();
            }
            N2OTimer = Time.time;
        }
    }

    public void ApplyPedal(float motorTorqueFactor, float brakeTorqueFactor)
    {
        ApplyBrake(brakeTorqueFactor);
        ApplyThrottle(motorTorqueFactor);
    }

    public void ApplySteer(float steerFactor)
    {
        float steerAngle = steerFactor * maxSteerAngle;
        flController.ApplySteer(steerAngle);
        frController.ApplySteer(steerAngle);
    }


}