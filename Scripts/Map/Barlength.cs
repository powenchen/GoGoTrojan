using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barlength : MonoBehaviour {
    public bool isHPBar ;
    private RectTransform rectTransform;
    private Car car;
    // Use this for initialization
    void Start () {
        rectTransform = GetComponent<RectTransform>();
        car = GetComponentInParent<Car>();
	}
	
	// Update is called once per frame
	void Update () {
        float width = 2;
        if (car != null)
        {
            if (isHPBar)
            {
                width *= (car.getHP() / car.getMaxHP());
            }
            else
            {
                width *= (car.getMP() / car.getMaxMP());
            }
        }
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }

}
