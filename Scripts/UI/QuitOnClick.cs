using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuitOnClick : MonoBehaviour {

	//public Canvas quitMenu;
	public Button StartButton;
	public Button OptionButton;
	public Button AboutButton;
	public Button ExitButton;


	public void Start() {
		StartButton = StartButton.GetComponent<Button> ();
		OptionButton = OptionButton.GetComponent<Button> ();
		AboutButton = AboutButton.GetComponent<Button> ();
		ExitButton = ExitButton.GetComponent<Button> ();
	}

	public void DisableButtons() {
		StartButton.enabled = false;
		OptionButton.enabled = false;
		AboutButton.enabled = false;
		ExitButton.enabled = false;
	}

	public void EnableButtons() {
		StartButton.enabled = true;
		OptionButton.enabled = true;
		AboutButton.enabled = true;
		ExitButton.enabled = true;

		//Debug.Log("adfasf");
	}

	public void Quit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}