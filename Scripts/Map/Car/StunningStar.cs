using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunningStar : MonoBehaviour {
    public float rotateSpeed = 720.0f; //degrees per second
    public float rotateTime = 1.0f;
    public float lifetime = 1.0f;
    private float timer = 0.0f;
    // Use this for initialization
    void Start () {
        StartCoroutine(rotateAngleInSecond(transform, Vector3.up * rotateSpeed, rotateTime));
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator rotateAngleInSecond(Transform transform, Vector3 rotateSpeed, float inTime)
    {
        
        float rotateFrames = inTime / Time.deltaTime;
        Vector3 step = rotateSpeed * Time.deltaTime;
        for (int t = 0; t < rotateFrames; t++)
        {
            transform.Rotate(step);
            yield return null;
        }

    }

    public void setLifetime(float t)
    {
        lifetime = t;
    }
}
