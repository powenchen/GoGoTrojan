using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUpgrade : MonoBehaviour {

    public GameObject okBtnGameObject;
    public GameObject yesBtnGameObject;
    public GameObject noBtnGameObject;
    public Text upgradeText;
    public Text upgradeTextShadow;
    public Text upgradeBtnText;
    public Text upgradeBtnTextShadow;
    public Text coinText;
	public Text priceText;
	public Button upgrade;
	public Button previousButton;
	public Button nextButton;
	public Button backButton;
	public GameObject confirmUpgrade;
	public GameObject locked;
	public List<Image> starList = new List<Image> ();
	public List<GameObject> carList = new List<GameObject>();

	private float lerpSpeed = 2;
	private float totalCoins;
	private float upgradePrice;
	private int carSelected = StaticVariables.carID;  
	private float currentLevel;
	private bool carChanged = false;          // update attributes if true
	private bool upgraded = false;

	private List<float> currentAttribute = new List<float>();
	private List<float> nextAttribute = new List<float>();
	private List<float> maxAttribute = new List<float> ();

	// HP
	public Image currentBarHP;
	public Image nextBarHP;
	private float currentHP;
	private float nextHP;
	private float maxHP;

	// MP
	public Image currentBarMP;
	public Image nextBarMP;
	private float currentMP;
	private float nextMP;
	private float maxMP;  

	// SPD
	public Image currentBarSPD;
	public Image nextBarSPD;
	private float currentSPD;
	private float nextSPD;
	private float maxSPD;    

	// CDR
	public Image currentBarCDR;
	public Image nextBarCDR;
	private float currentCDR;
	private float nextCDR;
	private float maxCDR;      

	// ATK
	public Image currentBarATK;
	public Image nextBarATK;
	private float currentATK;
	private float nextATK;
	private float maxATK;      

	// DEF
	public Image currentBarDEF;
	public Image nextBarDEF;
	private float currentDEF;
	private float nextDEF;
	private float maxDEF;           

	void Start () {
		Load.initialize ();
		Debug.Log (Application.persistentDataPath);
        carSelected = StaticVariables.carID;

        showTotalCoins ();
		showUpgradePrice ();
		enableBtns ();
		confirmUpgrade.SetActive (false);

        // Display Car Selected
        displayCar(carSelected);

    }

    void displayCar(int carSelected)
    {
        if (StaticVariables.GetLockStatus(carSelected))
        {
            StaticVariables.carID = 0;
        }
        else
        {
            StaticVariables.carID = carSelected;
        }
        foreach (GameObject car in carList)
        {
            car.SetActive(false);
        }
        carList[carSelected].SetActive(true);

        // Display Level
        SetStar(carSelected);

        currentLevel = StaticVariables.GetCurrentCarLevel(carSelected);
        currentAttribute = StaticVariables.GetCurrentCarAttribute(carSelected);
        nextAttribute = StaticVariables.GetNextLevelCarAttribute(carSelected);
        maxAttribute = StaticVariables.GetMaxCarAttribute(carSelected);
        maxHP = maxAttribute[0];
        maxMP = maxAttribute[1];
        maxSPD = maxAttribute[2];
        maxCDR = maxAttribute[3];
        maxATK = maxAttribute[4];
        maxDEF = maxAttribute[5];
        if (StaticVariables.GetLockStatus(carSelected))
        {
            upgrade.interactable = false;
            locked.SetActive(true);
        }
        else
        {
            //upgrade clickable if it is upgradable
            if (StaticVariables.GetNextLevelCarPrice(carSelected) == -1)
            {
                upgradeBtnText.text = "MAX LV";
                upgradeBtnTextShadow.text = "MAX LV";

                upgrade.interactable = false;
            }
            else
            {
                upgradeBtnText.text = "Upgrade";
                upgradeBtnTextShadow.text = "Upgrade";
                upgrade.interactable = true;
            }
            locked.SetActive(false);
        }
        
        upgradePrice = StaticVariables.GetNextLevelCarPrice(carSelected, (int)currentLevel);
        priceText.text = ((upgradePrice == -1) ? "--" : "$ " + upgradePrice);
    }

	void Update () {
        showTotalCoins();
		if (carChanged || upgraded) { 
			currentAttribute = StaticVariables.GetCurrentCarAttribute (carSelected);
			nextAttribute = StaticVariables.GetNextLevelCarAttribute (carSelected);
			carChanged = false;
			upgraded = false;
		}
		attSlider ();
	}
		
	public void prevBtn()
    {
        carSelected = Mathf.Clamp(carSelected - 1,0, carList.Count - 1);
        displayCar(carSelected);

    }
			

	public void nextBtn()
    {
        carSelected = Mathf.Clamp(carSelected + 1,0, carList.Count - 1);
        displayCar(carSelected);
    }

	// Upgrade Button, Yes, No
	public void upgradeBtn() {
        foreach (Button btn in FindObjectsOfType<Button>())
        {
            btn.interactable = false;
        }

        upgradePrice = StaticVariables.GetNextLevelCarPrice(carSelected, (int)currentLevel);
        upgradeText.text = "Upgrade to the next level takes " + upgradePrice + " coins";
        upgradeTextShadow.text = "Upgrade to the next level takes " + upgradePrice + " coins";
        confirmUpgrade.SetActive (true);
        //disableBtns ();
    }

	public void noBtn() {
		confirmUpgrade.SetActive (false);

        foreach (Button btn in FindObjectsOfType<Button>())
        {
            btn.interactable = true;
        }

        displayCar(carSelected);
        //enableBtns ();
    }

    void displayNoEnoughMeneyMessage()
    {
        okBtnGameObject.SetActive(true);
        yesBtnGameObject.SetActive(false);
        noBtnGameObject.SetActive(false);
        upgradeText.text = "You don't have enough coins";
        upgradeTextShadow.text = "You don't have enough coins";
    }

    public void OnClickOK()
    {
        okBtnGameObject.SetActive(false);
        yesBtnGameObject.SetActive(true);
        noBtnGameObject.SetActive(true);
        upgradePrice = StaticVariables.GetNextLevelCarPrice(carSelected, (int)currentLevel);
        upgradeText.text = "Upgrade to the next level takes " + upgradePrice + " coins";
        upgradeTextShadow.text = "Upgrade to the next level takes " + upgradePrice + " coins";

        displayCar(carSelected);
    }

	public void yesBtn() {
        totalCoins = StaticVariables.GetTotalCoins();
        upgradePrice = StaticVariables.GetNextLevelCarPrice(carSelected);

        if (totalCoins < upgradePrice)
        {
            displayNoEnoughMeneyMessage();
            return;
        }

        StaticVariables.SetCurrentCarLevel(carSelected, (int)currentLevel + 1);
        StaticVariables.SetTotalCoins(totalCoins - upgradePrice);
        //Debug.Log("total coins is now set to " + StaticVariables.saveData["totalCoins"].n);
        SetStar (carSelected);
		confirmUpgrade.SetActive (false);
		upgraded = true;

        Save.saveState();

        foreach (Button btn in FindObjectsOfType<Button>())
        {
            btn.interactable = true;
        }

        displayCar(carSelected);
    }

	private void showTotalCoins() {
		// Display Total Coins
		totalCoins = StaticVariables.GetTotalCoins ();
		string[] temp = coinText.text.Split (':');
		coinText.text = temp [0] + ": $ " + totalCoins;
	}

	private void showUpgradePrice() {
		// Display Upgrade Price
		upgradePrice = StaticVariables.GetNextLevelCarPrice(carSelected, (int)currentLevel);
		priceText.text = ((upgradePrice == -1) ? "--" : "$ " + upgradePrice);
    }

	private void SetStar(int carIndex) {
		// Initialize
		foreach (Image star in starList) {
			star.color = new Color (0, 0, 0, 0.5f);
		}

		// Display # of levels
		currentLevel = StaticVariables.GetCurrentCarLevel (carIndex);
		for (var i = 0; i < currentLevel; i++) {
			starList [i].color = new Color (255, 255, 255);
		}
	}

	private void disableBtns() {
		upgrade.interactable = false;
		previousButton.interactable = false;
		nextButton.interactable = false;
		backButton.interactable = false;
	}

	private void enableBtns() {
		if (StaticVariables.GetTotalCoins () - StaticVariables.GetNextLevelCarPrice (carSelected, (int)currentLevel) >= 0) {
			upgrade.interactable = true;
		}
		previousButton.interactable = true;
		nextButton.interactable = true;
		backButton.interactable = true;
	}
		
	// Set values for attribute bars
	private void attSlider(){

		currentHP = ValueTranslate (currentAttribute [0], maxHP);
		
		if (currentHP != currentBarHP.fillAmount) {
			currentBarHP.fillAmount = Mathf.Lerp (currentBarHP.fillAmount, currentHP, Time.deltaTime * lerpSpeed);
		}
        

        currentMP = ValueTranslate (currentAttribute [1], maxMP);
		
		if (currentMP != currentBarMP.fillAmount) {
			currentBarMP.fillAmount = Mathf.Lerp (currentBarMP.fillAmount, currentMP, Time.deltaTime * lerpSpeed);
		}
        
		currentSPD = ValueTranslate (currentAttribute [2], maxSPD);
		
		if (currentSPD != currentBarSPD.fillAmount) {
			currentBarSPD.fillAmount = Mathf.Lerp (currentBarSPD.fillAmount, currentSPD, Time.deltaTime * lerpSpeed);
		}
        

		currentCDR = ValueTranslate (currentAttribute [3], maxCDR);
		
		if (currentCDR != currentBarCDR.fillAmount) {
			currentBarCDR.fillAmount = Mathf.Lerp (currentBarCDR.fillAmount, currentCDR, Time.deltaTime * lerpSpeed);
		}
        

		currentATK = ValueTranslate (currentAttribute [4], maxATK);
		
		if (currentATK != currentBarATK.fillAmount) {
			currentBarATK.fillAmount = Mathf.Lerp (currentBarATK.fillAmount, currentATK, Time.deltaTime * lerpSpeed);
		}
        
        currentDEF = ValueTranslate (currentAttribute [5], maxDEF);
		
		if (currentDEF != currentBarDEF.fillAmount) {
			currentBarDEF.fillAmount = Mathf.Lerp (currentBarDEF.fillAmount, currentDEF, Time.deltaTime * lerpSpeed);
		}

        if (nextAttribute != null)
        {
            nextHP = ValueTranslate(nextAttribute[0], maxHP);
            if (nextHP != nextBarHP.fillAmount)
            {
                nextBarHP.fillAmount = Mathf.Lerp(nextBarHP.fillAmount, nextHP, Time.deltaTime * lerpSpeed);
            }
            nextMP = ValueTranslate(nextAttribute[1], maxMP);
            if (nextMP != nextBarMP.fillAmount)
            {
                nextBarMP.fillAmount = Mathf.Lerp(nextBarMP.fillAmount, nextMP, Time.deltaTime * lerpSpeed);
            }
            nextSPD = ValueTranslate(nextAttribute[2], maxSPD);
            if (nextSPD != nextBarSPD.fillAmount)
            {
                nextBarSPD.fillAmount = Mathf.Lerp(nextBarSPD.fillAmount, nextSPD, Time.deltaTime * lerpSpeed);
            }
            nextCDR = ValueTranslate(nextAttribute[3], maxCDR);
            if (nextCDR != nextBarCDR.fillAmount)
            {
                nextBarCDR.fillAmount = Mathf.Lerp(nextBarCDR.fillAmount, nextCDR, Time.deltaTime * lerpSpeed);
            }
            nextATK = ValueTranslate(nextAttribute[4], maxATK);
            if (nextATK != nextBarATK.fillAmount)
            {
                nextBarATK.fillAmount = Mathf.Lerp(nextBarATK.fillAmount, nextATK, Time.deltaTime * lerpSpeed);
            }
            nextDEF = ValueTranslate(nextAttribute[5], maxDEF);
            if (nextDEF != nextBarDEF.fillAmount)
            {
                nextBarDEF.fillAmount = Mathf.Lerp(nextBarDEF.fillAmount, nextDEF, Time.deltaTime * lerpSpeed);
            }
        }


    }

	// Mapping current value to scale 0-1;
	private float ValueTranslate(float currValue, float maxValue) {
		return currValue/maxValue;
	}



}
