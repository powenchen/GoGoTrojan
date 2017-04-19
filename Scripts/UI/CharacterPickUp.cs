using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterPickUp : MonoBehaviour {

	public Button confirmText;
	public Button backText;
	public Button storeText;
	public Button nextCharacterText;
	public Button previousCharacterText;
	public Text title;
	public Text values;
	public Text level;
	public Text letterGrade;
	public Image img;
	public Image skillImage;
	public Button[] charList;
	public Sprite[] spriteList;
	public Sprite[] skillList;
	public GameObject expBar;
	public GameObject HPBar;
	public GameObject MPBar;
	public GameObject speedBar;
	public GameObject CDBar;
	public GameObject attackBar;
	public GameObject defenseBar;
	public GameObject hiddenChar;
	private Color32 cyan;
	private Color32 white;


	public static int characterPicked;

	public CameraMover MyCameraMover; 

	// Use this for initialization
	void Start () {

		//Debug.Log (Application.persistentDataPath);


		Load.initialize ();
		for(int i = 0; i < charList.Length; i++) {
			JSONObject character = StaticVariables.GetCharacterAttribute (i);
			if (!character.GetField ("unlocked").b) {
				if (i == 5) {
					// charList [i].GetComponentInParent<GameObject> ().SetActive (false);	// Does not work
					hiddenChar.SetActive (false);
				} else {
					DisableButton (charList [i]);
				}
			}
		}

		cyan = new Color32 (0, 244, 255, 255);
		white = new Color32 (255, 255, 255, 255);
		characterPicked = 0;
		ChangeCharacterDetail (characterPicked);


	}

	public void DisableButton(Button button) {
		button.GetComponent<Image> ().color = new Color32(60, 60, 60, 255);
		button.GetComponentsInChildren<Image> ()[1].color = new Color32 (255, 255, 255, 255);	// show the lock image
		button.enabled = false;
		
	}

	public void EnableButton(Button button) {
		button.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Confirm() {
        //PlayerPrefs.SetInt ("PlayerID", characterPicked);
        StaticVariables.characterID = characterPicked;

        SceneManager.LoadScene ("CarPickUp");
	}

	public void Goback() {
		SceneManager.LoadScene ("MapPickUp");
	}

	public void Store() {
		SceneManager.LoadScene ("CharacterAttributes");
	}

    public void PickCharacter(int charNum)
    {
        charList[charNum].GetComponentsInParent<Image>()[1].color = white;
        characterPicked = charNum;
        img.sprite = spriteList[charNum];
        //ChangeCharacterDetail(charNum);
        this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(charNum, 1f));
    }

	public void PickViterbiAnna() {
		// change the frame color of last picked car back to white
		charList[characterPicked].GetComponentsInParent<Image>()[1].color = white;
		characterPicked = 0;
		img.sprite = spriteList [characterPicked];
		ChangeCharacterDetail (characterPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(characterPicked, 1f));
	}
	public void PickMarshallMary() {
		// change the frame color of last picked car back to white
		charList[characterPicked].GetComponentsInParent<Image>()[1].color = white;
		characterPicked = 1;
		img.sprite = spriteList [characterPicked];
		ChangeCharacterDetail (characterPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(characterPicked, 1f));

	}
	public void PickKeckLily() {
		// change the frame color of last picked car back to white
		charList[characterPicked].GetComponentsInParent<Image>()[1].color = white;
		characterPicked = 2;
		img.sprite = spriteList [characterPicked];
		ChangeCharacterDetail (characterPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(characterPicked, 1f));
	}
	public void PickCinemaPat() {
		// change the frame color of last picked car back to white
		charList[characterPicked].GetComponentsInParent<Image>()[1].color = white;
		characterPicked = 3;
		img.sprite = spriteList [characterPicked];
		ChangeCharacterDetail (characterPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(characterPicked, 1f));
	}
	public void PickGouldJonh() {
		// change the frame color of last picked car back to white
		charList[characterPicked].GetComponentsInParent<Image>()[1].color = white;
		characterPicked = 4;
		img.sprite = spriteList [characterPicked];
		ChangeCharacterDetail (characterPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(characterPicked, 1f));
	}
	public void PickTrojanAlex() {
		// change the frame color of last picked car back to white
		charList[characterPicked].GetComponentsInParent<Image>()[1].color = white;
		characterPicked = 5;
		img.sprite = spriteList [characterPicked];
		ChangeCharacterDetail (characterPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(characterPicked, 1f));
	}

	// Depricated
//	public void NextCharacter() {
//		characterPicked = 1;
//		ChangeCharacterDetail (characterPicked);
//		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(1, 0.5f));
//		nextCharacterText.enabled = false;
//		previousCharacterText.enabled = true;
//	}
//
//	public void PreviousCharacter() {
//		characterPicked = 0;
//		ChangeCharacterDetail (characterPicked);
//		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(0, 0.5f));
//		nextCharacterText.enabled = true;
//		previousCharacterText.enabled = false;
//	}

	public void ChangeCharacterDetail(int characterNumber) {
        PickCharacter(characterNumber);
        //		float player1HP = PlayerPrefs.GetFloat ("PlayerOne_Health");
        //		float player1MP = PlayerPrefs.GetFloat ("PlayerOne_Mana");
        //		float player1Speed = PlayerPrefs.GetFloat ("PlayerOne_Speed");
        //		float player1Power = PlayerPrefs.GetFloat ("PlayerOne_Power");
        //
        //		float player2HP = PlayerPrefs.GetFloat ("PlayerTwo_Health");
        //		float player2MP = PlayerPrefs.GetFloat ("PlayerTwo_Mana");
        //		float player2Speed = PlayerPrefs.GetFloat ("PlayerTwo_Speed");
        //		float player2Power = PlayerPrefs.GetFloat ("PlayerTwo_Power");
        //
        //		switch (characterNumber) {
        //		case 0:
        //			title.text = "A n n a";
        //			values.text = player1HP + " pts\n" + player1MP + " pts\n" + player1Power + " N\n" + player1Speed + " m/hr\n" + "N2O";
        //			letterGrade.text = "B\nC\nC\nC\nB";
        //			break;
        //		case 1:
        //			title.text = "J o h n";
        //			values.text = player2HP + " pts\n" + player2MP + " pts\n" + player2Power + " N\n" + player2Speed + " m/hr\n" + "Time Stop";
        //			letterGrade.text = "C\nB\nB\nB\nA";
        //			break;
        //		case 2:
        //			title.text = "J o h n";
        //			values.text = player2HP + " pts\n" + player2MP + " pts\n" + player2Power + " N\n" + player2Speed + " m/hr\n" + "Time Stop";
        //			letterGrade.text = "C\nB\nB\nB\nA";
        //			break;
        //		case 3:
        //			title.text = "J o h n";
        //			values.text = player2HP + " pts\n" + player2MP + " pts\n" + player2Power + " N\n" + player2Speed + " m/hr\n" + "Time Stop";
        //			letterGrade.text = "C\nB\nB\nB\nA";
        //			break;
        //		case 4:
        //			title.text = "J o h n";
        //			values.text = player2HP + " pts\n" + player2MP + " pts\n" + player2Power + " N\n" + player2Speed + " m/hr\n" + "Time Stop";
        //			letterGrade.text = "C\nB\nB\nB\nA";
        //			break;
        //		case 5:
        //			title.text = "J o h n";
        //			values.text = player2HP + " pts\n" + player2MP + " pts\n" + player2Power + " N\n" + player2Speed + " m/hr\n" + "Time Stop";
        //			letterGrade.text = "C\nB\nB\nB\nA";
        //			break;
        //		default:
        //			break;
        //		}


        //update frame color of the car picked
        foreach (Button btn in charList)
        {
            if(btn.IsActive())
            {
                btn.GetComponentsInParent<Image>()[1].color = white;
            }
        }
        Debug.Log("char num = " + characterNumber + " becomes cyan");
		charList[characterNumber].GetComponentsInParent<Image>()[1].color = cyan;

		string charName = "";
		if(characterNumber == 0) { charName = "A n n a"; }
		else if(characterNumber == 1) { charName = "M a r y"; }
		else if(characterNumber == 2) { charName = "L i l y"; }
		else if(characterNumber == 3) { charName = "P a t"; }
		else if(characterNumber == 4) { charName = "J o h n"; }
		else if(characterNumber == 5) { charName = "A l e x"; }

		//Load.initialize ();
		JSONObject character = StaticVariables.GetCharacterAttribute (characterNumber);
		float currentlevel = character.GetField ("level").n;
		float currentExp = character.GetField ("currentExp").n;
		float maxExpForThisLevel = character.GetField ("maxExpForThisLevel").n;
		float hp = character.GetField ("hp").n;
		float mp = character.GetField ("mp").n;
		float speed = character.GetField ("speed").n;
		float CD = character.GetField ("CD").n;
		float attack = character.GetField ("attack").n;
		float defense = character.GetField ("defense").n;
		string skillName = character.GetField ("skillName").ToString();

        List<float> characterMaxAtt = StaticVariables.GetMaxCharacterAttribute(characterNumber);
        float maxLevel = StaticVariables.GetMaxCharacterLv(characterNumber);
        float maxHp = characterMaxAtt[0];
        float maxMp = characterMaxAtt[1];
        float maxSpeed = characterMaxAtt[2];
        float maxCD = characterMaxAtt[3];
        float maxAttack = characterMaxAtt[4];
        float maxDefense = characterMaxAtt[5];

        float widthMultiplier = 2;

        level.text = "Level " + currentlevel;
		var expBarTransform = expBar.GetComponent<Image>().transform as RectTransform;
		expBarTransform.sizeDelta = new Vector2 (currentExp/maxExpForThisLevel * 100, expBarTransform.sizeDelta.y);


		var HPMaxBarTransform = HPBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		HPMaxBarTransform.sizeDelta = new Vector2 (maxHp * widthMultiplier , HPMaxBarTransform.sizeDelta.y);
		var HPBarTransform = HPBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		HPBarTransform.sizeDelta = new Vector2 (hp * widthMultiplier, HPBarTransform.sizeDelta.y);
		HPBar.GetComponentInChildren<Text> ().text = hp + "/" + maxHp;


		var MPMaxBarTransform = MPBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		MPMaxBarTransform.sizeDelta = new Vector2 (maxMp*widthMultiplier, MPMaxBarTransform.sizeDelta.y);
		var MPBarTransform = MPBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		MPBarTransform.sizeDelta = new Vector2 (mp * widthMultiplier, MPBarTransform.sizeDelta.y);
		MPBar.GetComponentInChildren<Text> ().text = mp + "/" + maxMp;

		var speedMaxBarTransform = speedBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		speedMaxBarTransform.sizeDelta = new Vector2 (maxSpeed*widthMultiplier, speedMaxBarTransform.sizeDelta.y);
		var speedBarTransform = speedBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		speedBarTransform.sizeDelta = new Vector2 (speed * widthMultiplier, speedBarTransform.sizeDelta.y);
		speedBar.GetComponentInChildren<Text> ().text = speed + "/" + maxSpeed;

		var CDMaxBarTransform = CDBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		CDMaxBarTransform.sizeDelta = new Vector2 (maxCD*widthMultiplier, CDMaxBarTransform.sizeDelta.y);
		var CDBarTransform = CDBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		CDBarTransform.sizeDelta = new Vector2 (CD * widthMultiplier, CDBarTransform.sizeDelta.y);
		CDBar.GetComponentInChildren<Text> ().text = CD + "/" + maxCD;

		var attackMaxBarTransform = attackBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		attackMaxBarTransform.sizeDelta = new Vector2 (maxAttack*widthMultiplier, attackMaxBarTransform.sizeDelta.y);
		var attackBarTransform = attackBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		attackBarTransform.sizeDelta = new Vector2 (attack * widthMultiplier, attackBarTransform.sizeDelta.y);
		attackBar.GetComponentInChildren<Text> ().text = attack + "/" + maxAttack;

		var defenseMaxBarTransform = defenseBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		defenseMaxBarTransform.sizeDelta = new Vector2 (maxDefense*widthMultiplier, defenseMaxBarTransform.sizeDelta.y);
		var defenseBarTransform = defenseBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		defenseBarTransform.sizeDelta = new Vector2 (defense * widthMultiplier, defenseBarTransform.sizeDelta.y);
		defenseBar.GetComponentInChildren<Text> ().text = defense + "/" + maxDefense;

		skillName = skillName.Substring (1, skillName.Length - 2);

		title.text = charName;
		values.text = "\n\n\n\n\n\n" + skillName + "\n";

		switch(skillName) {
		case "dragonBreath":
			skillImage.sprite = skillList[0];
			break;
		case "goldAttack":
			skillImage.sprite = skillList [1];
			break;
		case "shield":
			skillImage.sprite = skillList [2];
			break;
		case "freeze":
			skillImage.sprite = skillList [3];
			break;
		case "nitro":
			skillImage.sprite = skillList [4];
			break;
		case "achilles":
			skillImage.sprite = skillList [5];
			break;
		default:
			break;
		}

	}
}
