using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCompleteUI : MonoBehaviour {
    
    public GameObject startVideo;
    // Use this for initialization
    void Start () {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            startVideo.SetActive(true);
            startVideo.GetComponent<VideoPlayer>().Restart();
            gameObject.SetActive(false);
        }
    }
}
