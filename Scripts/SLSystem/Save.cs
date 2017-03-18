using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour {

    public static void saveState()
    {
        string saveStr = "";
        if (StaticVariables.saveData != null)
            saveStr = StaticVariables.saveData.ToString();
        File.WriteAllText(Application.dataPath + "/Resources/savedata.json", saveStr);
    }
}
