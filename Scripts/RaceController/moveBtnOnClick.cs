using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveBtnOnClick : MonoBehaviour {

	public Button MoveBtn;
	public Text MoveBtnText;
    public Car car;


	// Use this for initialization
	void Start () {
		if (MoveBtnText != null) {
            MoveBtnText.text = "";
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (car.getMP() == car.getMaxMP())
        {
            if (car.GetComponent<N2OSkill>() != null)
            {
                MoveBtnText.text = "NITRO";
            }
            if (car.GetComponent<TimeStopSkill>() != null)
            {
                MoveBtnText.text = "TIME";
            }
        }
        else {

            MoveBtnText.text = "";
        }
	}

	public void moveButtonOnClick () {
        
		MoveBtnText.text = "";
        car.useSkill();

	}

    public void setPlayerCar(Car c)
    {
        car = c;
    }
}
