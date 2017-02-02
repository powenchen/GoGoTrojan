using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mover : MonoBehaviour
{
    public float motorTorque = 10000;
    public float brakeTorque = 20000;
    private float currentTorque = 0f;
    public float steerRatio = 10;
    public float speedThreshold;
    public int stepsBelowThreshold, stepsAboveThreshold;

    public GameObject frontLeft, frontRight, rearLeft, rearRight;
    private WheelCollider flCollider, frCollider, rlCollider, rrCollider;
    public Text speedText;
    private Rigidbody rb;

    // Use this for initialization 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 0, 0);
        speedText.text = "0 MPH";
        flCollider = frontLeft.GetComponent<WheelCollider>();
        frCollider = frontRight.GetComponent<WheelCollider>();
        rlCollider = rearLeft.GetComponent<WheelCollider>();
        rrCollider = rearRight.GetComponent<WheelCollider>();

        flCollider.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        frCollider.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        rlCollider.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        rrCollider.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);

    }

    // Update is called once per frame 
    void Update()
    {
        speedText.text = Mathf.Round(rb.velocity.magnitude * 2.23693629f*10f)/10f + " MPH";

        /*if (Input.GetKey(KeyCode.W))
        {
            transform.position -= Quaternion.Euler(0,-90,0)*transform.forward * forwardSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Quaternion.Euler(0, -90, 0) * transform.forward * backwardSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0.0f, -rotateSpeed * Time.deltaTime, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f);
        }*/

    }
    void FixedUpdate()
    {
        flCollider.motorTorque = frCollider.motorTorque = rlCollider.motorTorque = rrCollider.motorTorque = motorTorque * Input.GetAxis("Vertical");
        flCollider.brakeTorque = frCollider.brakeTorque = rlCollider.brakeTorque = rrCollider.brakeTorque = brakeTorque * Input.GetAxis("Jump");

        flCollider.steerAngle = frCollider.steerAngle = steerRatio * Input.GetAxis("Horizontal");
        frontRight.transform.rotation = frontLeft.transform.rotation = Quaternion.Euler(90,90,-10 * Input.GetAxis("Horizontal")) ; 

    }
}