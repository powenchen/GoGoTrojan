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
    public int nextSteps = 3;

    public Vector3 com;
    public GameObject frontLeft, frontRight, rearLeft, rearRight;
    private Rigidbody rb;
    private WheelController flController, frController, rlController, rrController;

    public float sensorLength = 5;
    public float sideSensorLength = 3;
    public float frontSensorStartPoint = 3;
    public float frontSensorMargin = 1;
    public float frontSensorAngle = 15;
    public float avoidSpeed = 10;
    private int flag = 0;

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
        if (pathPointIdx >= pathPoints.Length)
        {
            flController.ApplyBrake(maxBrakeTorque);
            frController.ApplyBrake(maxBrakeTorque);
            rlController.ApplyBrake(maxBrakeTorque);
            rrController.ApplyBrake(maxBrakeTorque);
            return;
        }
        if (flag == 0)
        {
            if (pathPointIdx < pathPoints.Length)
            {
                bool isReverse = false;
                int bestIdx = pathPointIdx;

                Vector3 bestPoint = transform.InverseTransformPoint(new Vector3(
                                                            pathPoints[pathPointIdx].position.x,
                                                            transform.position.y,
                                                            pathPoints[pathPointIdx].position.z
                                                            ));
                for (int i = pathPointIdx + 1; i < pathPoints.Length && i < pathPointIdx + nextSteps; ++i)
                {
                    Vector3 point = transform.InverseTransformPoint(new Vector3(
                                                            pathPoints[i].position.x,
                                                            transform.position.y,
                                                            pathPoints[i].position.z
                                                            ));
                    if (point.z > 0)
                    {
                        if ((point.x / point.z) < (bestPoint.x / bestPoint.z))
                        {
                            bestPoint = point;
                            bestIdx = i;
                        }
                    }
                }

                if (bestPoint.z < -distThreshold)
                {
                    isReverse = true;
                }
                else
                {
                    pathPointIdx = bestIdx;
                    Debug.Log("go to point" + bestIdx);
                }



                Vector3 steerVector = transform.InverseTransformPoint(new Vector3(
                                                            pathPoints[pathPointIdx].position.x,
                                                            transform.position.y,
                                                            pathPoints[pathPointIdx].position.z
                                                            ));

                //Debug.Log(steerVector.ToString());
                float newSteer = maxSteer * (steerVector.x / steerVector.magnitude);

                float newMotorTorque = maxMotorTorque * (1 - Mathf.Abs(steerVector.x / steerVector.magnitude));

                if (isReverse)
                {
                    Debug.Log("isReverse; go to point" + bestPoint.x + "," + bestPoint.z);
                    flController.ApplySteer(-maxSteer * Mathf.Sign(newSteer));
                    frController.ApplySteer(-maxSteer * Mathf.Sign(newSteer));

                    flController.ApplyThrottle(-maxMotorTorque * 0.5f);
                    frController.ApplyThrottle(-maxMotorTorque * 0.5f);
                    rlController.ApplyThrottle(-maxMotorTorque * 0.5f);
                    rrController.ApplyThrottle(-maxMotorTorque * 0.5f);
                }
                else if (bestPoint.magnitude < distThreshold)
                {
                    Debug.Log("passed point" + pathPointIdx + "[" + pathPoints[pathPointIdx].position.x + "," + pathPoints[pathPointIdx].position.y + "," + pathPoints[pathPointIdx].position.z + "]");

                    ++pathPointIdx;
                }

                else
                {
                    flController.ApplySteer(newSteer);
                    frController.ApplySteer(newSteer);

                    flController.ApplyThrottle(newMotorTorque);
                    frController.ApplyThrottle(newMotorTorque);
                    rlController.ApplyThrottle(newMotorTorque);
                    rrController.ApplyThrottle(newMotorTorque);
                }
            }
            if (rb.velocity.magnitude > topSpeed)
            {
                float slowDownRatio = rb.velocity.magnitude / topSpeed;
                rb.velocity /= slowDownRatio;
            }
        }

        Sensor();
    }


    public void speedDebuff(float debuffRatio)
    {
        topSpeed /= debuffRatio;
    }

    public void removeDebuff(float debuffRatio)
    {
        topSpeed *= debuffRatio;
    }

    public void Sensor()
    {
        flag = 0;
        float avoidSensitivity = 0;
        Vector3 pos;
        pos = transform.position;
        pos += transform.forward * frontSensorStartPoint;
        RaycastHit hit;
        
        //braking sensor
        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength) && !hit.transform.CompareTag("Terrain"))
        {
            ++flag;
            flController.ApplyBrake(maxBrakeTorque * 0.2f);
            frController.ApplyBrake(maxBrakeTorque * 0.2f);
            rlController.ApplyBrake(maxBrakeTorque * 0.2f);
            rrController.ApplyBrake(maxBrakeTorque * 0.2f);
            Debug.DrawLine(pos, hit.point, Color.red);
        }
        else
        {
            flController.ApplyBrake(0);
            frController.ApplyBrake(0);
            rlController.ApplyBrake(0);
            rrController.ApplyBrake(0);
        }
        

        //front right sensor and right angled sensor
        pos = transform.position;
        pos += transform.forward * frontSensorStartPoint + transform.right*frontSensorMargin;
        Vector3 sensorAngle = Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward;
        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength) && !hit.transform.CompareTag("Terrain"))
        {
            ++flag;
            avoidSensitivity -= 1;
            Debug.DrawLine(pos, hit.point, Color.white);
        }
        else if (Physics.Raycast(pos, sensorAngle, out hit, sensorLength) && !hit.transform.CompareTag("Terrain"))
        {
            ++flag;
            avoidSensitivity -= 0.5f;
            Debug.DrawLine(pos, hit.point, Color.white);
        }


        //front left sensor
        pos = transform.position;
        pos += transform.forward * frontSensorStartPoint - transform.right * frontSensorMargin;

        sensorAngle = Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward;
        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength) && !hit.transform.CompareTag("Terrain"))
        {
            ++flag;
            avoidSensitivity += 1;
            Debug.DrawLine(pos, hit.point, Color.white);
        }
        else if (Physics.Raycast(pos, sensorAngle, out hit, sensorLength) && !hit.transform.CompareTag("Terrain"))
        {
            ++flag;
            avoidSensitivity += 0.5f;
            Debug.DrawLine(pos, hit.point, Color.white);
        }

        //right side sensor
        if (Physics.Raycast(transform.position, transform.right, out hit, sideSensorLength) && !hit.transform.CompareTag("Terrain"))
        {
            ++flag;
            avoidSensitivity -= 0.5f;
            Debug.DrawLine(transform.position, hit.point, Color.white);
        }

        //left side sensor
        if (Physics.Raycast(transform.position, -transform.right, out hit, sideSensorLength) && !hit.transform.CompareTag("Terrain"))
        {
            ++flag;
            avoidSensitivity += 0.5f;
            Debug.DrawLine(transform.position, hit.point, Color.white);
        }

        pos = transform.position;
        pos += transform.forward * frontSensorStartPoint;
        //front mid sensor
        if (avoidSensitivity == 0)
        {
            if (Physics.Raycast(pos, transform.forward, out hit, sensorLength))
            {
                if (hit.normal.x < 0)
                {
                    avoidSensitivity = 1;
                }
                else
                {
                    avoidSensitivity = -1;
                }
                Debug.DrawLine(pos, hit.point, Color.white);
            }
        }

        if (flag != 0)
        {
            AvoidSteer(avoidSensitivity);
        }

    }

    void AvoidSteer(float sensitivity)
    {
        float newSteer = avoidSpeed * sensitivity;
        Debug.Log("avoid steer " + newSteer);
        flController.ApplySteer(newSteer);
        frController.ApplySteer(newSteer);
    }


}
