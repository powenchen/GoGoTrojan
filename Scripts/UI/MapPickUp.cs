using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapPickUp : MonoBehaviour {

	public Button confirmText;
	public Button backText;
	public Text title;
	public Text value;
	public Image routePreview;
	public static int routePicked;
	public Sprite star;
	public GameObject starsPan;
	public Button[] maps;
    public Sprite[] miniMaps;
    public Sprite[] sMaps;
    private string[] titles = {"D o w n t o w n  L A", "V i t e r b i", "M a r s h a l l", "K e c k", "C i n e m a", "G o u l d", "T r o j a n" };
	private int[] difficulty = { 1, 2, 1, 3, 4, 3, 5 };
	private string[] distance = { "Medium", "Long", "Short", "Short", "Medium", "Short", "Long" };
    public Sprite lockSprite;
    // Use this for initialization
    void Start () {
        for (int i = 0; i < maps.Length; ++i)
        {
            if (i > StaticVariables.saveData["progress"].n + 1)
            {
                maps[i].GetComponent<Image>().sprite = lockSprite;
                maps[i].interactable = false;
            }
            else
            {
                maps[i].GetComponent<Image>().sprite = sMaps[i];
                maps[i].interactable = true;
            }

        }
        confirmText = confirmText.GetComponent<Button> ();
		backText = backText.GetComponent<Button> ();
		routePreview = routePreview.GetComponent<Image> ();
		routePicked = 0;
		ChangeMapDetail (routePicked);
	}
	
	// Update is called once per frame
	void Update () {
	}



	public void Confirm() {
        //PlayerPrefs.SetInt("CourseID", routePicked-1);
        StaticVariables.mapID = routePicked;

        SceneManager.LoadScene ("CharacterPickUp");
	}

	public void GoBack() {
		SceneManager.LoadScene ("Story");
	}

	public void PickRouteAt(int mapNumber) {
		routePicked = mapNumber;
		ChangeMapDetail (routePicked);
	}

	private void ShowStarsFor(int number) {
		for(int i = 0; i < 5; i++) {
			starsPan.GetComponentsInChildren<Image> () [i].sprite = null;
		}
		for(int i = 0; i < number; i++) {
			starsPan.GetComponentsInChildren<Image> () [i].sprite = star;
		}
	}

	private void ChangeMapDetail(int mapNumber) {
		title.text = titles[mapNumber];
		routePreview.sprite = miniMaps [mapNumber];
		ShowStarsFor (difficulty[mapNumber]);
		string valueStr = distance[mapNumber] + "\n";

		valueStr += StaticVariables.GetMaxRecordOfMap(mapNumber);
		value.text = valueStr;
	}

}
