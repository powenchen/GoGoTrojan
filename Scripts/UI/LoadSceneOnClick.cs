using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadBySceneName(string sceneName) {

		PlayerPrefs.SetInt ("PlayerID", 1);
		PlayerPrefs.SetInt ("CarID", 1);
		PlayerPrefs.SetInt ("CourseID", 1);
		PlayerPrefs.SetInt ("TotalCoins", PlayerPrefs.GetInt("TotalCoins"));
		PlayerPrefs.SetString ("TotalTime", "none");

		PlayerPrefs.SetFloat ("PlayerOne_Health", 150);
		PlayerPrefs.SetFloat ("PlayerOne_Mana", 100);
		PlayerPrefs.SetFloat ("PlayerOne_Speed", 300);
		PlayerPrefs.SetFloat ("PlayerOne_SkillCDR", 180);
		PlayerPrefs.SetFloat ("PlayerOne_Power", 90);

		PlayerPrefs.SetFloat ("PlayerTwo_Health", 140);
		PlayerPrefs.SetFloat ("PlayerTwo_Mana", 120);
		PlayerPrefs.SetFloat ("PlayerTwo_Speed", 280);
		PlayerPrefs.SetFloat ("PlayerTwo_SkillCDR", 200);
		PlayerPrefs.SetFloat ("PlayerTwo_Power", 100);

		PlayerPrefs.SetFloat ("CarOne_Health", 120);
		PlayerPrefs.SetFloat ("CarOne_Mana", 90);
		PlayerPrefs.SetFloat ("CarOne_Speed", 80);
		PlayerPrefs.SetFloat ("CarOne_Power", 100);

		PlayerPrefs.SetFloat ("CarTwo_Health", 200);
		PlayerPrefs.SetFloat ("CarTwo_Mana", 100);
		PlayerPrefs.SetFloat ("CarTwo_Speed", 50);
		PlayerPrefs.SetFloat ("CarTwo_Power", 70);

		//SceneManager.LoadScene (sceneIndex);
		SceneManager.LoadScene(sceneName);
	}
}