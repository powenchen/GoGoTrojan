using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInitializer : MonoBehaviour {
    public bool debugInit = false;
	// Use this for initialization
	void Awake () {
        Load.initialize(debugInit);
        Debug.Log("initialize here and only here");
	}
	
}
