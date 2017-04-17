using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour {
    //private miniMapMark[] markList;
    //private int playerMarkIdx;
    private Camera minimapCam;
    public Material[] materials;

    private void Start()
    {
        GetComponent<Renderer>().material = materials[StaticVariables.mapID];
    }

    // Update is called once per frame
    void Update ()
    {
    }

    public void setCamTarget(Transform player)
    {
        minimapCam = GetComponentInChildren<Camera>();
        //markList = GetComponentsInChildren<miniMapMark>();
        minimapCam.gameObject.GetComponent<MiniMapCameraFollow>().setTarget(player);
    }
    
}
