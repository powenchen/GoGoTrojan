using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Car car;

	// Use this for initialization
	void Start () {
        car = GetComponent<Car>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void FixedUpdate()
    {
        

        if (Input.GetKeyDown(KeyCode.N))
        {
            car.useSkill();
        }
        
        float motorTorqueFactor = 1;// * Input.GetAxis("Vertical");
        float brakeTorqueFactor = 0;//Input.GetAxis("Jump");

        car.ApplyPedal(motorTorqueFactor, brakeTorqueFactor);

        


        float steerFactor = Input.GetAxis("Horizontal");   // Input.acceleration.x;

        car.ApplySteer(steerFactor);

    }

}
