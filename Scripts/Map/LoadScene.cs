using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    public string nextSceneName;
	// Use this for initialization
	void Start () {
        if (nextSceneName != "")
        {
            SceneManager.LoadSceneAsync(nextSceneName);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadSceneByName(string name)
    {

        SceneManager.LoadSceneAsync(name);
    }
}
