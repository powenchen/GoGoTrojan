using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour {

    // call this function after
    // 1. comlpeting an upgrade
    // 2. equipping/removing a card
    // 3. finishing a race
    public static void saveState()
    {
        string filePath = Application.persistentDataPath + "/savedata.json";
        if (StaticVariables.saveData != null)
        {
            string saveStr = StaticVariables.saveData.ToString();
            File.WriteAllText(filePath, saveStr);
        }
        else
        {
            Debug.LogError("Cannot save data since it's null");
        }
    }
}
