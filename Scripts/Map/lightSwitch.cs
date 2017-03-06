using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSwitch : MonoBehaviour {
    private Light lightSource;
    public bool lightOn = true;
    private void OnDrawGizmos()
    {
        lightSource = GetComponentInChildren<Light>();
        if (lightSource != null)
        {
            lightSource.enabled = lightOn;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
