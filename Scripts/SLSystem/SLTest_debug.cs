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
        foreach (JSONObject jsonObj in StaticVariables.saveData.GetField("characters").list)
        {
            jsonObj.SetField("MaxHP", jsonObj.GetField("MaxHP").n + 1);
            jsonObj.SetField("MaxMP", jsonObj.GetField("MaxMP").n + 2);
            jsonObj.SetField("SkillCD", jsonObj.GetField("SkillCD").n + 3);
            jsonObj.SetField("AttackPower", jsonObj.GetField("AttackPower").n + 4);
        }
        int coinT = (int)(StaticVariables.saveData.GetField("TotalCoins").n);
        StaticVariables.saveData.SetField("TotalCoins", coinT + 100);

        Save.saveState(Application.persistentDataPath + "/savedata.json");
        Debug.Log("new data saved!!");
    }
}
