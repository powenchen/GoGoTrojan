using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load : MonoBehaviour {
    public static void loadState(string filePath)
    {
        string jsonStr = File.ReadAllText(filePath);
        StaticVariables.saveData = new JSONObject(jsonStr);
        //Debug.Log("Char0 HP = " + StaticVariables.saveData.GetField("characters").list[0].GetField("MaxHP").ToString());
    }
}
