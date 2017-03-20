using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariables : MonoBehaviour {
    public static int ranking = -1;
    public static bool gameIsOver = false;
    public static bool gameStarts = false;
    public static bool musicStartFlag = false;
    public static string raceTimeStr = "";
    public static int coinNumber = 0;
    public static int characterID = 0;
    public static int mapID = 0;
    public static int carID = 0;
    public static JSONObject saveData = null;

    public static void ResetVariables()
    {
        //reset everything for one 'run'
        ranking = -1;
        gameIsOver = false;
        gameStarts = false;
        musicStartFlag = false;
        raceTimeStr = "";
        coinNumber = 0;
        // dont reset savedata
    }
    
    public static JSONObject GetCarStatus(int carID)
    {
        // return the car data json object based on car id and savedata
        return saveData.GetField("cars").list[carID];
    }
    
    public static JSONObject GetCharacterStatus(int charID)
    {
        // return the Character data json object based on character id and savedata
        return saveData.GetField("characters").list[charID];
    }

    public static int GetTotalCoins()
    {
        // return TotalCoins number
        return (int)(saveData.GetField("TotalCoins").n);
    }

    public static void SetTotalCoins(int totalNum)
    {
        // set TotalCoins number
        saveData.SetField("TotalCoins", totalNum);
    }
}
