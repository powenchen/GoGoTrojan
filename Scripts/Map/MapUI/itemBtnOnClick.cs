using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class itemBtnOnClick : MonoBehaviour {

	public Button ItemBtn;
	public ItemCollector playerItemCollector;
    public Sprite[] itemIcons;
    public Image itemBtnImage;
	//public Text test;

    private int itemIdx = -1;

	// Use this for initialization
	void Start () {

        itemBtnImage.enabled = (false);
    }
	
	// Update is called once per frame
	void Update () {
        if (playerItemCollector.GetItemValue() == -1)
        {
            itemBtnImage.enabled = (false);
            return;
        }
        if (playerItemCollector.GetItemValue() != itemIdx)
        {

            itemIdx = playerItemCollector.GetItemValue();
            if (itemIdx >= 0 && itemIdx < itemIcons.Length)
            {
                itemBtnImage.sprite = itemIcons[itemIdx];
                itemBtnImage.enabled = (true);
            }
            /*
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

            }*/

        }

    }

	public void itemButtonOnClick () {
        
        if (itemIdx != -1)
        {
            itemIdx = -1;

            playerItemCollector.useItem();
        }

    }

	

    public void setCollector(ItemCollector collector)
    {
        playerItemCollector = collector;
    }
}
