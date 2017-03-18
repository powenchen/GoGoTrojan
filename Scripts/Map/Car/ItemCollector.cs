using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour {

    public const int ITEM_LIST_LENGTH = 5;

	//public Button ItemBtn;
	//public Text ItemBtnText;

    public int itemIdx = -1;

    private float itemPutOffset = 5;

    public GameObject[] itemLists = new GameObject[ITEM_LIST_LENGTH];

    // we have total 6 kinds of items and effects
    //	1, missle		-> 射出擊中敵人扣血
    //	2, land mine	-> 敵人碰到扣血
    //	3, oil spill	-> 打滑
    //	4, glue			-> 速度變慢一段時間

    //	5, lightning	-> 閃電劈所有敵人 (TODO)
    //	6, shield		-> 不受技能道具影響 (TODO)

    // private int numOfItems = 2;

    // number of coins
   // public int coinCount;
    private float lightningDamage = 20;

	// if play doesn't collect an item, the default itemValue = 0;
	//private int itemValue = 0;

	// Use this for initialization
	void Start () {
		//coinCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// we have 4 kinds of collectable pickups
	// 1. coin
	// 2. heart
	// 3. poison
	// 4. box (randomly generate items) //(arrow?!)

	void OnTriggerEnter(Collider other) 
	{
        if (other.GetComponent<Pickup>() != null)
        {
            if (other.GetComponent<Coin>() != null)
            {
                //get coin
                PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + 1);
                StaticVariables.coinNumber += other.GetComponent<Coin>().getCoinValue();
                //PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
                //coinCount += other.GetComponent<Coin>().getCoinValue();
            }
            else if (other.GetComponent<Item>() != null)
            {
                //itemIdx = Random.Range(0, itemLists .Length);
                int total = 1000;
                int rng = Random.Range(0, total);
                if (rng < 25)
                {
                    itemIdx = 0;
                }
                else if (rng < 50)
                {
                    itemIdx = 1;
                }
                else if (rng < 75)
                {
                    itemIdx = 2;
                }
                else if (rng < 100)
                {
                    itemIdx = 3;
                }
                else {
                    itemIdx = 4;
                }
            }
            Destroy(other.gameObject);
        }


	}


    public void useItem()
    {
        if (itemIdx != -1)
        {
            if (itemIdx == 0)// missile is a special case
            {
                Vector3 spawnPosition = transform.position + 2 * transform.forward * itemPutOffset;
                Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
                Instantiate(itemLists[itemIdx], spawnPosition, spawnRotation, transform);
            }
            else if (itemIdx == 4) // lightning is a special case
            {
                foreach (Car car in FindObjectsOfType<Car>())
                {
                    if (!car.Equals(GetComponent<Car>()))
                    {
                        Vector3 spawnPosition = car.transform.position;
                        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        Instantiate(itemLists[itemIdx], spawnPosition, spawnRotation, car.transform);
                        car.decreaseHP(lightningDamage);
                    }
                }
            }
            else
            {
                Vector3 spawnPosition = transform.position - transform.forward * itemPutOffset;
                Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                Instantiate(itemLists[itemIdx], spawnPosition, spawnRotation);
            }
            itemIdx = -1;

        }
    }

    public int GetItemValue()
    {
        return itemIdx;
    }

	/*public int GetCoinNumber() {
	
		return coinCount;
	}*/
}
