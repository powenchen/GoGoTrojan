using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadBar : MonoBehaviour
{
    public RectTransform backGround;
    public float progress = 0;
    public float progressSpeed = 30;
    private float maxProgressSpeed = 30;
    private float maxProgress = 100;
    private float progressThres;
    // Use this for initialization
    void Start () {
        progressThres = 0.9f * maxProgress;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float width = backGround.sizeDelta.x * (progress / maxProgress);
        float height = backGround.sizeDelta.y;
        GetComponent<RectTransform>().sizeDelta = new Vector2(width,height);
        if (progress < progressThres)
        {
            GrowProgress();
        }
        

    }

    void GrowProgress()
    {
        progress += Time.deltaTime * progressSpeed*(Random.value);

    }
}
