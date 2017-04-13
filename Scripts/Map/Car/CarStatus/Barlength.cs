using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barlength : MonoBehaviour {
    public bool isHPBar ;
    private RectTransform rectTransform;
    private Car car;
    private CarStatus carStatus;
    // Use this for initialization
    void Start () {
        rectTransform = GetComponent<RectTransform>();
        car = GetComponentInParent<Car>();
        carStatus = GetComponentInParent<CarStatus>();
    }
	
	// Update is called once per frame
	void Update () {
        float maxWidth = 2;
        float width = maxWidth;
        if (car != null)
        {
            if (isHPBar)
            {
                width *= (carStatus.getHP() / carStatus.getMaxHP());
            }
            else
            {
                width *= (carStatus.getMP() / carStatus.getMaxMP());
            }
        }
        width = Mathf.Clamp(width, 0, 2);
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }

}
