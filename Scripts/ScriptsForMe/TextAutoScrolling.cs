using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAutoScrolling : MonoBehaviour
{
    public float startPos = -685;
    public float endPos = -66;
    public float scrollTime = 12;
    private float timer = 0;
    // Use this for initialization
    void Start () {
        Vector3 prevPos = GetComponent<RectTransform>().anchoredPosition3D;
        GetComponent<RectTransform>().anchoredPosition3D = new Vector3(prevPos.x, startPos, prevPos.z);
        
    }
	
	// Update is called once per frame

	void Update () {

        if (timer < scrollTime)
        {
            float speed = (endPos - startPos) / scrollTime;
            float dT = Time.deltaTime;
            Vector3 prevPos = GetComponent<RectTransform>().anchoredPosition3D;
            float yT = startPos + speed * timer;
            GetComponent<RectTransform>().anchoredPosition3D = new Vector3(prevPos.x, yT, prevPos.z);
            timer += dT;
        }
	}
}
