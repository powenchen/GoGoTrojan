using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNitrogen : MonoBehaviour {
    public float magnitude = 0.001f;
    private Transform parentTransform;
	// Use this for initialization
    public float upPos = 2.5f, backPos = -8f;
	void Start () {
        transform.localPosition = new Vector3(0, upPos, backPos);
    }
	
	// Update is called once per frame
	void Update () {
    }
    public IEnumerator Shake(float duration)
    {
        
        transform.localPosition = new Vector3(0, upPos, backPos*0.5f);
        yield return new WaitForSeconds(duration);
        transform.position = new Vector3(0, upPos, backPos);
    }
}
