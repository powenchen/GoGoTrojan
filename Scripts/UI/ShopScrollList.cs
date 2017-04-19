using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScrollList : ScrollList {

	private JSONObject cards;

   

    public override void RefreshDisplay(string attribute)
    {
        //Debug.Log("Refreshing deisplay for" + attribute + " panel");
        cards = StaticVariables.GetCardList();
        RemoveButtons();
        AddButtons(attribute);
        RefreshTotalCoins();
        detailPanelControl.clearPanel();
    }

    public override void AddButtons(string attribute){
		int count = cards.GetField (attribute).Count;
		for (int i = 0; i < count; i++) 
		{
			//Debug.Log ("Going through card " + attribute + " " + i); 
			JSONObject card = cards.GetField (attribute).list [i];
			if (card.GetField ("number").n > 0) {

				GameObject newButton = buttonObjectPool.GetObject ();
				newButton.transform.SetParent (contentPanel);

				CardButton cardButton = newButton.GetComponent<CardButton>();
				cardButton.Setup (attribute, i, this);
			}
		}

	}
}
