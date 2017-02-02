using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour
{
    public float forwardSpeed ;
    public float backwardSpeed ;
    public float rotateSpeed ;

    // Use this for initialization 
    void Start()
    {

    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position -= Quaternion.Euler(0, -90, 0) *(transform.forward * forwardSpeed )* Time.deltaTime;
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
        }
       
    }
}