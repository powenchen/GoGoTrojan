using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SLTest_debug : MonoBehaviour
{
    public bool debugSwitch = true;
    // Use this for initialization
    void Awake()
    {
        Debug.Log("persistentDataPath = " + Application.persistentDataPath);
        if (!File.Exists(Application.persistentDataPath + "/savedata.json"))
        {
            string dummy = "{\"characters\":[{\"MaxHP\":123,\"MaxMP\":456,\"SkillCD\":78,\"AttackPower\":9}],\"TotalCoins\":0}";
            File.WriteAllText(Application.persistentDataPath + "/savedata.json", dummy);
        }
        Load.loadState(Application.persistentDataPath + "/savedata.json");
    }

    // Update is called once per frame
    void Update()
    {
        if (debugSwitch && StaticVariables.saveData != null)
        {
            changeSaveData();
            debugSwitch = false;
        }
    }

    private void changeSaveData()
    {
        Debug.Log("old data = " + StaticVariables.saveData.ToString());
        foreach (JSONObject charItem in StaticVariables.saveData.GetField("characters").list)
        {
            //DO something
            charItem.SetField("SkillCD", charItem.GetField("SkillCD").n + 20);
        }
        //sample code
        JSONObject charData = StaticVariables.GetCharacterStatus(0);
        charData.SetField("MaxMP", charData.GetField("MaxMP").n + 10);
        charData.SetField("MaxHP", charData.GetField("MaxHP").n + 15);


        int coinT = StaticVariables.GetTotalCoins();
        StaticVariables.SetTotalCoins(coinT+100);

        Save.saveState(Application.persistentDataPath + "/savedata.json");
        Debug.Log("new data saved!!");
    }
}
