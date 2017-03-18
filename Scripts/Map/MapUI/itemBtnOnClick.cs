using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class itemBtnOnClick : MonoBehaviour {

	public Button ItemBtn;
	public Text ItemBtnText;
	public ItemCollector playerItemCollector;
	//public Text test;

    private int itemIdx = -1;

	// Use this for initialization
	void Start () {
        ItemBtn.interactable = true;
        if (ItemBtnText != null) {
			ItemBtnText.text = "";
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (playerItemCollector.GetItemValue() != itemIdx)
        {
            itemIdx = playerItemCollector.GetItemValue();
            
            switch (itemIdx)
            {
                case 0:
                    ItemBtnText.text = "MISSILE";
                    break;
                case 1:
                    ItemBtnText.text = "LANDMINE";
                    break;
                case 2:
                    ItemBtnText.text = "OIL";
                    break;
                case 3:
                    ItemBtnText.text = "GLUE";
                    break;
                case 4:
                    ItemBtnText.text = "LIGHTNING";
                    break;

            }

        }

    }

	public void itemButtonOnClick () {
        
        //ItemBtn.interactable = false;
        itemIdx = -1;

        ItemBtnText.text = "";
		int itemValue = playerItemCollector.GetItemValue ();
        //test.text = "itemValue = " + itemValue.ToString ();

        playerItemCollector.useItem();

    }

	

    public void setCollector(ItemCollector collector)
    {
        playerItemCollector = collector;
    }
}
