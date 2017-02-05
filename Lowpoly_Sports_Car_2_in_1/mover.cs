using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mover : MonoBehaviour
{
    public Vector3 com;
    public float motorTorque = 10000;
    public float brakeTorque = 20000;
    public float steerRatio = 10;
    public float speedThreshold=1;
    public int stepsBelowThreshold=15, stepsAboveThreshold=12;

    public GameObject frontLeft, frontRight, rearLeft, rearRight;
    private WheelCollider flCollider, frCollider, rlCollider, rrCollider;
    public Text speedText;
    private Rigidbody rb;

	public float forwardStiffness = 5,sidewayStiffness =10;

    // Use this for initialization 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass =com;
        speedText.text = "0 km/h";
        flCollider = frontLeft.GetComponent<WheelCollider>();
        frCollider = frontRight.GetComponent<WheelCollider>();
        rlCollider = rearLeft.GetComponent<WheelCollider>();
        rrCollider = rearRight.GetComponent<WheelCollider>();

        flCollider.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        frCollider.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        rlCollider.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
        rrCollider.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
		ConfigureFriction ();
    }


	void ConfigureFriction()
	{
		WheelFrictionCurve fFriction = flCollider.forwardFriction, sFriction = flCollider.sidewaysFriction; 
		fFriction.stiffness = forwardStiffness;
		sFriction.stiffness = sidewayStiffness;

		flCollider.sidewaysFriction = sFriction;
		flCollider.forwardFriction = fFriction;
		frCollider.sidewaysFriction = sFriction;
		frCollider.forwardFriction = fFriction;
		rlCollider.sidewaysFriction = sFriction;
		rlCollider.forwardFriction = fFriction;
		rrCollider.sidewaysFriction = sFriction;
		rrCollider.forwardFriction = fFriction;
	}
    // Update is called once per frame 
    void Update()
    {
        speedText.text = Mathf.Round((rb.velocity.magnitude* 3600/1000)*10)/10f + " km/h";
        frontLeft.transform.Rotate(new Vector3(0, flCollider.rpm / 60 * 360 * Time.deltaTime, 0));
        frontRight.transform.Rotate(new Vector3(0, frCollider.rpm / 60 * 360 * Time.deltaTime, 0));
        rearLeft.transform.Rotate(new Vector3(0, rlCollider.rpm / 60 * 360 * Time.deltaTime, 0));
        rearRight.transform.Rotate(new Vector3(0, rrCollider.rpm / 60 * 360 * Time.deltaTime, 0));

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

        flCollider.steerAngle = frCollider.steerAngle =  steerRatio * Input.GetAxis("Horizontal");

       // Debug.Log("motortorque = " + flCollider.motorTorque);
    }
}