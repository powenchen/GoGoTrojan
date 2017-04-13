using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalStopArea : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.GetComponent<Car>())
        {
            other.GetComponent<Car>().stopRunning();

        }
    }
}
