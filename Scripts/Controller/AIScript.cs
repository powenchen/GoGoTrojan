using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// test modification

public class AIScript : MonoBehaviour {
    public float distThreshold = 3.0f;
    
    public GameObject path;
    private Transform[] pathPoints;
    private int pathPointIdx = 1; // start from 1 since idx = 0 is parent component 
    
    private Rigidbody rb;

    public float sensorLength = 5;
    public float sideSensorLength = 3;
    public float frontSensorStartPoint = 3;
    public float frontSensorMargin = 1;
    public float frontSensorAngle = 15;
    public float avoidSpeed = 0.6f;
    private int flag = 0;
    private bool reversing = false;
    public float reverseTimer = 0;
    public float waitToReverse = 2;
    public float reverseFor = 1.5f;
    public float stuckThreshlod = 10 * 1000 / 3600;//(10 km/h)

    private mover carMover;

    // Use this for initialization
    void Start () {
        carMover = GetComponent<mover>();
        rb = GetComponent<Rigidbody>();
        if (path!=null)
        {
            pathPoints = path.GetComponentsInChildren<Transform>();
        }
        

    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {

        //Debug.Log("steer = "+frontLeft.GetComponent<WheelCollider>().steerAngle + ", motor torque = "+ frontLeft.GetComponent<WheelCollider>().motorTorque);

        if (pathPointIdx >= pathPoints.Length)
        {
            carMover.ApplyBrake(1);// full brake
            return;
        }
        Vector3 nextPoint = transform.InverseTransformPoint(new Vector3(
                                                            pathPoints[pathPointIdx].position.x,
                                                            transform.position.y,
                                                            pathPoints[pathPointIdx].position.z
                                                            ));
               
        Vector3 steerVector = transform.InverseTransformPoint(new Vector3(
                                                            pathPoints[pathPointIdx].position.x,
                                                            transform.position.y,
                                                            pathPoints[pathPointIdx].position.z
                                                            ));

        if (nextPoint.magnitude < distThreshold)
        {
            Debug.Log("AI passed point" + pathPointIdx + "[" + pathPoints[pathPointIdx].position.x + "," + pathPoints[pathPointIdx].position.y + "," + pathPoints[pathPointIdx].position.z + "]");
            ++pathPointIdx;
        }

        float newSteerFactor =  (steerVector.x / steerVector.magnitude);

        float newMotorTorqueFactor = 1;// * (1 - Mathf.Abs(steerVector.x / steerVector.magnitude));

        if (reversing)
        {
            newMotorTorqueFactor *= -1;
        }

        if (flag == 0)
        {
            carMover.ApplySteer(newSteerFactor);
        }
        carMover.ApplyThrottle(newMotorTorqueFactor);


       Sensor();
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
        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength) /*&& !hit.transform.CompareTag("Terrain")*/)
        {
            ++flag;
            carMover.ApplyBrake(0.2f);
            Debug.DrawLine(pos, hit.point, Color.red);
        }
        else
        {
            carMover.ApplyBrake(0);
        }
        

        //front right sensor and right angled sensor
        pos = transform.position;
        pos += transform.forward * frontSensorStartPoint + transform.right*frontSensorMargin;
        Vector3 sensorAngle = Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward;
        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength) /*&& !hit.transform.CompareTag("Terrain")*/)
        {
            ++flag;
            avoidSensitivity -= 1;
            Debug.DrawLine(pos, hit.point, Color.white);
        }
        else if (Physics.Raycast(pos, sensorAngle, out hit, sensorLength) /*&& !hit.transform.CompareTag("Terrain")*/)
        {
            ++flag;
            avoidSensitivity -= 0.5f;
            Debug.DrawLine(pos, hit.point, Color.white);
        }


        //front left sensor
        pos = transform.position;
        pos += transform.forward * frontSensorStartPoint - transform.right * frontSensorMargin;

        sensorAngle = Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward;
        if (Physics.Raycast(pos, transform.forward, out hit, sensorLength) /*&& !hit.transform.CompareTag("Terrain")*/)
        {
            ++flag;
            avoidSensitivity += 1;
            Debug.DrawLine(pos, hit.point, Color.white);
        }
        else if (Physics.Raycast(pos, sensorAngle, out hit, sensorLength) /*&& !hit.transform.CompareTag("Terrain")*/)
        {
            ++flag;
            avoidSensitivity += 0.5f;
            Debug.DrawLine(pos, hit.point, Color.white);
        }

        //right side sensor
        if (Physics.Raycast(transform.position, transform.right, out hit, sideSensorLength) /*&& !hit.transform.CompareTag("Terrain")*/)
        {
            ++flag;
            avoidSensitivity -= 0.5f;
            Debug.DrawLine(transform.position, hit.point, Color.white);
        }

        //left side sensor
        if (Physics.Raycast(transform.position, -transform.right, out hit, sideSensorLength) /*&& !hit.transform.CompareTag("Terrain")*/)
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

        //reverse mechanism
        if (rb.velocity.magnitude < stuckThreshlod && !reversing)
        {
            reverseTimer += Time.deltaTime;
            if (reverseTimer >= waitToReverse)
            {
                reverseTimer = 0;
                reversing = true;
            }
        }
        else if (!reversing)
        {
            reverseTimer = 0;
        }

        if (reversing)
        {
            Debug.Log("reversing");
            reverseTimer += Time.deltaTime;
            avoidSensitivity *= -1;
            if (reverseTimer >= reverseFor)
            {
                reverseTimer = 0;
                reversing = false;
            }
        }

        if (flag != 0)
        {
            AvoidSteer(avoidSensitivity);
        }

    }

    void AvoidSteer(float sensitivity)
    {
        float newSteerFactor = avoidSpeed * sensitivity;
        carMover.ApplySteer(newSteerFactor);
    }


}
