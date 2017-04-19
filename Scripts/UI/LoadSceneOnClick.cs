using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadBySceneName(string sceneName) {
		//SceneManager.LoadScene (sceneIndex);
		SceneManager.LoadScene(sceneName);
	}
}