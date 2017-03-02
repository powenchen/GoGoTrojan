using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagChange : MonoBehaviour {
    public string tagString = "";
    
    private void OnDrawGizmos()
    {
        
        if (tagString.Length > 0)
        {
            Transform[] chilfdrenTransform = GetComponentsInChildren<Transform>();


            foreach (Transform t in chilfdrenTransform)
            {
                t.gameObject.tag = tagString;
            }
            tagString = "";
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
