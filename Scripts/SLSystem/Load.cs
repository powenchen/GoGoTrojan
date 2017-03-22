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


    public static void initialize()
    {
        string cardStr = File.ReadAllText(Application.persistentDataPath + "/card_data.json");
        StaticVariables.cardData = new JSONObject(cardStr);
        string carStr = File.ReadAllText(Application.persistentDataPath + "/car_data.json");
        StaticVariables.carData = new JSONObject(carStr);
        string characterStr = File.ReadAllText(Application.persistentDataPath + "/character_data.json");
        StaticVariables.characterData = new JSONObject(characterStr);
        //Debug.Log("Char0 HP = " + StaticVariables.saveData.GetField("characters").list[0].GetField("MaxHP").ToString());
    }
}
