using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mover : MonoBehaviour
{
    public Vector3 com = new Vector3(0,0,0);
    public float maxMotorTorque = 10000;
    public float maxBrakeTorque = 20000;
    public float steerRatio = 10;
    public GameObject frontLeft, frontRight, rearLeft, rearRight;
    public Text speedText;
    public float topSpeed = 100 * 1000 / 3600;//(100 km/h)

    private bool isN2OReady  = true;
    private Rigidbody rb;
    private WheelController flController, frController, rlController, rrController;
    public float N2OPower = 2;

    public float N2OTime = 3;
    public ParticleSystem[] N2OParticles;
    
    private float pathUpdateThreshold = 20;
    public int pathIdx = 1;
    public GameObject pathObject;
    private Transform[] pathArray;
    private float stuckSpeedThres = 3;
    private float N2OTimer = 0;
    private bool isN2OEmitting = false;

    // Use this for initialization 
    void Start()
    {

        pathArray = pathObject.GetComponentsInChildren<Transform>();
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
                 Mathf.Clamp(transform.rotation.eulerAngles.x, 335, 360),
                 transform.rotation.eulerAngles.y,
                 transform.rotation.eulerAngles.z
                 );
        }
        else if (transform.rotation.eulerAngles.x <180)
        {
            transform.rotation = Quaternion.Euler(
                 Mathf.Clamp(transform.rotation.eulerAngles.x, 0, 25),
                 transform.rotation.eulerAngles.y,
                 transform.rotation.eulerAngles.z
                 );
        }

        if (transform.rotation.eulerAngles.z > 180)
        {
            transform.rotation = Quaternion.Euler(
                 transform.rotation.eulerAngles.x,
                 transform.rotation.eulerAngles.y,
                 Mathf.Clamp(transform.rotation.eulerAngles.z, 335, 360)
                 );
        }
        else if (transform.rotation.eulerAngles.z < 180)
        {
            transform.rotation = Quaternion.Euler(
                 transform.rotation.eulerAngles.x,
                 transform.rotation.eulerAngles.y,
                 Mathf.Clamp(transform.rotation.eulerAngles.z, 0, 25)
                 );
        }

       // Debug.Log("transform.localEulerAngles = "+transform.localEulerAngles.ToString()+ " deviate angle = " + deviateAngle);
        if ((transform.position - pathArray[pathIdx].position).magnitude < pathUpdateThreshold && pathIdx+1 < pathArray.Length)
        {
           // Debug.Log("passed path point" + (pathIdx) + pathArray[pathIdx].position.ToString());
            pathIdx++;
        }
        int bestIdx = pathIdx;
        float bestDist = (transform.position - pathArray[bestIdx].position).magnitude;
        for (int i = pathIdx + 1; i < pathArray.Length; ++i)
        {
            if ((transform.position - pathArray[i].position).magnitude < bestDist)
            {
                bestIdx = i;
                bestDist = (transform.position - pathArray[bestIdx].position).magnitude;
            }
        }
        if (pathIdx != bestIdx)
        {
            //Debug.Log("go to  path point" + (bestIdx));
            pathIdx = bestIdx;

        }

        if (speedText != null)
        {
            speedText.text = Mathf.Round((rb.velocity.magnitude * 3600 / 1000) * 10) / 10f + " km/h";
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.N) && isN2OReady && !isN2OEmitting)
        {
            isN2OReady = false;
            isN2OEmitting = true;
            foreach (ParticleSystem N2O in N2OParticles)
            {
                N2O.Play();
            }
            N2OTimer = Time.time;
        }
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
        float motorTorque = maxMotorTorque;// * Input.GetAxis("Vertical");
        float brakeTorque = maxBrakeTorque * Input.GetAxis("Jump") ;
        flController.ApplyThrottle(motorTorque);
        frController.ApplyThrottle(motorTorque);
        rlController.ApplyThrottle(motorTorque);
        rrController.ApplyThrottle(motorTorque);
        
        
        flController.ApplyBrake(brakeTorque);
        frController.ApplyBrake(brakeTorque);
        rlController.ApplyBrake(brakeTorque);
        rrController.ApplyBrake(brakeTorque);


        float steerAngle = steerRatio *Input.GetAxis("Horizontal");// Input.acceleration.x;//

        flController.ApplySteer(steerAngle);
        frController.ApplySteer(steerAngle);

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