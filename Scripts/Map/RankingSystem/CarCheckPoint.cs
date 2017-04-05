using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarCheckPoint : MonoBehaviour {
    public RankingSystem ranking;
    public int dist;

	public void OnTriggerEnter(Collider other) {
		if (!ranking) {
			ranking = FindObjectOfType<RankingSystem> ();
		}
        if(other.GetComponent<Car>() != null)
        {
            ranking.SetCarDist(other.GetComponent<Car>(), dist);
        }
	}
    
		
}
