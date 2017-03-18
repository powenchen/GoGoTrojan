using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load : MonoBehaviour {
	/*void Start () {
        jsonStr = File.ReadAllText(Application.dataPath + "/Resources/savedata.json");
        //Debug.Log("json = " + jsonStr);
        StaticVariables.saveData = new JSONObject(jsonStr);
        
        Debug.Log("Char0 HP = " + StaticVariables.saveData.GetField("characters").list[0].GetField("MaxHP").ToString());
        Debug.Log("Char0 MP = " + StaticVariables.saveData.GetField("characters").list[0].GetField("MaxMP").ToString());
        Debug.Log("Char0 CD = " + StaticVariables.saveData.GetField("characters").list[0].GetField("SkillCD").ToString());
        Debug.Log("Char0 Att = " + StaticVariables.saveData.GetField("characters").list[0].GetField("AttackPower").ToString());
        Debug.Log("json = " + StaticVariables.saveData.ToString());
    }*/

    public static void loadState()
    {
        string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/savedata.json");
        StaticVariables.saveData = new JSONObject(jsonStr);
        Debug.Log("Char0 HP = " + StaticVariables.saveData.GetField("characters").list[0].GetField("MaxHP").ToString());
    }
}
