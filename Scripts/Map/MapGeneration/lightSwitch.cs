using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSwitch : MonoBehaviour {
    private Light lightSource;
    private void OnDrawGizmos()
    {
        LightSwitch();
    }
    // Use this for initialization
    void Start () {
        LightSwitch();
    }


	
	// Update is called once per frame
	void Update () {
    }

    void LightSwitch()
    {
        bool lightOn = !FindObjectOfType<Skybox>().material.name.StartsWith("Sunny");
        lightSource = GetComponentInChildren<Light>();
        if (lightSource != null)
        {
            lightSource.enabled = lightOn;
        }
    }
}
