using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariables : MonoBehaviour
{
    public static int ranking = -1;
    public static bool gameIsOver = false;
    public static bool gameStarts = false;
    public static bool musicStartFlag = false;
    public static string raceTimeStr = "";
    public static int coinNumber = 0;
    public static float expGained = 0;
    public static int characterID = 0;
    public static int mapID = 0;
    public static int carID = 0;
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

    public static float GetCurrentCarLevel(int carIndex)
    {
        return saveData.GetField("cars").list[carIndex].GetField("level").n;
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
    public static List<float> GetCurrentCarAttribute(int carIndex)
    {
        List<float> ret = new List<float>();
        float basePoint = carData.GetField("cars").list[carIndex].GetField("baseStatus").GetField("hp").n;
        float levelModifier = carData.GetField("cars").list[carIndex].GetField("statusModifier").GetField("hp").n;
        float levelDiff = GetCurrentCarLevel(carIndex) - 1;

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
    float GetNextLevelCarPrice(int carIndex, int currentLevel = -1)
    {
        if (currentLevel <= 0)
        {
            currentLevel = (int)GetCurrentCarLevel(carIndex);
        }
        if (currentLevel == (int)(carData.GetField("maxLevel").n))
        {
            return -1;
        }
        return carData.GetField("cars").list[carIndex].GetField("price").list[currentLevel].n;
    }

    // return true if it is locked
    public static bool GetLockStatus(int carIndex)
    {
        return carData.GetField("cars").list[carIndex].GetField("unlocked").n < GetProgress();
    }

    public void SetCurrentCarLevel(int carIndex, int currentLevel)
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
        ret.AddField("name", cardData.GetField(attribute).list[cardID].GetField("name").str);
        ret.AddField("type", attribute);
        ret.AddField("number", saveData.GetField("cards").GetField(attribute).list[cardID].GetField("number").n);
        ret.AddField("maxNumber", saveData.GetField("cards").GetField(attribute).list[cardID].GetField("maxNumber").n);
        string rank = cardData[attribute][cardID]["rank"].str;
        ret.AddField("rank", rank);
        ret.AddField("sellPrice", cardData.GetField("price")[rank].n);

        return ret;
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


float GetSellPrice(string rank)
    {
        return cardData["price"][rank].n;
    }

}
