using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardButton : MonoBehaviour {
	
	public Button button;
	public Text nameLabel;
	public Text priceLabel;
	public Text countLabel;
	public Image iconImage;
    public DetailPanelController detailPanelControl;
    public IconController iconControl;


    private JSONObject cardInfo;
	private string attribute;
	private int id;
	private ShopScrollList shopList;
    private GameObject detailPanel;
    private GameObject confirmPanel;

    // Use this for initialization
    void Start () {
        detailPanel = GameObject.Find("DetailPanel");
        detailPanelControl = detailPanel.GetComponent<DetailPanelController>();
        confirmPanel = GameObject.Find("Canvas").transform.Find("ConfirmPanel").gameObject;
        
        
        button.onClick.AddListener(HandleClick);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Setup(string attribute, int id, ShopScrollList currentShopList)
	{
		this.attribute = attribute;
		this.id = id;
        shopList = currentShopList;
        cardInfo = StaticVariables.cardData.GetField(attribute).list[id];
		nameLabel.text = cardInfo.GetField ("shortName").str;
		countLabel.text = "Count: " + StaticVariables.GetCardCount (attribute, id);
		priceLabel.text = "Price: " + StaticVariables.cardData.GetField("price").GetField(cardInfo.GetField("rank").str).n;
        iconControl = GameObject.Find("IconController").GetComponent<IconController>();
        iconImage.sprite = iconControl.setCardImage(attribute + cardInfo.GetField("rank").str);

    }

    public void HandleClick()
    {
        Debug.Log("Loading information of card " + attribute + " " + id);
        confirmPanel.SetActive(false);
        

        detailPanelControl.showPanel();
        detailPanelControl.setName(cardInfo["shortName"].str);
        detailPanelControl.setDesciption(cardInfo["description"].str);
        //detailPanelControl.setActionButtonText("Sell");
        detailPanelControl.setRankImage(attribute + cardInfo.GetField("rank").str);
        detailPanelControl.actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        detailPanelControl.actionButton.GetComponent<Button>().onClick.AddListener(ActionButtonOnClick);

    }

    public void ActionButtonOnClick()
    {
        Debug.Log("Action Button Clicked, selling " + attribute + " " + id);
        
        confirmPanel.SetActive(true);
        GameObject confirmButton = confirmPanel.transform.Find("ConfirmButton").gameObject;
        GameObject cancelButton = confirmPanel.transform.Find("CancelButton").gameObject;
        GameObject confirmText = confirmPanel.transform.Find("ConfirmText").gameObject;
        confirmButton.GetComponent<Button>().onClick.RemoveAllListeners();
        cancelButton.GetComponent<Button>().onClick.RemoveAllListeners();
        int currentCardCount = (int)StaticVariables.GetCardCount(attribute, id);
        if(currentCardCount > 0)
        {
            confirmText.GetComponent<Text>().text = "Confirm to Sell?";
            confirmButton.GetComponent<Button>().interactable = true;
            confirmButton.GetComponent<Button>().onClick.AddListener(ConfirmButtonOnClick);            
            cancelButton.GetComponent<Button>().onClick.AddListener(CancelButtonOnClick);
        }
        else
        {
            confirmButton.GetComponent<Button>().interactable = false;
            cancelButton.GetComponent<Button>().onClick.AddListener(CancelButtonOnClick);
            Debug.Log("Card Count is 0");
        }


    }

    public void ConfirmButtonOnClick()
    {
        //Debug.Log("Confirm Button Clicked");
        int totalCoins = StaticVariables.GetTotalCoins();
        //Debug.Log("ConfirmButtonOnClick TotalCoins Before Selling: " + totalCoins);
        int price = (int)StaticVariables.cardData.GetField("price").GetField(cardInfo.GetField("rank").str).n;
        StaticVariables.SetTotalCoins((float)totalCoins + price);
        totalCoins = StaticVariables.GetTotalCoins();
        //Debug.Log("ConfirmButtonOnClick TotalCoins After Selling: " + totalCoins);
        //Debug.Log("ConfirmButtonOnClick price: " + price);
        int currentCardCount = StaticVariables.GetCardCount(attribute, id);

        /*detailPanel.transform.Find("Name").gameObject.SetActive(false);
        detailPanel.transform.Find("Description").gameObject.SetActive(false);
        detailPanel.transform.Find("ActionButton").gameObject.SetActive(false);
        detailPanel.transform.Find("RankImage").gameObject.SetActive(false);*/

        //Debug.Log("ConfirmButtonOnClick Card Count Before Selling: " + currentCardCount);
        StaticVariables.SetCardCount(attribute, id, currentCardCount - 1);
        int currentCardMaxCount = StaticVariables.GetCardMaxCount(attribute, id);
        StaticVariables.SetCardMaxCount(attribute, id, currentCardMaxCount - 1);
        //currentCardCount = StaticVariables.GetCardCount(attribute, id);
        //Debug.Log("ConfirmButtonOnClick Card Count After Selling: " + currentCardCount);
        //Debug.Log("Card before refreshing display is:" + attribute + " " + id);
        shopList.RefreshDisplay(attribute);
        //Debug.Log("Card after refreshing display is:" + attribute + " " + id);
        confirmPanel.SetActive(false);

    }

    public void CancelButtonOnClick()
    {
        //Debug.Log("Cancel Button Clicked");
        confirmPanel.SetActive(false);
    }
}
