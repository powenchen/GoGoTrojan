using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarPickUp : MonoBehaviour {

    public GameObject[] characters;
    public Transform[] characterSpawnPoint;
    public Button confirmText;
	public Button backText;
	public Button storeText;
	public Text title;
	public GameObject slot;
	public Text values;
	public Text level;
	public Button[] carList;
	public Sprite[] spriteList;
	public Sprite[] cardList;
	public GameObject expBar;
	public GameObject HPBar;
	public GameObject MPBar;
	public GameObject speedBar;
	public GameObject CDBar;
	public GameObject attackBar;
	public GameObject defenseBar;
	public GameObject equipCardPan;
	public GameObject confirmCardPan;
	public GameObject cardButtonList;
	private int cardPicked;
	private int slotPicked;
	private string cardTypePicked;	// Can be decided by carID + slotID
	private Color32 atk;
	private Color32 def;
	private Color32 spe;
	private Color32 cyan;
	private Color32 white;

	public static int carPicked;

	public CameraMover MyCameraMover; 

	// Use this for initialization
	void Start () {
//		Debug.Log (Application.persistentDataPath);
        foreach(Transform spawnPoint in characterSpawnPoint)
        {
            GameObject obj = Instantiate(characters[StaticVariables.characterID], new Vector3(), new Quaternion(), spawnPoint);
            obj.transform.localPosition = new Vector3();
            obj.transform.localRotation = new Quaternion();
        }

		Load.initialize ();
		for(int i = 0; i < carList.Length; i++) {
			if(StaticVariables.GetLockStatus(i)) {
				DisableButton (carList [i]);
			}
		}
		carPicked = StaticVariables.carID;
		cardPicked = 0;
		slotPicked = 0;
		cardTypePicked = "ATK";
		atk = new Color32(255,160,0,255);
		def = new Color32(0,0,255,255);
		spe = new Color32(0,176,23,255);
		cyan = new Color32 (0, 244, 255, 255);
		white = new Color32 (255, 255, 255, 255);
		ChangeCarDetail (carPicked);

	}

	private void PrintCardInfo(string attribute, int cardID) {

		JSONObject card = StaticVariables.GetCardInfo (attribute, cardID);
		Sprite pic = cardList [0];
		if(attribute == "ATK") { 
			if(card.GetField("rank").str == "C") { pic = cardList[0]; }
			else if(card.GetField("rank").str == "B") { pic = cardList[1]; }
			else if(card.GetField("rank").str == "A") { pic = cardList[2]; }
			else if(card.GetField("rank").str == "S") { pic = cardList[3]; }
		} else if(attribute == "DEF") { 
			if(card.GetField("rank").str == "C") { pic = cardList[4]; }
			else if(card.GetField("rank").str == "B") { pic = cardList[5]; }
			else if(card.GetField("rank").str == "A") { pic = cardList[6]; }
			else if(card.GetField("rank").str == "S") { pic = cardList[7]; }
		} else if(attribute == "SPE") { 
			if(card.GetField("rank").str == "C") { pic = cardList[8]; }
			else if(card.GetField("rank").str == "B") { pic = cardList[9]; }
			else if(card.GetField("rank").str == "A") { pic = cardList[10]; }
			else if(card.GetField("rank").str == "S") { pic = cardList[11]; }
		}

		/*if (card.GetField ("number").n <= 0) {
			string emptySlot = "Card1 (" + cardID + ")";
			GameObject delete = GameObject.Find (emptySlot);
			if(delete) { delete.SetActive (false); }
			return;
		} else {
			string occupiedSlot = "Card1 (" + cardID + ")";
			GameObject show = GameObject.Find (occupiedSlot);
			if (!show) { show.SetActive (true); }
		}*/

        string occupiedSlot = "Card1 (" + cardID + ")";
        GameObject show = GameObject.Find(occupiedSlot);
        if (!show) { show.SetActive(true); }

        cardButtonList.GetComponentsInChildren<Button> () [cardID].GetComponentsInChildren<Image> ()[1].sprite = pic;
		cardButtonList.GetComponentsInChildren<Button> ()[cardID].GetComponentsInChildren<Text>()[0].text = card.GetField("shortName").str;
		string description = "Available: " + card.GetField ("number").ToString() + "\t\t";
		description += "Max Number: " + card.GetField("maxNumber").ToString() + "\t\t";
		description += "Sell price: " + card.GetField("sellPrice").ToString();

		cardButtonList.GetComponentsInChildren<Button> () [cardID].GetComponentsInChildren<Text> () [1].text = description;
	}

	public void SetCardPicked(int number) {
		cardPicked = number;

	}

	// Not in use right now
	public void DismissConfirmCardPanel() {
		confirmCardPan.SetActive (false);
	}

	// Not in use right now
	public void ShowConfirmCardPanel() {
		confirmCardPan.SetActive (true);
	}

	public void ShowEquipCardPanel(int slotNumber) {
		cardTypePicked = StaticVariables.GetCarSlotAttribute (carPicked, slotNumber);
		equipCardPan.SetActive (true);
		slotPicked = slotNumber;
		int i = 0;
		int j = 0;
		switch(cardTypePicked) {
		case "ATK":
			j = 34;
			break;
		case "DEF":
			j = 26;
			break;
		case "SPE":
			j = 32;
			break;
		default:
			break;
		}
		for(; i < j; i++) {	//atk 34, def 26, spe 32
			PrintCardInfo (cardTypePicked, i);
		}
		for(; i < 34; i++) {
			string emptySlot = "Card1 (" + i + ")";
			GameObject delete = GameObject.Find (emptySlot);
			if(delete) { delete.SetActive (false); }
		}

	
	}

	public void EquipCard() {
		StaticVariables.SetCurrentCardInSlot (carPicked, slotPicked, cardPicked);	// QUESTION no need card type?? will the parameter change if I reload the carWithCardAttribute?
		ChangeCarDetail (carPicked);
		Debug.Log ("the slots: "+ StaticVariables.GetCurrentCardInSlot(carPicked)[0]);
		DismissEquipCardPanel ();
        Save.saveState();
	}

	public void RemoveCard() {
		StaticVariables.RemoveCurrentCardInSlot (carPicked, slotPicked);	// QUESTION Same quesiton as the previous one
		ChangeCarDetail(carPicked);
		DismissEquipCardPanel ();
        Save.saveState();
    }

	public void DismissEquipCardPanel() {
		equipCardPan.SetActive (false);
	}

	public void DisableButton(Button button) {
		button.GetComponent<Image> ().color = new Color32(60, 60, 60, 255);
        button.enabled = false;
        button.GetComponentsInChildren<Image> ()[1].color = new Color32 (255, 255, 255, 255);	// show the lock image;
		

	}

	public void EnableButton(Button button) {
		button.enabled = true;
	}

	// Update is called once per frame
	void Update () {
	}

	public void Confirm() {
		//PlayerPrefs.SetInt ("PlayerID", characterPicked);
		StaticVariables.carID = carPicked;

		SceneManager.LoadScene ("Race");
	}

	public void Goback() {
		SceneManager.LoadScene ("CharacterPickUp");
	}

	public void Store() {
		SceneManager.LoadScene ("Upgrade");
	}

	public void Setting() {
		SceneManager.LoadScene ("Settings");
	}

    public void PickCar(int carNum)
    {
        carPicked = carNum;
        StaticVariables.carID = carNum;
        this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(carNum, 1f));
    }
    /*
	public void PickCarLearner() {
		// change the frame color of last picked car back to white
		carList[carPicked].GetComponentsInParent<Image>()[1].color = white;
		carPicked = 0;
        StaticVariables.carID = carPicked;

        ChangeCarDetail (carPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(carPicked, 1f));
	}
	public void PickTexiEarner() {
		// change the frame color of last picked car back to white
		carList[carPicked].GetComponentsInParent<Image>()[1].color = white;
		carPicked = 1;
        StaticVariables.carID = carPicked;
        ChangeCarDetail (carPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(carPicked, 1f));

	}
	public void PickSportcarProducer() {
		// change the frame color of last picked car back to white
		carList[carPicked].GetComponentsInParent<Image>()[1].color = white;
		carPicked = 2;
        StaticVariables.carID = carPicked;
        ChangeCarDetail (carPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(carPicked, 1f));
	}
	public void PickAmbulenceHealer() {
		// change the frame color of last picked car back to white
		carList[carPicked].GetComponentsInParent<Image>()[1].color = white;
		carPicked = 3;
        StaticVariables.carID = carPicked;
        ChangeCarDetail (carPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(carPicked, 1f));
	}
	public void PickPolicecarRaider() {
		// change the frame color of last picked car back to white
		carList[carPicked].GetComponentsInParent<Image>()[1].color = white;
		carPicked = 4;
        StaticVariables.carID = carPicked;
        ChangeCarDetail (carPicked);
		this.MyCameraMover.StartCoroutine(this.MyCameraMover.MoveToPosition(carPicked, 1f));
	}*/
		
	public void ChangeCarDetail(int carNumber) {
        PickCar(carNumber);
        //update frame color of the car picked
        foreach (Button btn in carList)
        {
            btn.GetComponentsInParent<Image>()[1].color = white;
        }
        carList[carNumber].GetComponentsInParent<Image>()[1].color = cyan;

		string carName = "";
		string skillName = "";
		if(carNumber == 0) { carName = "L e a r n e r"; skillName = "Exp * 2";}
		else if(carNumber == 1) { carName = "E a r n e r"; skillName = "Coin * 2"; }
		else if(carNumber == 2) { carName = "P r o d u c e r"; skillName = "CDR * 1.5"; }
		else if(carNumber == 3) { carName = "H e a l e r"; skillName = "HP++/sec."; }
		else if(carNumber == 4) { carName = "R a i d e r"; skillName = "ATK * 1.2"; }

		List<float> currCarAttr = StaticVariables.GetCurrentCarAttributeWithCard (carNumber);  // return [hp, mp, speed,CD, attack, defense]	// QUESTION not working when with card
		float currentlevel = StaticVariables.GetCurrentCarLevel(carNumber);
		float hp = currCarAttr[0];
		float mp = currCarAttr [1];
		float speed = currCarAttr [2];
		float CD = currCarAttr [3];
		float attack = currCarAttr [4];
		float defense = currCarAttr [5];
        List<float> maxCarAttr = StaticVariables.GetMaxCarAttribute(carNumber);
        float maxHp = maxCarAttr[0];
        float maxMp = maxCarAttr[1];
        float maxSpeed = maxCarAttr[2];
        float maxCD = maxCarAttr[3];
        float maxAttack = maxCarAttr[4];
        float maxDefense = maxCarAttr[5];

        float widthMultiplier = 3.5f;

        level.text = "Level " + currentlevel;
		var expBarTransform = expBar.GetComponent<Image>().transform as RectTransform;
        float maxLvl = StaticVariables.carData["maxLevel"].n;
        Debug.Log("maxlvl = " + maxLvl);
		expBarTransform.sizeDelta = new Vector2 (currentlevel/ maxLvl * 130, expBarTransform.sizeDelta.y);


		var HPMaxBarTransform = HPBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		HPMaxBarTransform.sizeDelta = new Vector2 (maxHp* widthMultiplier, HPMaxBarTransform.sizeDelta.y);
		var HPBarTransform = HPBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		HPBarTransform.sizeDelta = new Vector2 (hp * widthMultiplier, HPBarTransform.sizeDelta.y);
		HPBar.GetComponentInChildren<Text> ().text = hp + "/" + maxHp;


		var MPMaxBarTransform = MPBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		MPMaxBarTransform.sizeDelta = new Vector2 (maxMp * widthMultiplier, MPMaxBarTransform.sizeDelta.y);
		var MPBarTransform = MPBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		MPBarTransform.sizeDelta = new Vector2 (mp * widthMultiplier, MPBarTransform.sizeDelta.y);
		MPBar.GetComponentInChildren<Text> ().text = mp + "/" + maxMp;

		var speedMaxBarTransform = speedBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		speedMaxBarTransform.sizeDelta = new Vector2 (maxSpeed * widthMultiplier, speedMaxBarTransform.sizeDelta.y);
		var speedBarTransform = speedBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		speedBarTransform.sizeDelta = new Vector2 (speed * widthMultiplier, speedBarTransform.sizeDelta.y);
		speedBar.GetComponentInChildren<Text> ().text = speed + "/" + maxSpeed;

		var CDMaxBarTransform = CDBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		CDMaxBarTransform.sizeDelta = new Vector2 (maxCD * widthMultiplier, CDMaxBarTransform.sizeDelta.y);
		var CDBarTransform = CDBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		CDBarTransform.sizeDelta = new Vector2 (CD * widthMultiplier, CDBarTransform.sizeDelta.y);
		CDBar.GetComponentInChildren<Text> ().text = CD + "/" + maxCD;

		var attackMaxBarTransform = attackBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		attackMaxBarTransform.sizeDelta = new Vector2 (maxAttack * widthMultiplier, attackMaxBarTransform.sizeDelta.y);
		var attackBarTransform = attackBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		attackBarTransform.sizeDelta = new Vector2 (attack * widthMultiplier, attackBarTransform.sizeDelta.y);
		attackBar.GetComponentInChildren<Text> ().text = attack + "/" + maxAttack;

		var defenseMaxBarTransform = defenseBar.GetComponentsInChildren<Image>()[0].transform as RectTransform;
		defenseMaxBarTransform.sizeDelta = new Vector2 (maxDefense * widthMultiplier, defenseMaxBarTransform.sizeDelta.y);
		var defenseBarTransform = defenseBar.GetComponentsInChildren<Image>()[1].transform as RectTransform;
		defenseBarTransform.sizeDelta = new Vector2 (defense * widthMultiplier, defenseBarTransform.sizeDelta.y);
		defenseBar.GetComponentInChildren<Text> ().text = defense + "/" + maxDefense;

		// Get slot information
		List<float> cardsInSlot = StaticVariables.GetCurrentCardInSlot(carPicked);		// QUESTION Return a card ID??
//		slot.GetComponentsInChildren<Text> () [0].text = (cardsInSlot [0] == -1)? "Empty" : cardsInSlot [0].ToString();
//		slot.GetComponentsInChildren<Text> () [1].text = (cardsInSlot [0] == -1)? "Empty" : cardsInSlot [0].ToString();
//		slot.GetComponentsInChildren<Text> () [2].text = (cardsInSlot [1] == -1)? "Empty" : cardsInSlot [1].ToString();
//		slot.GetComponentsInChildren<Text> () [3].text = (cardsInSlot [1] == -1)? "Empty" : cardsInSlot [1].ToString();
//		slot.GetComponentsInChildren<Text> () [4].text = (cardsInSlot [2] == -1)? "Empty" : cardsInSlot [2].ToString();
//		slot.GetComponentsInChildren<Text> () [5].text = (cardsInSlot [2] == -1)? "Empty" : cardsInSlot [2].ToString();
		for(int i = 0; i < 6; i++) {
//			slot.GetComponentsInChildren<Text> () [i].text = (cardsInSlot [i/2] == -1)? "Empty" : cardsInSlot [i/2].ToString();
			slot.GetComponentsInChildren<Text> () [i].text = (cardsInSlot [i/2] == -1)? "Empty" : "";
			// get card info
			var slotType = StaticVariables.GetCarSlotAttribute (carNumber, (int)i/2);
			if(cardsInSlot[i/2] == -1) {
				slot.GetComponentsInChildren<Image> () [i - i % 2].GetComponentsInChildren<Image> () [1].sprite = null;
				slot.GetComponentsInChildren<Image> () [i - i % 2].GetComponentsInChildren<Image> () [1].color = new Color32 (120,120,120,255);
				continue;
			}
			JSONObject card = StaticVariables.GetCardInfo (slotType, (int)cardsInSlot[i/2]);
			Sprite pic = cardList [0];
			if(slotType == "ATK") { 
				if(card.GetField("rank").str == "C") { pic = cardList[0]; }
				else if(card.GetField("rank").str == "B") { pic = cardList[1]; }
				else if(card.GetField("rank").str == "A") { pic = cardList[2]; }
				else if(card.GetField("rank").str == "S") { pic = cardList[3]; }
			} else if(slotType == "DEF") { 
				if(card.GetField("rank").str == "C") { pic = cardList[4]; }
				else if(card.GetField("rank").str == "B") { pic = cardList[5]; }
				else if(card.GetField("rank").str == "A") { pic = cardList[6]; }
				else if(card.GetField("rank").str == "S") { pic = cardList[7]; }
			} else if(slotType == "SPE") { 
				if(card.GetField("rank").str == "C") { pic = cardList[8]; }
				else if(card.GetField("rank").str == "B") { pic = cardList[9]; }
				else if(card.GetField("rank").str == "A") { pic = cardList[10]; }
				else if(card.GetField("rank").str == "S") { pic = cardList[11]; }
			}
			slot.GetComponentsInChildren<Image> () [i - i % 2].GetComponentsInChildren<Image> () [1].sprite = pic;
			slot.GetComponentsInChildren<Image> () [i - i % 2].GetComponentsInChildren<Image> () [1].color = new Color32 (255,255,255,255);

		}

		for(int i = 0; i < 3; i++) {
			var slotType = StaticVariables.GetCarSlotAttribute (carNumber, i);
			Color32 color = atk;
			if(slotType == "ATK") { color = atk; }
			else if(slotType == "DEF") { color = def; }
			else if(slotType == "SPE") { color = spe; }
			slot.GetComponentsInChildren<Image>()[2 * i].color = color;
		}



//
//		skillName = skillName.Substring (1, skillName.Length - 2);

		title.text = carName;
		values.text = "\n\n\n\n\n\n" + skillName + "\n";

	}
}
