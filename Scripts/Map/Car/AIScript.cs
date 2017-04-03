using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// test modification

public class AIScript : MonoBehaviour {
    public bool skillDebugFlag = false;
    private float distThreshold = 10;

    private int lookAhead = 3;
    public PathManager path;
    private Transform[] pathPoints;
    public int pathPointIdx = 1; // start from 1 since idx = 0 is parent component 
    
    private Rigidbody rb;

    private float sensorLength = 15;
    private float sideSensorLength = 5;
    private float frontSensorStartPoint = 3;
    private float frontSensorMargin = 1;
    private float frontSensorAngle = 15;
    private float avoidSpeed = 1;
    private int flag = 0;
    private bool reversing = false;
    private float reverseTimer = 0;
    private float waitToReverse = 1.5f;
    private float reverseFor = 1.5f;
    private float stuckThreshlod = 10 * 1000 / 3600;//(10 km/h)
    private bool pauseFlag = false;
    private Car car;


    // Use this for initialization
    void Start () {
        if (GetComponent<TimeStopSkill>() != null)
        {
            lookAhead *= 2;
        }
        car = GetComponent<Car>();
        rb = GetComponent<Rigidbody>();
        if (path!=null)
        {
            pathPoints = path.gameObject.GetComponentsInChildren<Transform>();
        }
        

    }

    private void Update()
    {

    }

    public void PauseAI()
    {
        pauseFlag = true;
    }

    public void resumeAI()
    {
        pauseFlag = false;
    }

    private void FixedUpdate()
    {
        if (pauseFlag)
        {
            return;
        }
        if(skillDebugFlag)
            car.useSkill();
        //Debug.Log("speed = " + rb.velocity.magnitude);

        //Debug.Log("steer = "+frontLeft.GetComponent<WheelCollider>().steerAngle + ", motor torque = "+ frontLeft.GetComponent<WheelCollider>().motorTorque);

        if (pathPointIdx >= pathPoints.Length)
        {
            car.stopRunning();
            return;
        }
        float minDist = (pathPoints[pathPointIdx].position - transform.position).magnitude;
        int bestI = pathPointIdx;
        for (int i = pathPointIdx + 1; i < Mathf.Min(pathPointIdx+ lookAhead, pathPoints.Length) ; ++i)
        {
            float distI = (pathPoints[i].position - transform.position).magnitude;
            if (distI < minDist)
            {
                bestI = i;
                minDist = distI;
            }

        }
        if(pathPointIdx!= bestI)
        { 
            pathPointIdx = bestI;
            Debug.Log("AI skipped to point"+ bestI);
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
            //Debug.Log("AI passed point" + pathPointIdx + " " + pathPoints[pathPointIdx].position.ToString());
            ++pathPointIdx;
        }

        float newSteerFactor =  (steerVector.x / steerVector.magnitude); // -1< newSteerFactor <1
        if (steerVector.magnitude == 0)
            newSteerFactor = 0;

        float newMotorTorqueFactor = 1 - 0.5f*Mathf.Abs(newSteerFactor);
        //Debug.Log("newMotorTorqueFactor = " + newMotorTorqueFactor);

        if (reversing)
        {
            newMotorTorqueFactor *= -1;

            Debug.Log("reversing; newMotorTorqueFactor = " + newMotorTorqueFactor);
        }

        if (flag == 0)
        {
            car.ApplySteer(newSteerFactor);
        }
        car.ApplyThrottle(newMotorTorqueFactor);


       Sensor();
    }


    

    public void Sensor()
    {
        flag = 0;
        float avoidSensitivity = 0;
        Vector3 pos;
        pos = transform.position;
        pos += transform.forward * frontSensorStartPoint;
        RaycastHit hit = new RaycastHit();
        
        //braking sensor
        if (triggersSensor(pos, transform.forward,hit,  sensorLength))
        {
            ++flag;
            if (!reversing)
            {
                car.ApplyBrake(0.3f);
            }
            Debug.DrawLine(pos, hit.point, Color.red);
            //skillDebugFlag = false;
        }
        else
        {
            car.ApplyBrake(0);
            //skillDebugFlag = true;
        }
        

        //front right sensor and right angled sensor
        pos = transform.position;
        pos += transform.forward * frontSensorStartPoint + transform.right*frontSensorMargin;
        Vector3 sensorAngle = Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward;
        if (triggersSensor(pos, transform.forward,  hit, sensorLength))
        {
            ++flag;
            avoidSensitivity -= 1;
            Debug.DrawLine(pos, hit.point, Color.white);
        }
        else if (triggersSensor(pos, sensorAngle, hit, sensorLength))
        {
            ++flag;
            avoidSensitivity -= 0.5f;
            Debug.DrawLine(pos, hit.point, Color.white);
        }


        //front left sensor
        pos = transform.position;
        pos += transform.forward * frontSensorStartPoint - transform.right * frontSensorMargin;

        sensorAngle = Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward;
        if (triggersSensor(pos, transform.forward, hit, sensorLength))
        {
            ++flag;
            avoidSensitivity += 1;
            Debug.DrawLine(pos, hit.point, Color.white);
        }
        else if (triggersSensor(pos, sensorAngle, hit, sensorLength))
        {
            ++flag;
            avoidSensitivity += 0.5f;
            Debug.DrawLine(pos, hit.point, Color.white);
        }

        //right side sensor
        if (triggersSensor(transform.position, transform.right, hit, sideSensorLength))
        {
            ++flag;
            avoidSensitivity -= 0.5f;
            Debug.DrawLine(transform.position, hit.point, Color.white);
        }

        //left side sensor
        if (triggersSensor(transform.position, -transform.right, hit, sideSensorLength))
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
            if (triggersSensor(pos, transform.forward, hit, sensorLength))
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
        car.ApplySteer(newSteerFactor);
    }

    private bool triggersSensor(Vector3 pos, Vector3 dir, RaycastHit hit, float length)
    {
        if (Physics.Raycast(pos, dir, out hit, length) && hit.transform.GetComponent<CarCheckPoint>() == null && !hit.transform.CompareTag("Terrain") && !hit.transform.GetComponent<Collider>().isTrigger )
        {
            return true;
        }
        return false;
    }

}
