using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailPanelController : MonoBehaviour {

    public GameObject detailName;
    public GameObject description;
    public GameObject actionButton;
    public GameObject rankImage;
    public IconController iconControl;

    public GameObject confirmPanel;
    public Text confirmPanelText;


    public Image detailImage;

    public string sellCardAttr;
    public int sellCardID;
    public string buyKey;
    public float price;

    public string myName;

    private void Update()
    {
        detailName.GetComponent<Text>().text = myName;
        if (sellCardAttr != "")
        {
            detailName.GetComponent<Text>().text += "\n[" + StaticVariables.GetCardCount(sellCardAttr, sellCardID) + "/" + StaticVariables.GetCardMaxCount(sellCardAttr, sellCardID) + "]";
        }
    }
    public void clearPanel()
    {
        detailName.SetActive(false);
        description.SetActive(false);
        actionButton.SetActive(false);
        rankImage.SetActive(false);
    }

    public void showPanel()
    {
        detailName.SetActive(true);
        description.SetActive(true);
        actionButton.SetActive(true);
        rankImage.SetActive(true);
    }

    public void setName(string name)
    {
        myName = name;
    }

    public void setDesciption(string descriptionText)
    {
        description.GetComponent<Text>().text = descriptionText;
    }

    public void setActionButtonText(string actionText , float price)
    {
        actionButton.GetComponentInChildren<Text>().text = actionText;
        confirmPanel.GetComponent<ConfirmPanelManager>().price = price;
        SetBuyButtonListener(actionText,price);
        price = price;
    }

    public void setRankImage(string type)
    {
        rankImage.GetComponent<Image>().sprite = iconControl.setCardImage(type);
    }

    public void setImageSprite(Sprite sprite)
    {
        detailImage.sprite = sprite;
    }

    public void SetBuyButtonListener(string action,float price)
    {
        actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        if (price > StaticVariables.GetTotalCoins())
        {
            //Debug.Log("FailedTransactionButtonListener");
            actionButton.GetComponent<Button>().onClick.AddListener(() => FailedTransactionButtonListener("Not Enough Coin"));
            return;
        }
        else if (price < 0 &&StaticVariables.GetCardCount(sellCardAttr, sellCardID) <= 0 )
        {
            //Debug.Log("FailedTransactionButtonListener");
            actionButton.GetComponent<Button>().onClick.AddListener(() => FailedTransactionButtonListener("No available cards"));
            return;
        }

        //Debug.Log("SellBuyButtonListener " + StaticVariables.GetCardCount(sellCardAttr, sellCardID) + " " +sellCardID + " "+price);
        if (price > 0)//buy something
        {
            string description = "Confirm to " + action.ToLower() + " for $" + Mathf.Abs(price) + "?";
            actionButton.GetComponent<Button>().onClick.AddListener(() => SellBuyButtonListener(description,price,buyKey));
        }
        else if (price < 0)//sell something
        {
            string description = "Confirm to " + action.ToLower() + " for $" + Mathf.Abs(price) + "?";
            actionButton.GetComponent<Button>().onClick.AddListener(() => SellBuyButtonListener(description, price, sellCardAttr, sellCardID));
        }
    }

    public void SellBuyButtonListener(string description, float price = 0, string myKey = "", int sellID = -1)
    {
        foreach (Button btn in FindObjectsOfType<Button>())
        {
            btn.interactable = false;
        }
        if (myKey != "")
        {
            if (sellID != -1)
            {
                confirmPanel.GetComponent<ConfirmPanelManager>().sellID = sellID;
                confirmPanel.GetComponent<ConfirmPanelManager>().sellAttr = myKey;
            }
            else
            {
                confirmPanel.GetComponent<ConfirmPanelManager>().buyKey = myKey;
            }
        }
        if (price != 0)
        {
            confirmPanel.GetComponent<ConfirmPanelManager>().price = price;
        }
        confirmPanelText.text = description;// = "Confirm to buy?"
        confirmPanel.SetActive(true);
        confirmPanel.GetComponent<ConfirmPanelManager>().confirmBtn.SetActive(true);
        confirmPanel.GetComponent<ConfirmPanelManager>().cancelBtn.SetActive(true);
        confirmPanel.GetComponent<ConfirmPanelManager>().okBtn.SetActive(false);
    }

    public void FailedTransactionButtonListener(string description)
    {
        foreach (Button btn in FindObjectsOfType<Button>())
        {
            btn.interactable = false;
        }
        confirmPanelText.text = description;
        confirmPanel.SetActive(true);
        confirmPanel.GetComponent<ConfirmPanelManager>().confirmBtn.SetActive(false);
        confirmPanel.GetComponent<ConfirmPanelManager>().cancelBtn.SetActive(false);
        confirmPanel.GetComponent<ConfirmPanelManager>().okBtn.SetActive(true);
    }
}
