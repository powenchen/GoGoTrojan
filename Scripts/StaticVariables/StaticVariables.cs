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
        return saveData["cars"].list[carID];
    }
    
    public static JSONObject GetCharacterStatus(int charID)
    {
        // return the Character data json object based on character id and savedata
        return saveData["characters"].list[charID];
    }
    

    public static int GetTotalCoins()
    {
        // return TotalCoins number
        return (int)(saveData["totalCoins"].n);
    }

    public static void SetTotalCoins(float totalNum)
    {
        // set TotalCoins number
        saveData.SetField("totalCoins", totalNum);
    }
    public static int GetProgress()
    {
        // return TotalCoins number
        return (int)(saveData["progress"].n);
    }

    public static void SetProgress(int progress)
    {
        // set TotalCoins number
        saveData.SetField("progress", progress);
    }

    public static float GetCurrentCarLevel(int carIndex)
    {
        return saveData["cars"].list[carIndex]["level"].n;
    }

    // return [hp, mp, speed,CD, attack, defense]
    public static List<float> GetCurrentCarAttribute(int carIndex)
    {
        List<float> ret = new List<float>();
        float basePoint = carData["cars"].list[carIndex]["baseStatus"]["hp"].n;
        float levelModifier = carData["cars"].list[carIndex]["statusModifier"]["hp"].n;
        float levelDiff = GetCurrentCarLevel(carIndex) - 1;

        ret.Add(levelDiff * levelModifier + basePoint );

        basePoint = carData["cars"].list[carIndex]["baseStatus"]["mp"].n;
        levelModifier = carData["cars"].list[carIndex]["statusModifier"]["mp"].n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData["cars"].list[carIndex]["baseStatus"]["speed"].n;
        levelModifier = carData["cars"].list[carIndex]["statusModifier"]["speed"].n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData["cars"].list[carIndex]["baseStatus"]["CD"].n;
        levelModifier = carData["cars"].list[carIndex]["statusModifier"]["CD"].n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData["cars"].list[carIndex]["baseStatus"]["attack"].n;
        levelModifier = carData["cars"].list[carIndex]["statusModifier"]["attack"].n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData["cars"].list[carIndex]["baseStatus"]["defense"].n;
        levelModifier = carData["cars"].list[carIndex]["statusModifier"]["defense"].n;
        ret.Add(levelDiff * levelModifier + basePoint);


        return ret;

    }
    // return [hp, mp, speed,CD, attack, defense] for next level
    // return null if there is no next level
    public static List<float> GetNextLevelCarAttribute(int carIndex)
    {
        if (GetCurrentCarLevel(carIndex) == carData["maxLevel"].n)
        {
            return null;
        }

        List<float> ret = new List<float>();
        float basePoint = carData["cars"].list[carIndex]["baseStatus"]["hp"].n;
        float levelModifier = carData["cars"].list[carIndex]["statusModifier"]["hp"].n;
        float levelDiff = GetCurrentCarLevel(carIndex);

        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData["cars"].list[carIndex]["baseStatus"]["mp"].n;
        levelModifier = carData["cars"].list[carIndex]["statusModifier"]["mp"].n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData["cars"].list[carIndex]["baseStatus"]["speed"].n;
        levelModifier = carData["cars"].list[carIndex]["statusModifier"]["speed"].n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData["cars"].list[carIndex]["baseStatus"]["CD"].n;
        levelModifier = carData["cars"].list[carIndex]["statusModifier"]["CD"].n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData["cars"].list[carIndex]["baseStatus"]["attack"].n;
        levelModifier = carData["cars"].list[carIndex]["statusModifier"]["attack"].n;
        ret.Add(levelDiff * levelModifier + basePoint);

        basePoint = carData["cars"].list[carIndex]["baseStatus"]["defense"].n;
        levelModifier = carData["cars"].list[carIndex]["statusModifier"]["defense"].n;
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
        if (currentLevel == (int)(carData["maxLevel"].n))
        {
            return -1;
        }
        return carData["cars"].list[carIndex]["price"].list[currentLevel].n;
    }

    // return true if it is locked
    public static bool GetLockStatus(int carIndex)
    {
        return carData["cars"].list[carIndex]["unlocked"].n < GetProgress();
    }

    public void SetCurrentCarLevel(int carIndex, int currentLevel)
    {
        saveData["cars"].list[carIndex].SetField("level", currentLevel);
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

        for ( int i = 0;i< carData["cars"].list[carIndex]["slotAttributes"].list.Count; ++i)
        {
            string slotAttr = carData["cars"].list[carIndex]["slotAttributes"].list[i].str;
            if (slotAttr.Equals("ATK"))
            {
                int cardID = (int)saveData["cars"].list[carIndex]["slots"].list[i].n;
                string cardName = cardData["ATK"].list[cardID]["name"].str;
                if (cardName.StartsWith("atkIncrease"))
                {
                    attackModifier += cardData["ATK"].list[cardID]["attributes"].list[0].n;
                }
            }
            else if (slotAttr.Equals("DEF"))
            {
                int cardID = (int)saveData["cars"].list[carIndex]["slots"].list[i].n;
                string cardName = cardData["DEF"].list[cardID]["name"].str;
                if (cardName.StartsWith("increaseHP"))
                {
                    hpModifier += cardData["DEF"].list[cardID]["attributes"].list[0].n;
                }
                else if (cardName.StartsWith("defIncrease"))
                {
                    defenseModifier += cardData["DEF"].list[cardID]["attributes"].list[0].n;
                }
            }
            else if (slotAttr.Equals("SPE"))
            {
                int cardID = (int)saveData["cars"].list[carIndex]["slots"].list[i].n;
                string cardName = cardData["SPE"].list[cardID]["name"].str;
                if (cardName.StartsWith("mpIncrease"))
                {
                    mpModifier += cardData["SPE"].list[cardID]["attributes"].list[0].n;
                }
                else if (cardName.StartsWith("CDIncrease"))
                {
                    CDmodifier += cardData["SPE"].list[cardID]["attributes"].list[0].n;
                }
                else if (cardName.StartsWith("speedIncrease"))
                {
                    speedModifier += cardData["SPE"].list[cardID]["attributes"].list[0].n;
                }
                else if (cardName.StartsWith("tradeHealth"))
                {
                    hpModifier -= cardData["SPE"].list[cardID]["attributes"].list[0].n;
                    speedModifier += cardData["SPE"].list[cardID]["attributes"].list[1].n;
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
        foreach (JSONObject card in saveData["cars"].list[carIndex]["slots"].list)
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
        string attribute = carData["cars"].list[carIndex]["slotAttributes"].list[slotNumber].str;
        JSONObject newSlots = new JSONObject(JSONObject.Type.ARRAY);
        for(int i=0;i< saveData["cars"].list[carIndex]["slots"].list.Count;++i)
        {
            if (i == slotNumber)
            {
                newSlots.Add(-1);
                float number = saveData["cards"][attribute].list[cardIdx]["number"].n;
                float maxNumber = saveData["cards"][attribute].list[cardIdx]["maxNumber"].n;
                saveData["cards"][attribute].list[cardIdx].SetField("number", Mathf.Min(number + 1, maxNumber));
            }
            else
            {
                newSlots.Add(saveData["cars"].list[carIndex]["slots"].list[i]);
            }
        }
        saveData["cars"].list[carIndex].SetField("slots", newSlots);
    }

    public static void SetCurrentCardInSlot(int carIndex, int slotNumber, int cardID)
    {
        int oldCardIdx = (int)GetCurrentCardInSlot(carIndex)[slotNumber];
        string attribute = carData["cars"].list[carIndex]["slotAttributes"].list[slotNumber].str;

        if (saveData["cards"][attribute].list[oldCardIdx]["number"].n <= 0)
        {
            // no available cards
            return;
        }

        JSONObject newSlots = new JSONObject(JSONObject.Type.ARRAY);
        for (int i = 0; i < saveData["cars"].list[carIndex]["slots"].list.Count; ++i)
        {
            if (i == slotNumber)
            {
                newSlots.Add(cardID);
                float oldNumber = saveData["cards"][attribute].list[oldCardIdx]["number"].n;
                float oldMaxNumber = saveData["cards"][attribute].list[oldCardIdx]["maxNumber"].n;
                float newNumber = saveData["cards"][attribute].list[cardID]["number"].n;

                //remove old card from slot
                saveData["cards"][attribute].list[oldCardIdx].SetField("number", Mathf.Min(oldNumber + 1, oldMaxNumber));
                //insert new card to slot
                saveData["cards"][attribute].list[cardID].SetField("number", Mathf.Max(newNumber - 1, 0));
            }
            else
            {
                newSlots.Add(saveData["cars"].list[carIndex]["slots"].list[i]);
            }
        }
        saveData["cars"].list[carIndex].SetField("slots", newSlots);
    }

    public static JSONObject GetCardInfo(string attribute, int cardID)
    {
        /*
         * return type:
         * {
         *  "name":string,
         *  "type":string,
         *  "number":float,
         *  "maxNumber":float
         * }
         */
        if (!attribute.Equals("ATK") && !attribute.Equals("DEF") && !attribute.Equals("SPE"))
        {
            Debug.LogError("Attribute format is wrong(\"ATK\",\"DEF\" or \"SPE\" ) " + attribute);
            return null;
        }
        JSONObject ret = new JSONObject(JSONObject.Type.OBJECT);
        ret.AddField("name",cardData[attribute].list[cardID]["name"].str);
        ret.AddField("type",attribute);
        ret.AddField("number",saveData["cards"][attribute].list[cardID]["number"].n);
        ret.AddField("maxNumber", saveData["cards"][attribute].list[cardID]["maxNumber"].n);

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
        ret.AddField("unlocked", GetProgress() >= characterData["characters"].list[charID]["unlocked"].n);
        ret.AddField("level", saveData["characters"].list[charID]["level"].n);
        ret.AddField("currentExp", saveData["characters"].list[charID]["exp"].n);
        float lvlDiff = saveData["characters"].list[charID]["level"].n - 1;
        float maxExp = characterData["baseExp"].n * Mathf.Pow(characterData["expModifier"].n, lvlDiff);
        ret.AddField("maxExpForThisLevel", maxExp);

        float hp = characterData["characters"].list[charID]["baseStatus"]["hp"].n + lvlDiff * characterData["characters"].list[charID]["statusModifier"]["hp"].n;
        ret.AddField("hp", hp);
        float mp = characterData["characters"].list[charID]["baseStatus"]["mp"].n + lvlDiff * characterData["characters"].list[charID]["statusModifier"]["mp"].n;
        ret.AddField("mp", mp);
        float speed = characterData["characters"].list[charID]["baseStatus"]["speed"].n + lvlDiff * characterData["characters"].list[charID]["statusModifier"]["speed"].n;
        ret.AddField("speed", speed);
        float attack = characterData["characters"].list[charID]["baseStatus"]["attack"].n + lvlDiff * characterData["characters"].list[charID]["statusModifier"]["attack"].n;
        ret.AddField("attack", attack);
        float CD = characterData["characters"].list[charID]["baseStatus"]["CD"].n + lvlDiff * characterData["characters"].list[charID]["statusModifier"]["CD"].n;
        ret.AddField("CD", CD);
        float defense = characterData["characters"].list[charID]["baseStatus"]["defense"].n + lvlDiff * characterData["characters"].list[charID]["statusModifier"]["defense"].n;
        ret.AddField("defense", defense);
        ret.AddField("skillName", characterData["characters"].list[charID]["skillName"].str);
        
        return ret;
    }

    float GetSellPrice(string rank)
    {
        return cardData["price"][rank].n;
    }

}
