using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private mover carMover;

	// Use this for initialization
	void Start () {
        carMover = GetComponent<mover>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            carMover.ApplyN2O();
        }
        
        float motorTorqueFactor = 1;// * Input.GetAxis("Vertical");
        float brakeTorqueFactor = Input.GetAxis("Jump");

        carMover.ApplyPedal(motorTorqueFactor, brakeTorqueFactor);

        


        float steerFactor = Input.GetAxis("Horizontal");// Input.acceleration.x;

        carMover.ApplySteer(steerFactor);

    }
}
