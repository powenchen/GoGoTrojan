using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCardScrollList : ScrollList {

    private JSONObject cardPacks;
    
    public override void RefreshDisplay(string attribute = "none")
    {
        //Debug.Log("Refreshing deisplay buy panel");
        RemoveButtons();
        AddButtons();
        RefreshTotalCoins();
        detailPanelControl.clearPanel();
    }

    public override void AddButtons(string attribute = "none")
    {
        int count = StaticVariables.cardData["cardProbability"].Count;
        for (int i = 0; i < count; i++)
        {
            //Debug.Log ("Going through card " + attribute + " " + i); 
            string cardPackName = StaticVariables.cardData["cardProbability"].keys[i];
            //Debug.Log(cardPackName);
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            BuyCardButton buyCardButton = newButton.GetComponent<BuyCardButton>();
            buyCardButton.Setup(cardPackName, this);
            
        }
    }
}
