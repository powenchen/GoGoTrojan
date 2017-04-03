using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveBtnOnClick : MonoBehaviour {

	public Button MoveBtn;
	public Text MoveBtnText;
    public Car car;
    public CarStatus carStatus;


    // Use this for initialization
    void Awake () {

        if (MoveBtnText != null) {
            MoveBtnText.text = "";
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (carStatus.getMP() == carStatus.getMaxMP())
        {
            if (car.GetComponent<N2OSkill>() != null)
            {
                MoveBtnText.text = "NITRO";
            }
            if (car.GetComponent<TimeStopSkill>() != null)
            {
                MoveBtnText.text = "TIME";
            }
            if (car.GetComponent<CoinAttackSkill>() != null)
            {
                MoveBtnText.text = "CASH CHUCKER";
            }
            if (car.GetComponent<FlameSkill>() != null)
            {
                MoveBtnText.text = "DRAGON'S BREATH";
            }
        }
        else {

            MoveBtnText.text = "";
        }
	}

	public void moveButtonOnClick () {
        
        if (car.useSkill())
        {
            MoveBtnText.text = "";
        }

	}

    public void setPlayerCar(Car c)
    {
        car = c;
        carStatus = c.GetComponent<CarStatus>();
    }
}
