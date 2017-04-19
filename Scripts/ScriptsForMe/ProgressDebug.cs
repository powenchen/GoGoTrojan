using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressDebug : MonoBehaviour {
    [Range (-1,7)]
    public int progress;
    public bool debug = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            StaticVariables.SetProgress( progress);
            debug = false;
        }
    }
}
