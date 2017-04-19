using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariables : MonoBehaviour
{
    public static int ranking = -1;
    public static bool gameIsOver = false;
    public static bool gameStarts = false;
    public static bool musicStartFlag = false;
    public static bool firstClear = false;
    public static string raceTimeStr = "";
    public static int coinNumber = 0;
    public static float expGained = 0;
    public static float coinModifier = 0;
    public static float expModifier = 0;
    public static int characterID = 0;
    public static int mapID = 0;
    public static int skyBoxID = 0;
    public static int carID = 0;
   // public static string prevSceneName = "";//used for setting go back
    public static JSONObject saveData = null;

    //unchanged data, initialize in the beginning
    public static JSONObject cardData = null;
    public static JSONObject carData = null;
    public static JSONObject characterData = null;

    public static JSONObject cardPackData = null;

    public static void ResetVariables()
    {
        //reset everything for one 'run'
        ranking = -1;
        gameIsOver = false;
        gameStarts = false;
        musicStartFlag = false;
        expGained = 0;
        raceTimeStr = "";
        coinNumber = 0;
        coinModifier = 0;
        expModifier = 0;
        firstClear = false;
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
        return (int)(saveData.GetField("totalCoins").n);
    }

    public static void SetTotalCoins(float totalNum)
    {
        // set TotalCoins number
        saveData.SetField("totalCoins", totalNum);
        //Debug.Log("total coins is now set to " + saveData["totalCoins"].n);
    }
    public static int GetProgress()
    {
        // return TotalCoins number
        return (int)(saveData.GetField("progress").n);
    }

    public static void SetProgress(float progress)
    {
        // set TotalCoins number
        float newProgress = Mathf.Max(progress, saveData["progress"].n);
        saveData.SetField("progress", newProgress);
    }

    public static float GetCurrentCarLevel(int carIndex)
    {
        return saveData.GetField("cars").list[carIndex].GetField("level").n;
    }
    public static float GetCurrentCharLevel(int charIndex)
    {
        return saveData.GetField("characters").list[charIndex].GetField("level").n;
    }

    public static string GetCarSlotAttribute(int carIndex, int slotIndex)
    {
        if (carIndex < 0 || carIndex > carData["cars"].list.Count || slotIndex < 0 || slotIndex > 2)
        {
            return "ERROR";
        }
        return carData["cars"][carIndex]["slotAttributes"][slotIndex].str;
    }


    // return [hp, mp, speed,CD, attack, defense]
    public static List<float> GetCurrentCarAttribute(int carIndex, int lv = -1)
    {
        List<float> ret = new List<float>();
        float basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("hp").n;
        float levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("hp").n;
        float levelDiff = GetCurrentCarLevel(carIndex) - 1;
        if (lv != -1)
        {
            levelDiff = lv - 1;
        }
        

        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("mp").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("mp").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("speed").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("speed").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("CD").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("CD").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("attack").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("attack").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("defense").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("defense").n;
        ret.Add(levelDiff * levelModifier + basePoint);


        return ret;

    }

    // return [hp, mp, speed,CD, attack, defense]
    public static List<float> GetCurrentCharAttribute(int charIndex, int lv = -1)
    {
        List<float> ret = new List<float>();
        float basePoint = characterData.GetField("characters").list[charIndex].GetField("baseStatus").GetField("hp").n;
        float levelModifier = characterData.GetField("characters").list[charIndex].GetField("statusModifier").GetField("hp").n;
        float levelDiff = GetCurrentCarLevel(charIndex) - 1;
        if (lv != -1)
        {
            levelDiff = lv - 1;
        }


        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = characterData.GetField("characters").list[charIndex].GetField("baseStatus").GetField("mp").n;
        levelModifier = characterData.GetField("characters").list[charIndex].GetField("statusModifier").GetField("mp").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = characterData.GetField("characters").list[charIndex].GetField("baseStatus").GetField("speed").n;
        levelModifier = characterData.GetField("characters").list[charIndex].GetField("statusModifier").GetField("speed").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = characterData.GetField("characters").list[charIndex].GetField("baseStatus").GetField("CD").n;
        levelModifier = characterData.GetField("characters").list[charIndex].GetField("statusModifier").GetField("CD").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = characterData.GetField("characters").list[charIndex].GetField("baseStatus").GetField("attack").n;
        levelModifier = characterData.GetField("characters").list[charIndex].GetField("statusModifier").GetField("attack").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = characterData.GetField("characters").list[charIndex].GetField("baseStatus").GetField("defense").n;
        levelModifier = characterData.GetField("characters").list[charIndex].GetField("statusModifier").GetField("defense").n;
        ret.Add(levelDiff * levelModifier + basePoint);


        return ret;

    }
    // return [hp, mp, speed,CD, attack, defense] for next level
    // return null if there is no next level
    public static List<float> GetNextLevelCarAttribute(int carIndex)
    {
        if (GetCurrentCarLevel(carIndex) == carData.GetField("maxLevel").n)
        {
            return null;
        }

        List<float> ret = new List<float>();
        float basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("hp").n;
        float levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("hp").n;
        float levelDiff = GetCurrentCarLevel(carIndex);

        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("mp").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("mp").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("speed").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("speed").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("CD").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("CD").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("attack").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("attack").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("defense").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("defense").n;
        ret.Add(levelDiff * levelModifier + basePoint);


        return ret;

    }


    // return upgrade price
    // return -1 if the car is alredy in max level
    public static float GetNextLevelCarPrice(int carIndex, int currentLevel = -1)
    {
        if (currentLevel <= 0)
        {
            currentLevel = (int)GetCurrentCarLevel(carIndex);
        }
        if (currentLevel == (int)(carData.GetField("maxLevel").n))
        {
            return -1;
        }
        return carData.GetField("cars").list[carIndex].GetField("price").list[currentLevel-1].n;
    }

    // return true if it is locked
    public static bool GetLockStatus(int carIndex)
    {
        //Debug.Log("this car unlocked at stage " + carData.GetField("cars").list[carIndex].GetField("unlocked").n + " current stage: " + GetProgress());
        //Debug.Log("json = " + saveData.ToString());
        return carData.GetField("cars").list[carIndex].GetField("unlocked").n > GetProgress();
    }

    public static void SetCurrentCarLevel(int carIndex, int currentLevel)
    {
        saveData.GetField("cars").list[carIndex].SetField("level", currentLevel);
    }

    // return [hp, mp, speed,CD, attack, defense]
    public static List<float> GetCurrentCarAttributeWithCard(int carIndex)
    {
        List<float> ret = GetCurrentCarAttribute(carIndex);

        float attackModifier = 1;
        float hpModifier = 1;
        float defenseModifier = 1;
        float mpModifier = 1;
        float speedModifier = 1;
        float CDmodifier = 1;

        for (int i = 0; i < carData.GetField("cars").list[carIndex].GetField("slotAttributes").list.Count; ++i)
        {
            string slotAttr = carData.GetField("cars").list[carIndex].GetField("slotAttributes").list[i].str;
            if (slotAttr.Equals("ATK"))
            {
                int cardID = (int)saveData.GetField("cars").list[carIndex].GetField("slots").list[i].n;

                if (cardID != -1)
                {
                    string cardName = cardData.GetField("ATK").list[cardID].GetField("name").str;
                    if (cardName.StartsWith("atkIncrease"))
                    {
                        attackModifier += cardData.GetField("ATK").list[cardID].GetField("attributes").list[0].n;
                    }
                }
            }
            else if (slotAttr.Equals("DEF"))
            {
                int cardID = (int)saveData.GetField("cars").list[carIndex].GetField("slots").list[i].n;
                if (cardID != -1)
                {
                    string cardName = cardData.GetField("DEF").list[cardID].GetField("name").str;
                    if (cardName.StartsWith("increaseHP"))
                    {
                        hpModifier += cardData.GetField("DEF").list[cardID].GetField("attributes").list[0].n;
                    }
                    else if (cardName.StartsWith("defIncrease"))
                    {
                        defenseModifier += cardData.GetField("DEF").list[cardID].GetField("attributes").list[0].n;
                    }
                }
            }
            else if (slotAttr.Equals("SPE"))
            {
                int cardID = (int)saveData.GetField("cars").list[carIndex].GetField("slots").list[i].n;
                if (cardID != -1)
                {

                    string cardName = cardData.GetField("SPE").list[cardID].GetField("name").str;
                    if (cardName.StartsWith("mpIncrease"))
                    {
                        mpModifier += cardData.GetField("SPE").list[cardID].GetField("attributes").list[0].n;
                    }
                    else if (cardName.StartsWith("CDIncrease"))
                    {
                        CDmodifier += cardData.GetField("SPE").list[cardID].GetField("attributes").list[0].n;
                    }
                    else if (cardName.StartsWith("speedIncrease"))
                    {
                        speedModifier += cardData.GetField("SPE").list[cardID].GetField("attributes").list[0].n;
                    }
                    else if (cardName.StartsWith("tradeHealth"))
                    {
                        hpModifier -= cardData.GetField("SPE").list[cardID].GetField("attributes").list[0].n;
                        speedModifier += cardData.GetField("SPE").list[cardID].GetField("attributes").list[1].n;
                    }
                }

            }


        }
        // return [hp, mp, speed,CD, attack, defense]
        ret[0] *= hpModifier;
        ret[1] *= mpModifier;
        ret[2] *= speedModifier;
        ret[3] *= CDmodifier;
        ret[4] *= attackModifier;
        ret[5] *= defenseModifier;

        return ret;
    }


    public static List<float> GetMaxCarAttribute(int carIndex)
    {
        List<float> ret = new List<float>();
        float basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("hp").n;
        float levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("hp").n;
        float levelDiff = carData["maxLevel"].n -1;

        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("mp").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("mp").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("speed").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("speed").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("CD").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("CD").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("attack").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("attack").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("defense").n;
        levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("defense").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        return ret;
    }

    public static List<float> GetCurrentCardInSlot(int carIndex)
    {
        List<float> ret = new List<float>();
        foreach (JSONObject card in saveData.GetField("cars").list[carIndex].GetField("slots").list)
        {
            ret.Add(card.n);
        }
        return ret;
    }

    public static void RemoveCurrentCardInSlot(int carIndex, int slotNumber)
    {
        int cardIdx = (int)GetCurrentCardInSlot(carIndex)[slotNumber];
        if (cardIdx == -1)
        {
            return;
        }
        string attribute = carData.GetField("cars").list[carIndex].GetField("slotAttributes").list[slotNumber].str;
        JSONObject newSlots = new JSONObject(JSONObject.Type.ARRAY);
        for (int i = 0; i < saveData.GetField("cars").list[carIndex].GetField("slots").list.Count; ++i)
        {
            if (i == slotNumber)
            {
                newSlots.Add(-1);
                float number = saveData.GetField("cards").GetField(attribute).list[cardIdx].GetField("number").n;
                float maxNumber = saveData.GetField("cards").GetField(attribute).list[cardIdx].GetField("maxNumber").n;
                saveData.GetField("cards").GetField(attribute).list[cardIdx].SetField("number", Mathf.Min(number + 1, maxNumber));
            }
            else
            {
                newSlots.Add(saveData.GetField("cars").list[carIndex].GetField("slots").list[i]);
            }
        }
        saveData.GetField("cars").list[carIndex].SetField("slots", newSlots);
    }

    public static void SetCurrentCardInSlot(int carIndex, int slotNumber, int cardID)
    {
        int oldCardIdx = (int)GetCurrentCardInSlot(carIndex)[slotNumber];
        string attribute = carData.GetField("cars").list[carIndex].GetField("slotAttributes").list[slotNumber].str;

        if (oldCardIdx != -1 && saveData.GetField("cards").GetField(attribute).list[oldCardIdx].GetField("number").n <= 0)
        {
            // no available cards
            return;
        }
       
        JSONObject newSlots = new JSONObject(JSONObject.Type.ARRAY);
        for (int i = 0; i < saveData.GetField("cars").list[carIndex].GetField("slots").list.Count; ++i)
        {
            if (i == slotNumber)
            {
                newSlots.Add(cardID);
                float newNumber = saveData.GetField("cards").GetField(attribute).list[cardID].GetField("number").n;
                if (newNumber <= 0)
                {
                    return;
                }
                //remove old card from slot
                if (oldCardIdx != -1)
                {
                    float oldNumber = saveData.GetField("cards").GetField(attribute).list[oldCardIdx].GetField("number").n;
                    float oldMaxNumber = saveData.GetField("cards").GetField(attribute).list[oldCardIdx].GetField("maxNumber").n;
                    saveData.GetField("cards").GetField(attribute).list[oldCardIdx].SetField("number", Mathf.Min(oldNumber + 1, oldMaxNumber));
                }
                //insert new card to slot
                newNumber = saveData.GetField("cards").GetField(attribute).list[cardID].GetField("number").n;
                saveData.GetField("cards").GetField(attribute).list[cardID].SetField("number", Mathf.Max(newNumber - 1, 0));
            }
            else
            {
                newSlots.Add(saveData.GetField("cars").list[carIndex].GetField("slots").list[i]);
            }
        }
        saveData.GetField("cars").list[carIndex].SetField("slots", newSlots);
    }

    public static JSONObject GetCardInfo(string attribute, int cardID)
    {
        /*
         * return type:
         * {
         * "shortName"Lstring,
         *  "name":string,
         *  "type":string,
         *  "number":float,
         *  "maxNumber":float,
         *  "rank":string,
         *  "sellPrice":float
         * }
         */
        if (!attribute.Equals("ATK") && !attribute.Equals("DEF") && !attribute.Equals("SPE"))
        {
            Debug.LogError("Attribute format is wrong(\"ATK\",\"DEF\" or \"SPE\" ) " + attribute);
            return null;
        }
        JSONObject ret = new JSONObject(JSONObject.Type.OBJECT);
        ret.AddField("shortName", cardData.GetField(attribute).list[cardID].GetField("shortName").str);

        ret.AddField("name", cardData.GetField(attribute).list[cardID].GetField("name").str);
        ret.AddField("type", attribute);
        ret.AddField("number", saveData.GetField("cards").GetField(attribute).list[cardID].GetField("number").n);
        ret.AddField("maxNumber", saveData.GetField("cards").GetField(attribute).list[cardID].GetField("maxNumber").n);
        string rank = cardData[attribute][cardID]["rank"].str;
        ret.AddField("rank", rank);
        ret.AddField("sellPrice", cardData.GetField("price")[rank].n);

        return ret;
    }

    // return [hp, mp, speed,CD, attack, defense]
    public static List<float> GetMaxCharacterAttribute(int charID)
    {

        List<float> ret = new List<float>();
        float basePoint = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("hp").n;
        float levelModifier = characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("hp").n;
        float levelDiff = characterData["maxLevel"].n - 1;

        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("mp").n;
        levelModifier = characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("mp").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("speed").n;
        levelModifier = characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("speed").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("CD").n;
        levelModifier = characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("CD").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("attack").n;
        levelModifier = characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("attack").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("defense").n;
        levelModifier = characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("defense").n;
        ret.Add(levelDiff * levelModifier + basePoint);

        return ret;
    }

    public static float GetMaxCarLv(int carID)
    {
        return carData["maxLevel"].n;
    }
    public static float GetMaxCharacterLv(int carID)
    {
        return characterData["maxLevel"].n;
    }
    public static JSONObject GetCharacterAttribute(int charID)
    {
        /*
         * return type:
         * {
         *  "unlocked":bool,
         *  "level":float,
         *  "currentExp":float,
         *  "maxExpForThisLevel":float,
         *  "hp":float,
         *  "mp":float,
         *  "speed":float,
         *  "CD":float,
         *  "attack":float.
         *  "defense":float,
         *  "skillName":string
         * }
         */
        JSONObject ret = new JSONObject(JSONObject.Type.OBJECT);

        ret.AddField("unlocked", GetProgress() >= characterData.GetField("characters").list[charID].GetField("unlocked").n);
        //Debug.Log("progress = " + GetProgress() + " char unlock at " + characterData.GetField("characters").list[charID].GetField("unlocked").n + ";unlocked?" + ret["unlocked"].b);
        ret.AddField("level", saveData.GetField("characters").list[charID].GetField("level").n);
        ret.AddField("currentExp", saveData.GetField("characters").list[charID].GetField("exp").n);
        float lvlDiff = saveData.GetField("characters").list[charID].GetField("level").n - 1;
        float maxExp = characterData.GetField("baseExp").n * Mathf.Pow(characterData.GetField("expModifier").n, lvlDiff);
        ret.AddField("maxExpForThisLevel", maxExp);

        float hp = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("hp").n + lvlDiff * characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("hp").n;
        ret.AddField("hp", hp);
        float mp = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("mp").n + lvlDiff * characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("mp").n;
        ret.AddField("mp", mp);
        float speed = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("speed").n + lvlDiff * characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("speed").n;
        ret.AddField("speed", speed);
        float attack = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("attack").n + lvlDiff * characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("attack").n;
        ret.AddField("attack", attack);
        float CD = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("CD").n + lvlDiff * characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("CD").n;
        ret.AddField("CD", CD);
        float defense = characterData.GetField("characters").list[charID].GetField("baseStatus").GetField("defense").n + lvlDiff * characterData.GetField("characters").list[charID].GetField("statusModifier").GetField("defense").n;
        ret.AddField("defense", defense);
        ret.AddField("skillName", characterData.GetField("characters").list[charID].GetField("skillName").str);

        return ret;
    }

    public static string GetCardRank(string attribute, int id)
    {
        return cardData[attribute][id]["rank"].str;
    }
    public static string GetCardDescription(string attribute, int id)
    {
        return cardData[attribute][id]["description"].str;
    }
    public static float GetSellPrice(string rank)
    {
        return cardData["price"][rank].n;
    }

    private static string NumToTimeStr(float n)
    {
        if (n == -1)
        {
            return "--";
        }
        string seconds = (n % 60).ToString();
        string minStr = Mathf.RoundToInt(n / 60).ToString();
        return minStr + ":" + seconds;
    }

    public static string GetMaxRecordOfMap(int mapID)
    {
        return NumToTimeStr(saveData["records"][mapID].n);
    }


    public static void SetMaxRecordOfMap(int mapID,float record)
    {
        JSONObject newRecords = new JSONObject(JSONObject.Type.ARRAY);

        for (int i=0;i<saveData["records"].list.Count;++i)
        {
            if (i == mapID&& ((record < saveData["records"].list[i].n) || saveData["records"].list[i].n==-1))
            {
                newRecords.Add(record);
            }
            else
            {
                newRecords.Add(saveData["records"].list[i].n);
            }
        }
        saveData.SetField("records", newRecords);
    }

    // return true if mapID is locked
    public static bool GetMapLockStatus(int mapID)
    {
        return saveData["progress"].n +1 < mapID;
    }

    public static void SetVolume(float volume)
    {
        if (volume >= 0 && volume <= 1)
        {
            saveData.SetField("volume", volume);
        }
    }


    public static JSONObject GetCardList()
    {
        // Get the information of current holding card status
        return saveData.GetField("cards");
    }

    public static int GetCardCount(string attribute, int id)
    {
        if (!attribute.Equals("ATK") && !attribute.Equals("DEF") && !attribute.Equals("SPE"))
        {
            Debug.LogError("Attribute format is wrong(\"ATK\",\"DEF\" or \"SPE\" ) " + attribute);
            return -1;
        }
        if (id < 0 || id >= saveData.GetField("cards").GetField(attribute).Count)
        {
            Debug.LogError("Invalid card id:" + attribute + " " + id);
            return -1;
        }
        return (int)saveData.GetField("cards").GetField(attribute).list[id].GetField("number").n;
    }

    public static void SetCardCount(string attribute, int id, int count)
    {
        if (count < 0)
        {
            Debug.LogError("Setting card count to negative number");
            return;
        }
        if (!attribute.Equals("ATK") && !attribute.Equals("DEF") && !attribute.Equals("SPE"))
        {
            Debug.LogError("Attribute format is wrong(\"ATK\",\"DEF\" or \"SPE\" ) " + attribute);
            return;
        }
        if (id < 0 || id >= saveData.GetField("cards").GetField(attribute).Count)
        {
            Debug.LogError("Invalid card id:" + attribute + " " + id);
            return;
        }
        saveData.GetField("cards").GetField(attribute).list[id].SetField("number", count);
    }
    public static int GetCardMaxCount(string attribute, int id)
    {
        if (!attribute.Equals("ATK") && !attribute.Equals("DEF") && !attribute.Equals("SPE"))
        {
            Debug.LogError("Attribute format is wrong(\"ATK\",\"DEF\" or \"SPE\" ) " + attribute);
            return -1;
        }
        if (id < 0 || id >= saveData.GetField("cards").GetField(attribute).Count)
        {
            Debug.LogError("Invalid card id:" + attribute + " " + id);
            return -1;
        }
        return (int)saveData.GetField("cards").GetField(attribute).list[id].GetField("maxNumber").n;
    }

    public static void SetCardMaxCount(string attribute, int id, int count)
    {
        if (count < 0)
        {
            Debug.LogError("Setting card count to negative number");
            return;
        }
        if (!attribute.Equals("ATK") && !attribute.Equals("DEF") && !attribute.Equals("SPE"))
        {
            Debug.LogError("Attribute format is wrong(\"ATK\",\"DEF\" or \"SPE\" ) " + attribute);
            return;
        }
        if (id < 0 || id >= saveData.GetField("cards").GetField(attribute).Count)
        {
            Debug.LogError("Invalid card id:" + attribute + " " + id);
            return;
        }
        saveData.GetField("cards").GetField(attribute).list[id].SetField("maxNumber", count);
    }


}
