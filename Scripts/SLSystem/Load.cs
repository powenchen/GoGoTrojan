using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load : MonoBehaviour {

    // hard code these init jsons
    private static string savedataInit = "{\"characters\":[{\"level\":1,\"exp\":0},{\"level\":1,\"exp\":0},{\"level\":1,\"exp\":0},{\"level\":1,\"exp\":0},{\"level\":1,\"exp\":0},{\"level\":1,\"exp\":0}],\"cars\":[{\"level\":1,\"slots\":[-1,-1,-1]},{\"level\":1,\"slots\":[-1,-1,-1]},{\"level\":1,\"slots\":[-1,-1,-1]},{\"level\":1,\"slots\":[-1,-1,-1]},{\"level\":1,\"slots\":[-1,-1,-1]}],\"progress\":-1,\"totalCoins\":0,\"cards\":{\"ATK\":[{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0}],\"DEF\":[{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0}],\"SPE\":[{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0}]}}";
	private static string carsInit = "{\"maxLevel\":6,\"cars\":[{\"name\":\"Learner\",\"unlocked\":-1,\"passiveName\":\"EXP * 2\",\"baseStatus\":{\"hp\":35,\"mp\":30,\"speed\":35,\"attack\":30,\"CD\":35,\"defense\":35},\"statusModifier\":{\"hp\":4,\"mp\":4,\"speed\":4,\"attack\":4,\"CD\":4,\"defense\":4},\"price\":[300,600,900,1200,1500],\"slotAttributes\":[\"ATK\",\"SPE\",\"DEF\"]},{\"name\":\"Earner\",\"unlocked\":1,\"passiveName\":\"coin * 2\",\"baseStatus\":{\"hp\":35,\"mp\":50,\"speed\":40,\"attack\":35,\"CD\":40,\"defense\":40},\"statusModifier\":{\"hp\":4,\"mp\":6,\"speed\":5,\"attack\":4,\"CD\":5,\"defense\":5},\"price\":[400,800,1200,1600,2000],\"slotAttributes\":[\"SPE\",\"DEF\",\"SPE\"]},{\"name\":\"Producer\",\"unlocked\":2,\"passiveName\":\"CDR * 1.5\",\"baseStatus\":{\"hp\":40,\"mp\":35,\"speed\":50,\"attack\":40,\"CD\":40,\"defense\":40},\"statusModifier\":{\"hp\":5,\"mp\":4,\"speed\":6,\"attack\":5,\"CD\":5,\"defense\":5},\"price\":[500,1000,1500,2000,2500],\"slotAttributes\":[\"SPE\",\"ATK\",\"SPE\"]},{\"name\":\"Healer\",\"unlocked\":3,\"passiveName\":\"HP++ per second\",\"baseStatus\":{\"hp\":45,\"mp\":40,\"speed\":40,\"attack\":40,\"CD\":35,\"defense\":50},\"statusModifier\":{\"hp\":5,\"mp\":5,\"speed\":5,\"attack\":5,\"CD\":4,\"defense\":6},\"price\":[600,1200,1800,2400,3000],\"slotAttributes\":[\"DEF\",\"SPE\",\"DEF\"]},{\"name\":\"Raider\",\"unlocked\":4,\"passiveName\":\"Attack * 1.2\",\"baseStatus\":{\"hp\":45,\"mp\":40,\"speed\":45,\"attack\":50,\"CD\":40,\"defense\":45},\"statusModifier\":{\"hp\":5,\"mp\":5,\"speed\":5,\"attack\":6,\"CD\":5,\"defense\":5},\"price\":[700,1400,2100,2800,3500],\"slotAttributes\":[\"ATK\",\"SPE\",\"ATK\"]}]}";
    private static string cardInit = "{\"price\":{\"C\":100,\"B\":500,\"A\":1000,\"S\":3000},\"ATK\":[{\"name\":\"atkIncrease10\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"atkIncrease20\",\"rank\":\"B\",\"attributes\":[0.2]},{\"name\":\"atkIncrease30\",\"rank\":\"A\",\"attributes\":[0.3]},{\"name\":\"atkIncrease50\",\"rank\":\"S\",\"attributes\":[0.5]},{\"name\":\"lifeSteal25\",\"rank\":\"C\",\"attributes\":[0.25]},{\"name\":\"lifeSteal40\",\"rank\":\"B\",\"attributes\":[0.4]},{\"name\":\"lifeSteal55\",\"rank\":\"A\",\"attributes\":[0.55]},{\"name\":\"lifeSteal80\",\"rank\":\"S\",\"attributes\":[0.8]},{\"name\":\"poison2For5\",\"rank\":\"C\",\"attributes\":[0.02,5]},{\"name\":\"poison3For5\",\"rank\":\"B\",\"attributes\":[0.03,5]},{\"name\":\"poison4For5\",\"rank\":\"A\",\"attributes\":[0.04,5]},{\"name\":\"poison6For5\",\"rank\":\"S\",\"attributes\":[0.06,5]},{\"name\":\"stun1\",\"rank\":\"S\",\"attributes\":[1]},{\"name\":\"singleToGlobal50\",\"rank\":\"S\",\"attributes\":[0.5]},{\"name\":\"speedReuction20\",\"rank\":\"C\",\"attributes\":[0.2,2]},{\"name\":\"speedReuction30\",\"rank\":\"B\",\"attributes\":[0.3,2]},{\"name\":\"speedReuction40\",\"rank\":\"A\",\"attributes\":[0.4,2]},{\"name\":\"speedReuction60\",\"rank\":\"S\",\"attributes\":[0.6,2]},{\"name\":\"crit20Dam1.5\",\"rank\":\"C\",\"attributes\":[0.2,1.5]},{\"name\":\"crit30Dam1.5\",\"rank\":\"B\",\"attributes\":[0.3,1.5]},{\"name\":\"crit30Dam2\",\"rank\":\"A\",\"attributes\":[0.3,2]},{\"name\":\"crit50Dam2\",\"rank\":\"S\",\"attributes\":[0.5,2]},{\"name\":\"trade20Health25Atk\",\"rank\":\"C\",\"attributes\":[0.2,0.25]},{\"name\":\"trade30Health50Atk\",\"rank\":\"B\",\"attributes\":[0.3,0.5]},{\"name\":\"trade30Health60Atk\",\"rank\":\"A\",\"attributes\":[0.3,0.6]},{\"name\":\"trade50Health100Atk\",\"rank\":\"S\",\"attributes\":[0.5,1]},{\"name\":\"increaseAccu15\",\"rank\":\"C\",\"attributes\":[0.15]},{\"name\":\"increaseAccu20\",\"rank\":\"B\",\"attributes\":[0.2]},{\"name\":\"increaseAccu25\",\"rank\":\"A\",\"attributes\":[0.25]},{\"name\":\"increaseAccu40\",\"rank\":\"S\",\"attributes\":[0.4]},{\"name\":\"atkPen20\",\"rank\":\"C\",\"attributes\":[20]},{\"name\":\"atkPen40\",\"rank\":\"B\",\"attributes\":[40]},{\"name\":\"atkPen60\",\"rank\":\"A\",\"attributes\":[60]},{\"name\":\"atkPen100\",\"rank\":\"S\",\"attributes\":[100]}],\"DEF\":[{\"name\":\"increaseHP10\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"increaseHP30\",\"rank\":\"B\",\"attributes\":[0.3]},{\"name\":\"increaseHP50\",\"rank\":\"A\",\"attributes\":[0.5]},{\"name\":\"increaseHP80\",\"rank\":\"S\",\"attributes\":[0.8]},{\"name\":\"hpRecover3\",\"rank\":\"C\",\"attributes\":[0.03]},{\"name\":\"hpRecover5\",\"rank\":\"B\",\"attributes\":[0.05]},{\"name\":\"hpRecover7\",\"rank\":\"A\",\"attributes\":[0.07]},{\"name\":\"hpRecover10\",\"rank\":\"S\",\"attributes\":[0.1]},{\"name\":\"damRedction10\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"damRedction15\",\"rank\":\"B\",\"attributes\":[0.15]},{\"name\":\"damRedction20\",\"rank\":\"A\",\"attributes\":[0.2]},{\"name\":\"damRedction40\",\"rank\":\"S\",\"attributes\":[0.4]},{\"name\":\"reflect50\",\"rank\":\"S\",\"attributes\":[0.5]},{\"name\":\"revive1\",\"rank\":\"S\",\"attributes\":[1]},{\"name\":\"startWithShield1\",\"rank\":\"C\",\"attributes\":[1]},{\"name\":\"startWithShield2\",\"rank\":\"B\",\"attributes\":[2]},{\"name\":\"startWithShield3\",\"rank\":\"A\",\"attributes\":[3]},{\"name\":\"startWithShield5\",\"rank\":\"S\",\"attributes\":[5]},{\"name\":\"defIncrease10\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"defIncrease30\",\"rank\":\"B\",\"attributes\":[0.3]},{\"name\":\"defIncrease50\",\"rank\":\"A\",\"attributes\":[0.5]},{\"name\":\"defIncrease100\",\"rank\":\"S\",\"attributes\":[1]},{\"name\":\"evationIncrease10\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"evationIncrease20\",\"rank\":\"B\",\"attributes\":[0.2]},{\"name\":\"evationIncrease30\",\"rank\":\"A\",\"attributes\":[0.3]},{\"name\":\"evationIncrease50\",\"rank\":\"S\",\"attributes\":[0.5]}],\"SPE\":[{\"name\":\"mpIncrease10\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"mpIncrease30\",\"rank\":\"B\",\"attributes\":[0.3]},{\"name\":\"mpIncrease50\",\"rank\":\"A\",\"attributes\":[0.5]},{\"name\":\"mpIncrease100\",\"rank\":\"S\",\"attributes\":[1]},{\"name\":\"mpRecoverIncrease3\",\"rank\":\"C\",\"attributes\":[0.03]},{\"name\":\"mpRecoverIncrease5\",\"rank\":\"B\",\"attributes\":[0.05]},{\"name\":\"mpRecoverIncrease7\",\"rank\":\"A\",\"attributes\":[0.07]},{\"name\":\"mpRecoverIncrease10\",\"rank\":\"S\",\"attributes\":[0.1]},{\"name\":\"mpCostReduction10\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"mpCostReduction20\",\"rank\":\"B\",\"attributes\":[0.2]},{\"name\":\"mpCostReduction30\",\"rank\":\"A\",\"attributes\":[0.3]},{\"name\":\"mpCostReduction50\",\"rank\":\"S\",\"attributes\":[0.5]},{\"name\":\"speedIncrease10\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"speedIncrease15\",\"rank\":\"B\",\"attributes\":[0.15]},{\"name\":\"speedIncrease20\",\"rank\":\"A\",\"attributes\":[0.2]},{\"name\":\"speedIncrease30\",\"rank\":\"S\",\"attributes\":[0.3]},{\"name\":\"coinIncrease60\",\"rank\":\"C\",\"attributes\":[0.6]},{\"name\":\"coinIncrease80\",\"rank\":\"B\",\"attributes\":[0.8]},{\"name\":\"coinIncrease100\",\"rank\":\"A\",\"attributes\":[1]},{\"name\":\"coinIncrease150\",\"rank\":\"S\",\"attributes\":[1.5]},{\"name\":\"expIncrease60\",\"rank\":\"C\",\"attributes\":[0.6]},{\"name\":\"expIncrease80\",\"rank\":\"B\",\"attributes\":[0.8]},{\"name\":\"expIncrease100\",\"rank\":\"A\",\"attributes\":[1]},{\"name\":\"expIncrease150\",\"rank\":\"S\",\"attributes\":[1.5]},{\"name\":\"healthBelow40SpeedInc20\",\"rank\":\"C\",\"attributes\":[0.4,0.2]},{\"name\":\"healthBelow40SpeedInc30\",\"rank\":\"B\",\"attributes\":[0.4,0.3]},{\"name\":\"healthBelow30SpeedInc40\",\"rank\":\"A\",\"attributes\":[0.3,0.4]},{\"name\":\"healthBelow20SpeedInc50\",\"rank\":\"S\",\"attributes\":[0.2,0.5]},{\"name\":\"tradeHealth5Speed5\",\"rank\":\"C\",\"attributes\":[0.05,0.05]},{\"name\":\"tradeHealth10Speed10\",\"rank\":\"B\",\"attributes\":[0.1,0.1]},{\"name\":\"tradeHealth15Speed15\",\"rank\":\"A\",\"attributes\":[0.15,0.15]},{\"name\":\"tradeHealth25Speed25\",\"rank\":\"S\",\"attributes\":[0.25,0.25]}],\"cardProbability\":{\"Normal\":{\"cost\":100,\"probability\":{\"anyC\":0.5,\"anyB\":0.4,\"anyA\":0.09,\"anyS\":0.01}},\"Gold\":{\"cost\":500,\"probability\":{\"anyC\":0.25,\"anyB\":0.35,\"anyA\":0.3,\"anyS\":0.1}},\"ATK\":{\"cost\":250,\"probability\":{\"atkC\":0.5,\"atkB\":0.4,\"atkA\":0.09,\"atkS\":0.01}},\"DEF\":{\"cost\":250,\"probability\":{\"defC\":0.5,\"defB\":0.4,\"defA\":0.09,\"defS\":0.01}},\"SPE\":{\"cost\":250,\"probability\":{\"speC\":0.5,\"speB\":0.4,\"speA\":0.09,\"speS\":0.01}}}}";
	private static string characterInit = "{\"maxLevel\":15,\"baseExp\":100,\"expModifier\":1.4,\"characters\":[{\"unlocked\":-1,\"skillName\":\"dragonBreath\",\"baseStatus\":{\"hp\":35,\"mp\":50,\"speed\":40,\"attack\":40,\"CD\":45,\"defense\":35},\"statusModifier\":{\"hp\":4,\"mp\":6,\"speed\":5,\"attack\":5,\"CD\":5,\"defense\":4}},{\"unlocked\":1,\"skillName\":\"goldAttack\",\"baseStatus\":{\"hp\":30,\"mp\":40,\"speed\":50,\"attack\":35,\"CD\":45,\"defense\":40},\"statusModifier\":{\"hp\":4,\"mp\":5,\"speed\":6,\"attack\":4,\"CD\":5,\"defense\":5}},{\"unlocked\":2,\"skillName\":\"shield\",\"baseStatus\":{\"hp\":50,\"mp\":45,\"speed\":40,\"attack\":35,\"CD\":30,\"defense\":45},\"statusModifier\":{\"hp\":6,\"mp\":5,\"speed\":5,\"attack\":4,\"CD\":4,\"defense\":5}},{\"unlocked\":3,\"skillName\":\"freeze\",\"baseStatus\":{\"hp\":45,\"mp\":40,\"speed\":35,\"attack\":45,\"CD\":35,\"defense\":50},\"statusModifier\":{\"hp\":5,\"mp\":5,\"speed\":4,\"attack\":5,\"CD\":4,\"defense\":6}},{\"unlocked\":5,\"skillName\":\"nitro\",\"baseStatus\":{\"hp\":40,\"mp\":30,\"speed\":40,\"attack\":50,\"CD\":35,\"defense\":40},\"statusModifier\":{\"hp\":5,\"mp\":4,\"speed\":5,\"attack\":6,\"CD\":4,\"defense\":5}},{\"unlocked\":7,\"skillName\":\"achilles\",\"baseStatus\":{\"hp\":50,\"mp\":50,\"speed\":50,\"attack\":50,\"CD\":50,\"defense\":50},\"statusModifier\":{\"hp\":6,\"mp\":6,\"speed\":6,\"attack\":6,\"CD\":6,\"defense\":6}}]}";

    public static void loadState(bool force = false)
    {
        string filePath = Application.persistentDataPath + "/savedata.json";
        if (!File.Exists(filePath) || force)
        {
            File.WriteAllText(filePath, savedataInit);
        }

        string jsonStr = File.ReadAllText(filePath);
        StaticVariables.saveData = new JSONObject(jsonStr);
        //Debug.Log("Char0 HP = " + StaticVariables.saveData.GetField("characters").list[0].GetField("MaxHP").ToString());
    }


    public static void initialize(bool force = false)
    {
        loadState(force);
        string cardDataPath = Application.persistentDataPath + "/card.json";
        string carDataPath = Application.persistentDataPath + "/cars.json";
        string characterDataPath = Application.persistentDataPath + "/character.json";
        string itemDataPath = Application.persistentDataPath + "/item.json";

        if (!File.Exists(cardDataPath) || force)
        {
            File.WriteAllText(cardDataPath, cardInit);
        }
        string cardStr = File.ReadAllText(cardDataPath);
        StaticVariables.cardData = new JSONObject(cardStr);

        if (!File.Exists(carDataPath) || force)
        {
            File.WriteAllText(carDataPath, carsInit);
        }
        string carStr = File.ReadAllText(carDataPath);
        StaticVariables.carData = new JSONObject(carStr);

        if (!File.Exists(characterDataPath) || force)
        {
            File.WriteAllText(characterDataPath, characterInit);
        }
        string characterStr = File.ReadAllText(characterDataPath);
        StaticVariables.characterData = new JSONObject(characterStr);

        if (!StaticVariables.cardPackData || force)
        {
            StaticVariables.cardPackData = cardPackJSONObj();
        }
    }

    private static JSONObject cardPackJSONObj()
    {
        if (StaticVariables.cardData == null) {
            return null;
        }
        JSONObject ret = new JSONObject(JSONObject.Type.OBJECT);

        JSONObject anyC = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject anyB = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject anyA = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject anyS = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject atkC = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject atkB = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject atkA = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject atkS = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject defC = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject defB = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject defA = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject defS = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject speC = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject speB = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject speA = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject speS = new JSONObject(JSONObject.Type.ARRAY);

        foreach (JSONObject card in StaticVariables.cardData.GetField("ATK").list)
        {
            if (card.GetField("rank").str == "C") {
                anyC.Add(card);
                atkC.Add(card);
            }
            else if (card.GetField("rank").str == "B")
            {
                anyB.Add(card);
                atkB.Add(card);
            }
            else if (card.GetField("rank").str == "A")
            {
                anyA.Add(card);
                atkA.Add(card);
            }
            else if (card.GetField("rank").str == "S")
            {
                anyS.Add(card);
                atkS.Add(card);
            }
        }

        foreach (JSONObject card in StaticVariables.cardData.GetField("DEF").list)
        {
            if (card.GetField("rank").str == "C")
            {
                anyC.Add(card);
                defC.Add(card);
            }
            else if (card.GetField("rank").str == "B")
            {
                anyB.Add(card);
                defB.Add(card);
            }
            else if (card.GetField("rank").str == "A")
            {
                anyA.Add(card);
                defA.Add(card);
            }
            else if (card.GetField("rank").str == "S")
            {
                anyS.Add(card);
                defS.Add(card);
            }
        }

        foreach (JSONObject card in StaticVariables.cardData.GetField("SPE").list)
        {
            if (card.GetField("rank").str == "C")
            {
                anyC.Add(card);
                speC.Add(card);
            }
            else if (card.GetField("rank").str == "B")
            {
                anyB.Add(card);
                speB.Add(card);
            }
            else if (card.GetField("rank").str == "A")
            {
                anyA.Add(card);
                speA.Add(card);
            }
            else if (card.GetField("rank").str == "S")
            {
                anyS.Add(card);
                speS.Add(card);
            }
        }

        ret.AddField("anyC", anyC);
        ret.AddField("anyB", anyB);
        ret.AddField("anyA", anyA);
        ret.AddField("anyS", anyS);
        ret.AddField("atkC", atkC);
        ret.AddField("atkB", atkB);
        ret.AddField("atkA", atkA);
        ret.AddField("atkS", atkS);
        ret.AddField("defC", defC);
        ret.AddField("defB", defB);
        ret.AddField("defA", defA);
        ret.AddField("defS", defS);
        ret.AddField("speC", speC);
        ret.AddField("speB", speB);
        ret.AddField("speA", speA);
		ret.AddField("speS", speS);
		//Debug.Log (ret.ToString());
        return ret;
    }
}
