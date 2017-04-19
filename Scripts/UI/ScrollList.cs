using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScrollList : MonoBehaviour {

    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;
    public Text TotalCoins;
    public DetailPanelController detailPanelControl;
    

    abstract public void RefreshDisplay(string attribute);
    

    public void RefreshTotalCoins()
    {
        int totalCoins = StaticVariables.GetTotalCoins();
        TotalCoins.text = "Total Coins: " + totalCoins;

    }

    public void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }

    }

    abstract public void AddButtons(string attribute = "none");

}
