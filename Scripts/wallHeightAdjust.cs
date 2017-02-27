using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallHeightAdjust : MonoBehaviour {
    public WallCreate leftWall, rightWall;
    public float leftHeight = 8, rightHeight = 8;
    // Use this for initialization
    private void OnDrawGizmos()
    {

        leftWall.height = leftHeight;
        rightWall.height = rightHeight;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
