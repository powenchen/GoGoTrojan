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
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Car>().stopRunning();

        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Car>().stopRunning();

        }
    }
}
