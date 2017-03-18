using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{

    public Transform target;

    // Use this for initialization
    void Start()
    {
        transform.position =
            new Vector3(
            target.position.x,
            transform.position.y,
            target.position.z
            );

    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
            new Vector3(
            target.position.x,
            transform.position.y,
            target.position.z
            );

    }

    public void setTarget(Transform t)
    {
        target = t;
    }
}
