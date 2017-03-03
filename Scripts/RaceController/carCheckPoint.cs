using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carCheckPoint : MonoBehaviour {

	public Text test;

	public int currentCheckPoint;		
	public int currentLap;
	public Transform lastposition;		// transform of the last point the car passed

	public int numOfCheckPoint;			// total number of checkpoints, @remember to check this number in gameobject component@

	public carCheckPoint[] cars;

	private static int CHECKPOINT_VALUE = 100;
	private static int LAP_VALUE = 10000;
	private int checkPointCount = 0;

	void Start () {
		test.text = "";
		currentCheckPoint = 0;
		currentLap = 0;
		checkPointCount = 0;
		lastposition = GameObject.Find ("Goal").transform;

		//test code
		InvokeRepeating ("GetCarPosition", 0.5f, 0.5f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other) {

		string otherTag = other.gameObject.tag;

		// if the car collides with checkpoints..
		if (otherTag.Contains("00")) {

			currentCheckPoint = System.Convert.ToInt32 (otherTag);

			// completed a lap && passed all checkpoints, recount checkPointCount.
			if (currentCheckPoint == 1 && checkPointCount == numOfCheckPoint) {
				currentLap++;
				checkPointCount = 0;
			}

			if (checkPointCount == currentCheckPoint - 1) {
				lastposition = other.transform;
				checkPointCount++;
			}
		}
	}

	public float GetDistance() {
		return (transform.position - lastposition.position).magnitude + currentCheckPoint * CHECKPOINT_VALUE + currentLap * LAP_VALUE;
	}
		
	public int GetCarPosition() {
		float distance = GetDistance();
		int position = 1;
		foreach (carCheckPoint car in cars) {
			if (car.GetDistance() > distance)
				position++;
		}

		// for test
		test.text = "Rank:" + position + "\ndistance: " + distance;

		return position;
	}
		
}
