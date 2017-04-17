using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingSystem : MonoBehaviour {
    
	public Text rankingText;
    private Dictionary<Car, int> carDistMapping = new Dictionary<Car, int>(); // the higher dist -> the higher rank
    private int totalCarNum;
    // Use this for initialization
    void Start () {
        Car[] cars = FindObjectsOfType<Car>();
        totalCarNum = cars.Length;
        foreach (Car car in cars)
        {
            if(!carDistMapping.ContainsKey(car))
                carDistMapping.Add(car, -1);
        }
		rankingText.text = "";

        rankingText.color = (FindObjectOfType<Skybox>().material.name.StartsWith("Sunny")) ? Color.black : Color.white;
    }
	
	// Update is called once per frame
	void Update () {
        rankingText.text = Mathf.Clamp(GetMyRank(),1, totalCarNum).ToString() +  "/" + totalCarNum.ToString();
    }

	public int GetMyRank() {

        foreach (Car car in FindObjectsOfType<Car>())
        {
            if (!carDistMapping.ContainsKey(car))
                carDistMapping.Add(car, -1);
        }
        int myDist = 0;

        Car[] cars = FindObjectsOfType<Car>();
        totalCarNum = FindObjectsOfType<AIScript>().Length + 1; //self + rest ai
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
        if(StaticVariables.ranking != 999)//leave, die 
            StaticVariables.ranking = myRank;
        return myRank;
    }

    public void SetCarDist(Car car, int dist)
    {
        if (!carDistMapping.ContainsKey(car))
            carDistMapping.Add(car, -1);
        if (carDistMapping[car] < dist)
        {
            carDistMapping[car] = dist;
            car.setRespawnIdx(dist);
        }
    }

    public int GetCarDist(Car car)
    {
        return carDistMapping[car];
    }

    public int GetCarRanking(Car car)
    {
        int carDist = carDistMapping[car];

        Car[] cars = FindObjectsOfType<Car>();
        int carRank = 1;

        foreach (Car c in cars)
        {
            if (carDistMapping[c] >= carDist && !car.Equals(c))
            {
                ++carRank;
            }
        }
        return carRank;
    }

    

    public Car GetFirstPlaceCar(Car exception = null)//skip exception
    {
        int dist = -1;
        Car ret = null;
        foreach (Car car in FindObjectsOfType<Car>())
        {
            if (carDistMapping[car] > dist && car != exception)
            {
                dist = carDistMapping[car];
                ret = car;
            }
        }
        return ret;
    }


    
}
