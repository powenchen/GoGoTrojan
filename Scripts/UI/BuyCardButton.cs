using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyCardButton : MonoBehaviour {
    public Button button;
    public Text nameLabel;
    public Text priceLabel;
    public Image iconImage;
    public DetailPanelController detailPanelControl;
    public IconController iconControl;

    private JSONObject buyTypeInfo;
    private string buyType;
    private BuyCardScrollList buyCardList;
    private GameObject detailPanel;
    private GameObject confirmPanel;
    private GetCardPanel getCardPanel;

    Random random = new Random();



    // Use this for initialization
    void Start () {
        detailPanel = GameObject.Find("DetailPanel");
        detailPanelControl = detailPanel.GetComponent<DetailPanelController>();
        confirmPanel = GameObject.Find("Canvas").transform.Find("ConfirmPanel").gameObject;
        getCardPanel = GameObject.Find("Canvas").transform.Find("GetCardPanel").GetComponent<GetCardPanel>();
        button.onClick.AddListener(HandleClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(string buyType, BuyCardScrollList currentShopList)
    {
        this.buyType = buyType;
        buyTypeInfo = StaticVariables.cardData["cardProbability"][buyType];
        //Debug.Log(buyTypeInfo.ToString());
        nameLabel.text = buyTypeInfo.GetField("name").str;
        priceLabel.text = "Price: " + buyTypeInfo["cost"].n;
        iconControl = GameObject.Find("IconController").GetComponent<IconController>();
        iconImage.sprite = iconControl.setCardImage("buy" + buyType);

        this.buyCardList = currentShopList;
    }

    public void HandleClick()
    {
        Debug.Log("Buy Card Button Clicked: " + buyType);
        confirmPanel.SetActive(false);

        

        detailPanelControl.showPanel();
        detailPanelControl.setName(buyTypeInfo["name"].str);
        detailPanelControl.setDesciption(buyTypeInfo["description"].str);
        //detailPanelControl.setActionButtonText("Buy");
        detailPanelControl.setRankImage("buy" + buyType);
        detailPanelControl.actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        detailPanelControl.actionButton.GetComponent<Button>().onClick.AddListener(ActionButtonOnClick);


    }

    public void ActionButtonOnClick()
    {
        Debug.Log("Action Button Clicked, buying type : " + buyType);

        confirmPanel.SetActive(true);
        GameObject confirmButton = confirmPanel.transform.Find("ConfirmButton").gameObject;
        GameObject cancelButton = confirmPanel.transform.Find("CancelButton").gameObject;
        GameObject confirmText = confirmPanel.transform.Find("ConfirmText").gameObject;
        confirmButton.GetComponent<Button>().onClick.RemoveAllListeners();
        cancelButton.GetComponent<Button>().onClick.RemoveAllListeners();
        if (StaticVariables.GetTotalCoins() < buyTypeInfo["cost"].n)
        {
            Debug.Log("Insufficient Coins");
            confirmButton.GetComponent<Button>().interactable = false;
            confirmText.GetComponent<Text>().text = "Insufficient Coins";

        }
        else
        {
            Debug.Log("Have enough money");
            confirmButton.GetComponent<Button>().interactable = true;
            confirmText.GetComponent<Text>().text = "Confirm to Buy?";
        }

        confirmButton.GetComponent<Button>().onClick.AddListener(ConfirmButtonOnClick);
        cancelButton.GetComponent<Button>().onClick.AddListener(CancelButtonOnClick);
    }

 

    public void ConfirmButtonOnClick()
    {
        Debug.Log("Confirm buying");
        JSONObject newCard = draw(buyType);
        string newCardAttribute = newCard["attribute"].str;
        int newCardId = (int)newCard["id"].n;
        StaticVariables.SetTotalCoins(StaticVariables.GetTotalCoins() - buyTypeInfo["cost"].n);
        buyCardList.RefreshDisplay();
        confirmPanel.SetActive(false);
        getCardPanel.showGetCardPanel();
        getCardPanel.setCardName(StaticVariables.cardData[newCardAttribute][newCardId]["shortName"].str);
        getCardPanel.setCardDescription(StaticVariables.cardData[newCardAttribute][newCardId]["description"].str);
        getCardPanel.setCardImage(newCardAttribute + StaticVariables.cardData[newCardAttribute][newCardId]["rank"].str);



    }

    public void CancelButtonOnClick()
    {
        Debug.Log("Cancel Buying");
        confirmPanel.SetActive(false);
    }


    public string drawCardRank(string drawType)
    {
        
        float randomProb = Random.value;
        Debug.Log("Random Value in drawCardRank: " + randomProb);
        //JSONObject probOfType = StaticVariables.cardData["cardProbability"][drawType];
        JSONObject probList = StaticVariables.cardData["cardProbability"][drawType]["probability"];
        string cardType = "";
        for (int i = 0; i < probList.list.Count && randomProb >= 0; i++)
        {
            cardType = (string)probList.keys[i];
            randomProb -= probList.GetField(cardType).n;
        }
        Debug.Log("Getting type: " + cardType);
        return cardType;
    }

    public JSONObject getCard(string cardType)
    {
        JSONObject cardArray = StaticVariables.cardPackData.GetField(cardType);
        int cardNum = (int)(Random.value * cardArray.list.Count);
        Debug.Log("Getting card number: " + cardNum);
        return cardArray[cardNum];
    }

    public JSONObject draw(string drawType)
    {
        
        string cardType = drawCardRank(drawType);
        JSONObject newCard = getCard(cardType);
        Debug.Log("newCard: " + newCard.ToString());
        //JSONObject cardData = StaticVariables.saveData["cards"][newCard.GetField("attribute").str].list[(int)newCard.GetField("id").n];
        //cardData.SetField("number", cardData["number"].n + 1);
        int currentCount = StaticVariables.GetCardCount(newCard["attribute"].str, (int)newCard["id"].n);
        StaticVariables.SetCardCount(newCard["attribute"].str, (int)newCard["id"].n, currentCount + 1);
        int currentMaxCount = StaticVariables.GetCardMaxCount(newCard["attribute"].str, (int)newCard["id"].n);
        StaticVariables.SetCardMaxCount(newCard["attribute"].str, (int)newCard["id"].n, currentMaxCount + 1);
        //cardData.SetField("maxNumber", cardData["maxNumber"].n + 1);

        return newCard;

    }
}
