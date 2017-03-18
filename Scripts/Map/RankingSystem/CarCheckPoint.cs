using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarCheckPoint : MonoBehaviour {
    public rankingSystem ranking;
    public int dist;

	public void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Car>() != null)
        {
            ranking.setCarDist(other.GetComponent<Car>(), dist);
        }
	}
    
		
}
