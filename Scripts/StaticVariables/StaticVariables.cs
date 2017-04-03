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

    //unchanged data, initialize in the beginning
    public static JSONObject cardData = null;
    public static JSONObject carData = null;
    public static JSONObject characterData = null;
    public static JSONObject itemData = null;

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

    public static JSONObject GetCardStatus(string attribute,int cardId)
    {
        // return the card data json object based on its attribute and serial # in the list
        return saveData.GetField("cards").GetField(attribute).list[cardId];
    }

    //call this function when purchasing a card
    public static void AddCard(string attribute, int cardId)
    {
        saveData.GetField("cards").GetField(attribute).Add(new JSONObject("{\"id\":"
            +cardId+",\"equipped\":-1\"price\":"
            +cardData.GetField("cards").list[cardId].GetField("price").n +"}"));
    }

    //call this function when selling a card
    public static void RemoveCard(string attribute, int cardNumber)
    {
        // remove the card data json object based on its attribute and serial # in the list
        saveData.GetField("cards").GetField(attribute).list.RemoveAt(cardNumber);
    }
    
    //call this function when selling a card
    public static void RemoveCard(string attribute, JSONObject cardObj)
    {
        // remove the card data json object based on its attribute in the list
        saveData.GetField("cards").GetField(attribute).list.Remove(cardObj);
    }

    public static int GetTotalCoins()
    {
        // return TotalCoins number
        return (int)(saveData.GetField("totalCoins").n);
    }

    public static void SetTotalCoins(int totalNum)
    {
        // set TotalCoins number
        saveData.SetField("totalCoins", totalNum);
    }
    public static int GetProgress()
    {
        // return TotalCoins number
        return (int)(saveData.GetField("progress").n);
    }

    public static void SetProgress(int progress)
    {
        // set TotalCoins number
        saveData.SetField("progress", progress);
    }
}
