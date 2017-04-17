using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveBtnOnClick : MonoBehaviour {

	public Button MoveBtn;
    public Car car;
    public CarStatus carStatus;
    public Sprite[] moveIcons;
    public Image moveBtnImage;


    // Use this for initialization
    void Awake () {
        moveBtnImage.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (car.ableToUseSkill()) 
        {
            moveBtnImage.enabled = true;
            if (car.GetComponent<N2OSkill>() != null)
            {
                moveBtnImage.sprite = moveIcons[0];
            }
            if (car.GetComponent<TimeStopSkill>() != null)
            {
                moveBtnImage.sprite = moveIcons[1];
            }
            if (car.GetComponent<CoinAttackSkill>() != null)
            {
                moveBtnImage.sprite = moveIcons[2];
            }
            if (car.GetComponent<FlameSkill>() != null)
            {
                moveBtnImage.sprite = moveIcons[3];
            }
            if (car.GetComponent<SpearSkill>() != null)
            {
                moveBtnImage.sprite = moveIcons[4];
            }

            if (car.GetComponent<ShieldSkill>() != null)
            {
                moveBtnImage.sprite = moveIcons[5];
            }
        }
        else {
            moveBtnImage.enabled = false;
        }
	}

	public void moveButtonOnClick () {
        
        if (car.useSkill())
        {
            moveBtnImage.enabled = false;
        }

	}

    public void setPlayerCar(Car c)
    {
        car = c;
        carStatus = c.GetComponent<CarStatus>();
    }
}
