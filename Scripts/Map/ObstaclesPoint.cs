using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPoint : MonoBehaviour {
    public GameObject[] obstacles;
    private float horizontalMargin = 4.5f;
    private float verticalMargin = 2f;

    private float initProb = 0.7f;
	// Use this for initialization
	void Start () {
        for(int i=0;i< 8;++i)
        {
            if (Random.value < initProb)
            {
                Vector3 horizOffset = (i % 4 - 2) * transform.right * horizontalMargin;
                Vector3 vertOffset = Mathf.RoundToInt(i /4) * transform.forward * verticalMargin;
                Vector3 pos = transform.position + horizOffset + vertOffset;
                int rng = Random.Range(0, obstacles.Length);
                Instantiate(obstacles[rng], pos, transform.rotation, transform);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
