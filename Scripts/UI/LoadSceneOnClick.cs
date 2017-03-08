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

		if(PlayerPrefs.GetFloat ("PlayerOne_Health") == 0) PlayerPrefs.SetFloat ("PlayerOne_Health", 100);
		if(PlayerPrefs.GetFloat ("PlayerOne_Mana") == 0) PlayerPrefs.SetFloat ("PlayerOne_Mana", 100);
		if(PlayerPrefs.GetFloat ("PlayerOne_Speed") == 0) PlayerPrefs.SetFloat ("PlayerOne_Speed", 100);
		if(PlayerPrefs.GetFloat ("PlayerOne_SkillCDR") == 0) PlayerPrefs.SetFloat ("PlayerOne_SkillCDR", 100);
		if(PlayerPrefs.GetFloat ("PlayerOne_Power") == 0) PlayerPrefs.SetFloat ("PlayerOne_Power", 100);

		if(PlayerPrefs.GetFloat ("PlayerTwo_Health") == 0) PlayerPrefs.SetFloat ("PlayerTwo_Health", 200);
		if(PlayerPrefs.GetFloat ("PlayerTwo_Mana") == 0) PlayerPrefs.SetFloat ("PlayerTwo_Mana", 200);
		if(PlayerPrefs.GetFloat ("PlayerTwo_Speed") == 0) PlayerPrefs.SetFloat ("PlayerTwo_Speed", 200);
		if(PlayerPrefs.GetFloat ("PlayerTwo_SkillCDR") == 0) PlayerPrefs.SetFloat ("PlayerTwo_SkillCDR", 200);
		if(PlayerPrefs.GetFloat ("PlayerTwo_Power") == 0) PlayerPrefs.SetFloat ("PlayerTwo_Power", 200);

		if(PlayerPrefs.GetFloat ("CarOne_Health") == 0) PlayerPrefs.SetFloat ("CarOne_Health", 100);
		if(PlayerPrefs.GetFloat ("CarOne_Mana") == 0) PlayerPrefs.SetFloat ("CarOne_Mana", 100);
		if(PlayerPrefs.GetFloat ("CarOne_Speed") == 0) PlayerPrefs.SetFloat ("CarOne_Speed", 100);
		if(PlayerPrefs.GetFloat ("CarOne_Power") == 0) PlayerPrefs.SetFloat ("CarOne_Power", 100);

		if(PlayerPrefs.GetFloat ("CarTwo_Health") == 0) PlayerPrefs.SetFloat ("CarTwo_Health", 200);
		if(PlayerPrefs.GetFloat ("CarTwo_Mana") == 0) PlayerPrefs.SetFloat ("CarTwo_Mana", 200);
		if(PlayerPrefs.GetFloat ("CarTwo_Speed") == 0) PlayerPrefs.SetFloat ("CarTwo_Speed", 200);
		if(PlayerPrefs.GetFloat ("CarTwo_Power") == 0) PlayerPrefs.SetFloat ("CarTwo_Power", 200);

		//SceneManager.LoadScene (sceneIndex);
		SceneManager.LoadScene(sceneName);
	}
}