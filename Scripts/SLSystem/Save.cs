using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour {

    public static void saveState(string filePath)
    {
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
