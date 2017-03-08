using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapPickUp : MonoBehaviour {

	public Button confirmText;
	public Button backText;
	public Button route1;
	public Button route2;
	public Image routePreview;
	public static int routePicked;
	public Sprite routePreview1;
	public Sprite routePreview2;
	public Text routePreviewName;

	// Use this for initialization
	void Start () {
		confirmText = confirmText.GetComponent<Button> ();
		backText = backText.GetComponent<Button> ();
		route1 = route1.GetComponent<Button> ();
		route2 = route2.GetComponent<Button> ();
		routePreview = routePreview.GetComponent<Image> ();
		routePreviewName = routePreviewName.GetComponent<Text> ();
		routePreview.sprite = routePreview1;
		routePicked = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Confirm() {
        PlayerPrefs.SetInt("CourseID", routePicked-1);
		SceneManager.LoadScene ("CharacterPickUp");
	}

	public void GoBack() {
		SceneManager.LoadScene ("Story");
	}

	public void PickRoute1() {
		routePicked = 1;
		Debug.Log ("You have picked route 1");
		route1.GetComponent<Outline> ().enabled = true;
		route2.GetComponent<Outline> ().enabled = false;
		routePreview.sprite = routePreview1;
		routePreviewName.text = "Route 1";

	}

	public void PickRoute2() {
		routePicked = 2;
		Debug.Log ("You have picked route 2");
		route2.GetComponent<Outline> ().enabled = true;
		route1.GetComponent<Outline> ().enabled = false;
		routePreview.sprite = routePreview2;
		routePreviewName.text = "Route 2";
	}


}
