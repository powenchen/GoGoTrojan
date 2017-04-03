using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load : MonoBehaviour {

    // hard code these init jsons
    private static string savedataInit = "";
    private static string carsInit = "";
    private static string cardInit = "";
    private static string characterInit = "";
    private static string itemInit = "";

    public static void loadState()
    {
        string filePath = Application.persistentDataPath + "/savedata.json";
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, savedataInit);
        }

        string jsonStr = File.ReadAllText(filePath);
        StaticVariables.saveData = new JSONObject(jsonStr);
        //Debug.Log("Char0 HP = " + StaticVariables.saveData.GetField("characters").list[0].GetField("MaxHP").ToString());
    }


    public static void initialize()
    {
        string cardDataPath = Application.persistentDataPath + "/card.json";
        string carDataPath = Application.persistentDataPath + "/cars.json";
        string characterDataPath = Application.persistentDataPath + "/character.json";
        string itemDataPath = Application.persistentDataPath + "/item.json";

        if (!File.Exists(cardDataPath))
        {
            File.WriteAllText(cardDataPath, cardInit);
        }
        string cardStr = File.ReadAllText(cardDataPath);
        StaticVariables.cardData = new JSONObject(cardStr);

        if (!File.Exists(carDataPath))
        {
            File.WriteAllText(carDataPath, carsInit);
        }
        string carStr = File.ReadAllText(carDataPath);
        StaticVariables.carData = new JSONObject(carStr);

        if (!File.Exists(characterDataPath))
        {
            File.WriteAllText(characterDataPath, characterInit);
        }
        string characterStr = File.ReadAllText(characterDataPath);
        StaticVariables.characterData = new JSONObject(characterStr);


        if (!File.Exists(itemDataPath))
        {
            File.WriteAllText(itemDataPath, itemInit);
        }
        string itemDataStr = File.ReadAllText(itemDataPath);
        StaticVariables.itemData = new JSONObject(itemDataStr);
        //Debug.Log("Char0 HP = " + StaticVariables.saveData.GetField("characters").list[0].GetField("MaxHP").ToString());
    }
}
