using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class CardStore : MonoBehaviour {

	public Text totalCoins;
    public Transform firstContentPanel;

    // Use this for initialization
    private void Start()
    {

        Load.initialize();
    }
    void Update () {
		
        //firstContentPanel.GetComponent<ScrollList>().RefreshDisplay("");
		//Debug.Log (Application.persistentDataPath + "/savedata.json");
		totalCoins.text = "Total Coins: " + StaticVariables.saveData.GetField ("totalCoins").ToString();
        //AssetDatabase.Refresh();
    }

}
