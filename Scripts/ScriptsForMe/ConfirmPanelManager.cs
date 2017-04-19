using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPanelManager : MonoBehaviour {
    // Use this for initialization
    public float price;
    //sell price would be negative
    public DetailPanelController detailController;

    public GameObject confirmBtn;
    public GameObject cancelBtn;
    public GameObject okBtn;

    public int sellID;
    public string sellAttr;
    public string buyKey;

    public Text confirmText;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClickConfirm()
    {
        float coin = StaticVariables.GetTotalCoins();
        StaticVariables.SetTotalCoins(coin - price);
        if (price > 0) //buy something
        {
            //Debug.Log("buyKey = " + buyKey);
            //Debug.Log(StaticVariables.cardData["cardProbability"][buyKey]["probability"].ToDictionary().ToString());
            float rng = Random.value;
            Dictionary<string, string> myDict = StaticVariables.cardData["cardProbability"][buyKey]["probability"].ToDictionary();
            float probAcc = 0;
            string cardKey="";
            foreach (string key in myDict.Keys)
            {
                probAcc += float.Parse(myDict[key]);
                if (rng < probAcc)
                {
                    cardKey = key;
                    break;
                }

            }
            int idx = Random.Range(0, StaticVariables.cardPackData[cardKey].list.Count);
            string cardAttr = StaticVariables.cardPackData[cardKey].list[idx]["attribute"].str;
            int cardID = (int)(StaticVariables.cardPackData[cardKey].list[idx]["id"].n);
            confirmBtn.SetActive(false);
            cancelBtn.SetActive(false);
            okBtn.SetActive(true);

            int currCardNum = StaticVariables.GetCardCount(cardAttr, cardID);
            int maxCardNum = StaticVariables.GetCardMaxCount(cardAttr, cardID);
            StaticVariables.SetCardCount(cardAttr, cardID, currCardNum + 1);
            StaticVariables.SetCardMaxCount(cardAttr, cardID, maxCardNum + 1);

            confirmText.text = "YOU GOT a ["+ StaticVariables.cardData[cardAttr][cardID]["shortName"].str + "]";

            detailController.SetBuyButtonListener("Buy", price);
            return;     
        }
        else if (price < 0)//sell something
        {
            int currCardNum = StaticVariables.GetCardCount(sellAttr,sellID);
            int maxCardNum = StaticVariables.GetCardMaxCount(sellAttr, sellID);
            StaticVariables.SetCardCount(sellAttr, sellID, currCardNum - 1);
            StaticVariables.SetCardMaxCount(sellAttr, sellID, maxCardNum - 1);
        }

        foreach (Button btn in FindObjectsOfType<Button>())
        {
            btn.interactable = true;
        }

        detailController.SetBuyButtonListener((price > 0) ? "Buy" : "Sell", price);
        Save.saveState();
        gameObject.SetActive(false);
    }

    public void OnClickCancel()
    {
        foreach (Button btn in FindObjectsOfType<Button>())
        {
            btn.interactable = true;
        }
        gameObject.SetActive(false);
    }
}
