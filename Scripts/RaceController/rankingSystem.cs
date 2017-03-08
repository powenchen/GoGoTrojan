using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rankingSystem : MonoBehaviour {
    
	public Text rankingText;
    private Dictionary<Car, int> carDistMapping = new Dictionary<Car, int>(); // the higher dist -> the higher rank
    private int totalCarNum;
    // Use this for initialization
    void Start () {
        Car[] cars = FindObjectsOfType<Car>();
        totalCarNum = cars.Length;
        foreach (Car car in cars)
        {
            carDistMapping.Add(car, -1);
        }
		rankingText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

        

        rankingText.text = getMyRank().ToString() +  "/" + totalCarNum.ToString();
    }

	public int getMyRank() {
        int myDist = 0;

        Car[] cars = FindObjectsOfType<Car>();
        foreach (Car car in cars)
        {
            if (car.GetComponent<PlayerController>() != null)
            {
                myDist = carDistMapping[car];
            }
        }

        int myRank = 1;

        foreach (Car car in cars)
        {
            if (car.GetComponent<AIScript>() != null && carDistMapping[car] >= myDist)
            {
                ++myRank;
            }
        }
        PlayerPrefs.SetInt("Ranking",myRank);
        return myRank;
    }

    public void setCarDist(Car car, int dist)
    {
        carDistMapping[car] = dist;
    }
    
}
