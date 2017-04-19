using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load : MonoBehaviour {

    // hard code these init jsons
    private static string savedataInit = "{\"volume\":1,\"records\":[-1,-1,-1,-1,-1,-1,-1],\"characters\":[{\"level\":1,\"exp\":0},{\"level\":1,\"exp\":0},{\"level\":1,\"exp\":0},{\"level\":1,\"exp\":0},{\"level\":1,\"exp\":0},{\"level\":1,\"exp\":0}],\"cars\":[{\"level\":1,\"slots\":[-1,-1,-1]},{\"level\":1,\"slots\":[-1,-1,-1]},{\"level\":1,\"slots\":[-1,-1,-1]},{\"level\":1,\"slots\":[-1,-1,-1]},{\"level\":1,\"slots\":[-1,-1,-1]}],\"progress\":-1,\"totalCoins\":0,\"cards\":{\"ATK\":[{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0}],\"DEF\":[{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0}],\"SPE\":[{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0},{\"number\":0,\"maxNumber\":0}]}}";
	private static string carsInit = "{\"maxLevel\":6,\"cars\":[{\"name\":\"Learner\",\"unlocked\":-1,\"passiveName\":\"EXP * 2\",\"baseStatus\":{\"hp\":35,\"mp\":30,\"speed\":35,\"attack\":30,\"CD\":35,\"defense\":35},\"statusModifier\":{\"hp\":4,\"mp\":4,\"speed\":4,\"attack\":4,\"CD\":4,\"defense\":4},\"price\":[300,600,900,1200,1500],\"slotAttributes\":[\"ATK\",\"SPE\",\"DEF\"]},{\"name\":\"Earner\",\"unlocked\":1,\"passiveName\":\"coin * 2\",\"baseStatus\":{\"hp\":35,\"mp\":50,\"speed\":40,\"attack\":35,\"CD\":40,\"defense\":40},\"statusModifier\":{\"hp\":4,\"mp\":6,\"speed\":5,\"attack\":4,\"CD\":5,\"defense\":5},\"price\":[400,800,1200,1600,2000],\"slotAttributes\":[\"SPE\",\"DEF\",\"SPE\"]},{\"name\":\"Producer\",\"unlocked\":2,\"passiveName\":\"CDR * 1.5\",\"baseStatus\":{\"hp\":40,\"mp\":35,\"speed\":50,\"attack\":40,\"CD\":40,\"defense\":40},\"statusModifier\":{\"hp\":5,\"mp\":4,\"speed\":6,\"attack\":5,\"CD\":5,\"defense\":5},\"price\":[500,1000,1500,2000,2500],\"slotAttributes\":[\"SPE\",\"ATK\",\"SPE\"]},{\"name\":\"Healer\",\"unlocked\":3,\"passiveName\":\"HP++ per second\",\"baseStatus\":{\"hp\":45,\"mp\":40,\"speed\":40,\"attack\":40,\"CD\":35,\"defense\":50},\"statusModifier\":{\"hp\":5,\"mp\":5,\"speed\":5,\"attack\":5,\"CD\":4,\"defense\":6},\"price\":[600,1200,1800,2400,3000],\"slotAttributes\":[\"DEF\",\"SPE\",\"DEF\"]},{\"name\":\"Raider\",\"unlocked\":4,\"passiveName\":\"Attack * 1.2\",\"baseStatus\":{\"hp\":45,\"mp\":40,\"speed\":45,\"attack\":50,\"CD\":40,\"defense\":45},\"statusModifier\":{\"hp\":5,\"mp\":5,\"speed\":5,\"attack\":6,\"CD\":5,\"defense\":5},\"price\":[700,1400,2100,2800,3500],\"slotAttributes\":[\"ATK\",\"SPE\",\"ATK\"]}]}";
    private static string cardInit = "{\"price\":{\"C\":100,\"B\":500,\"A\":1000,\"S\":3000},\"ATK\":[{\"name\":\"atkIncrease10\",\"shortName\":\"Wooden Dagger\",\"description\":\"Increase ATK by 10%\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"atkIncrease20\",\"shortName\":\"Silver Dagger\",\"description\":\"Increase ATK by 20%\",\"rank\":\"B\",\"attributes\":[0.2]},{\"name\":\"atkIncrease30\",\"shortName\":\"Golden Dagger\",\"description\":\"Increase ATK by 30%\",\"rank\":\"A\",\"attributes\":[0.3]},{\"name\":\"atkIncrease50\",\"shortName\":\"Crystal Dagger\",\"description\":\"Increase ATK by 50%\",\"rank\":\"S\",\"attributes\":[0.5]},{\"name\":\"lifeSteal25\",\"shortName\":\"Blood Thurst Lv1\",\"description\":\"HP will recover by 25% of the amount of damage dealt to opponent\",\"rank\":\"C\",\"attributes\":[0.25]},{\"name\":\"lifeSteal40\",\"shortName\":\"Blood Thurst Lv2\",\"description\":\"HP will recover by 40% of the amount of damage dealt to opponent\",\"rank\":\"B\",\"attributes\":[0.4]},{\"name\":\"lifeSteal55\",\"shortName\":\"Blood Thurst Lv3\",\"description\":\"HP will recover by 55% of the amount of damage dealt to opponent\",\"rank\":\"A\",\"attributes\":[0.55]},{\"name\":\"lifeSteal80\",\"shortName\":\"Unquenchable Blood Thurst\",\"description\":\"HP will recover by 80% of the amount of damage dealt to opponent\",\"rank\":\"S\",\"attributes\":[0.8]},{\"name\":\"poison2For5\",\"shortName\":\"Poisoned Arrow Lv1\",\"description\":\"Once hit target with an item, the target will get poisoned and lose 2% health per second for the next 5 seconds\",\"rank\":\"C\",\"attributes\":[0.02,5]},{\"name\":\"poison3For5\",\"shortName\":\"Poisoned Arrow Lv2\",\"description\":\"Once hit target with an item, the target will get poisoned and lose 3% health per second for the next 5 seconds\",\"rank\":\"B\",\"attributes\":[0.03,5]},{\"name\":\"poison4For5\",\"shortName\":\"Poisoned Arrow Lv3\",\"description\":\"Once hit target with an item, the target will get poisoned and lose 4% health per second for the next 5 seconds\",\"rank\":\"A\",\"attributes\":[0.04,5]},{\"name\":\"poison6For5\",\"shortName\":\"Black Mamba's Fang\",\"description\":\"Once hit target with an item, the target will get poisoned and lose 6% health per second for the next 5 seconds\",\"rank\":\"S\",\"attributes\":[0.06,5]},{\"name\":\"stun1\",\"shortName\":\"Medusa's Eye\",\"description\":\"Once hit the target with an item, stun the target for 1 second\",\"rank\":\"S\",\"attributes\":[1]},{\"name\":\"speedReuction20\",\"shortName\":\"Freezing Breeze Lv1\",\"description\":\"Once hit the target with an item, reduce it's speed by 20% for 2 seconds\",\"rank\":\"C\",\"attributes\":[0.2,2]},{\"name\":\"speedReuction30\",\"shortName\":\"Freezing Breeze Lv2\",\"description\":\"Once hit the target with an item, reduce it's speed by 30% for 2 seconds\",\"rank\":\"B\",\"attributes\":[0.3,2]},{\"name\":\"speedReuction40\",\"shortName\":\"Freezing Breeze Lv3\",\"description\":\"Once hit the target with an item, reduce it's speed by 40% for 2 seconds\",\"rank\":\"A\",\"attributes\":[0.4,2]},{\"name\":\"speedReuction60\",\"shortName\":\"Snow Storm\",\"description\":\"Once hit the target with an item, reduce it's speed by 60% for 2 seconds\",\"rank\":\"S\",\"attributes\":[0.6,2]},{\"name\":\"crit20Dam1.5\",\"shortName\":\"Weakness Exposure Lv1\",\"description\":\"20% chance turning a normal attack into critical strike with 1.5 times attack damage\",\"rank\":\"C\",\"attributes\":[0.2,1.5]},{\"name\":\"crit30Dam1.5\",\"shortName\":\"Weakness Exposure Lv2\",\"description\":\"30% chance turning a normal attack into critical strike with 1.5 times attack damage\",\"rank\":\"B\",\"attributes\":[0.3,1.5]},{\"name\":\"crit30Dam2\",\"shortName\":\"Weakness Exposure Lv3\",\"description\":\"30% chance turning a normal attack into critical strike with 2 times attack damage\",\"rank\":\"A\",\"attributes\":[0.3,2]},{\"name\":\"crit50Dam2\",\"shortName\":\"Achilles' Heel\",\"description\":\"50% chance turning a normal attack into critical strike with 2 times attack damage\",\"rank\":\"S\",\"attributes\":[0.5,2]},{\"name\":\"trade20Health25Atk\",\"shortName\":\"Wounded Berserker Lv1\",\"description\":\"Trade 20% max HP for 25% additional attack damage\",\"rank\":\"C\",\"attributes\":[0.2,0.25]},{\"name\":\"trade30Health50Atk\",\"shortName\":\"Wounded Berserker Lv2\",\"description\":\"Trade 30% max HP for 50% additional attack damage\",\"rank\":\"B\",\"attributes\":[0.3,0.5]},{\"name\":\"trade30Health60Atk\",\"shortName\":\"Wounded Berserker Lv3\",\"description\":\"Trade 30% max HP for 60% additional attack damage\",\"rank\":\"A\",\"attributes\":[0.3,0.6]},{\"name\":\"trade50Health100Atk\",\"shortName\":\"Kamikaze's Glory\",\"description\":\"Trade 50% max HP for 100% additional attack damage\",\"rank\":\"S\",\"attributes\":[0.5,1]},{\"name\":\"increaseAccu15\",\"shortName\":\"Hawk-Eye Lv1\",\"description\":\"Increase Accuracy by 15%\",\"rank\":\"C\",\"attributes\":[0.15]},{\"name\":\"increaseAccu20\",\"shortName\":\"Hawk-Eye Lv2\",\"description\":\"Increase Accuracy by 20%\",\"rank\":\"B\",\"attributes\":[0.2]},{\"name\":\"increaseAccu25\",\"shortName\":\"Hawk-Eye Lv3\",\"description\":\"Increase Accuracy by 25%\",\"rank\":\"A\",\"attributes\":[0.25]},{\"name\":\"increaseAccu40\",\"shortName\":\"Ultimate Hawk-Eye\",\"description\":\"Increase Accuracy by 40%\",\"rank\":\"S\",\"attributes\":[0.4]},{\"name\":\"atkPen20\",\"shortName\":\"Shield Shedder Lv1\",\"description\":\"Increase armor penetration by 20\",\"rank\":\"C\",\"attributes\":[20]},{\"name\":\"atkPen40\",\"shortName\":\"Shield Shedder Lv2\",\"description\":\"Increase armor penetration by 40\",\"rank\":\"B\",\"attributes\":[40]},{\"name\":\"atkPen60\",\"shortName\":\"Shield Shedder Lv3\",\"description\":\"Increase armor penetration by 60\",\"rank\":\"A\",\"attributes\":[60]},{\"name\":\"atkPen100\",\"shortName\":\"Unstoppable Will\",\"description\":\"Increase armor penetration by 100\",\"rank\":\"S\",\"attributes\":[100]}],\"DEF\":[{\"name\":\"increaseHP10\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"increaseHP30\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.3]},{\"name\":\"increaseHP50\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[0.5]},{\"name\":\"increaseHP80\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[0.8]},{\"name\":\"hpRecover3\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.03]},{\"name\":\"hpRecover5\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.05]},{\"name\":\"hpRecover7\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[0.07]},{\"name\":\"hpRecover10\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[0.1]},{\"name\":\"damRedction10\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"damRedction15\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.15]},{\"name\":\"damRedction20\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[0.2]},{\"name\":\"damRedction40\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[0.4]},{\"name\":\"reflect50\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[0.5]},{\"name\":\"revive1\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[1]},{\"name\":\"startWithShield1\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[1]},{\"name\":\"startWithShield2\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[2]},{\"name\":\"startWithShield3\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[3]},{\"name\":\"startWithShield5\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[5]},{\"name\":\"defIncrease10\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"defIncrease30\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.3]},{\"name\":\"defIncrease50\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[0.5]},{\"name\":\"defIncrease100\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[1]},{\"name\":\"evationIncrease10\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"evationIncrease20\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.2]},{\"name\":\"evationIncrease30\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[0.3]},{\"name\":\"evationIncrease50\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[0.5]}],\"SPE\":[{\"name\":\"mpIncrease10\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"mpIncrease30\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.3]},{\"name\":\"mpIncrease50\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[0.5]},{\"name\":\"mpIncrease100\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[1]},{\"name\":\"mpRecoverIncrease3\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.03]},{\"name\":\"mpRecoverIncrease5\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.05]},{\"name\":\"mpRecoverIncrease7\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[0.07]},{\"name\":\"mpRecoverIncrease10\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[0.1]},{\"name\":\"mpCostReduction10\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"mpCostReduction20\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.2]},{\"name\":\"mpCostReduction30\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[0.3]},{\"name\":\"mpCostReduction50\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[0.5]},{\"name\":\"speedIncrease10\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.1]},{\"name\":\"speedIncrease15\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.15]},{\"name\":\"speedIncrease20\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[0.2]},{\"name\":\"speedIncrease30\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[0.3]},{\"name\":\"coinIncrease60\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.6]},{\"name\":\"coinIncrease80\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.8]},{\"name\":\"coinIncrease100\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[1]},{\"name\":\"coinIncrease150\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[1.5]},{\"name\":\"expIncrease60\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.6]},{\"name\":\"expIncrease80\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.8]},{\"name\":\"expIncrease100\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[1]},{\"name\":\"expIncrease150\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[1.5]},{\"name\":\"tradeHealth5Speed5\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"C\",\"attributes\":[0.05,0.05]},{\"name\":\"tradeHealth10Speed10\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"B\",\"attributes\":[0.1,0.1]},{\"name\":\"tradeHealth15Speed15\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"A\",\"attributes\":[0.15,0.15]},{\"name\":\"tradeHealth25Speed25\",\"shortName\":\"\",\"description\":\"\",\"rank\":\"S\",\"attributes\":[0.25,0.25]}],\"cardProbability\":{\"Normal\":{\"name\":\"Normal Pack\",\"description\":\"\",\"cost\":100,\"probability\":{\"anyC\":0.5,\"anyB\":0.4,\"anyA\":0.09,\"anyS\":0.01}},\"Gold\":{\"name\":\"Golden Pack\",\"description\":\"\",\"cost\":500,\"probability\":{\"anyC\":0.25,\"anyB\":0.35,\"anyA\":0.3,\"anyS\":0.1}},\"ATK\":{\"name\":\"Attack Pack\",\"description\":\"\",\"cost\":250,\"probability\":{\"atkC\":0.5,\"atkB\":0.4,\"atkA\":0.09,\"atkS\":0.01}},\"DEF\":{\"name\":\"Deffense Pack\",\"description\":\"\",\"cost\":250,\"probability\":{\"defC\":0.5,\"defB\":0.4,\"defA\":0.09,\"defS\":0.01}},\"SPE\":{\"name\":\"Special Pack\",\"description\":\"\",\"cost\":250,\"probability\":{\"speC\":0.5,\"speB\":0.4,\"speA\":0.09,\"speS\":0.01}}}}";
    private static string characterInit = "{\"maxLevel\":15,\"baseExp\":100,\"expModifier\":1.4,\"characters\":[{\"unlocked\":-1,\"mpComsumption\":25,\"skillName\":\"dragonBreath\",\"baseStatus\":{\"hp\":35,\"mp\":50,\"speed\":40,\"attack\":40,\"CD\":45,\"defense\":35},\"statusModifier\":{\"hp\":4,\"mp\":6,\"speed\":5,\"attack\":5,\"CD\":5,\"defense\":4}},{\"unlocked\":1,\"mpComsumption\":20,\"skillName\":\"goldAttack\",\"baseStatus\":{\"hp\":30,\"mp\":40,\"speed\":50,\"attack\":35,\"CD\":45,\"defense\":40},\"statusModifier\":{\"hp\":4,\"mp\":5,\"speed\":6,\"attack\":4,\"CD\":5,\"defense\":5}},{\"unlocked\":2,\"skillName\":\"shield\",\"mpComsumption\":30,\"baseStatus\":{\"hp\":50,\"mp\":45,\"speed\":40,\"attack\":35,\"CD\":30,\"defense\":45},\"statusModifier\":{\"hp\":6,\"mp\":5,\"speed\":5,\"attack\":4,\"CD\":4,\"defense\":5}},{\"unlocked\":3,\"skillName\":\"freeze\",\"mpComsumption\":35,\"baseStatus\":{\"hp\":45,\"mp\":40,\"speed\":35,\"attack\":45,\"CD\":35,\"defense\":50},\"statusModifier\":{\"hp\":5,\"mp\":5,\"speed\":4,\"attack\":5,\"CD\":4,\"defense\":6}},{\"unlocked\":5,\"skillName\":\"nitro\",\"mpComsumption\":20,\"baseStatus\":{\"hp\":40,\"mp\":30,\"speed\":40,\"attack\":50,\"CD\":35,\"defense\":40},\"statusModifier\":{\"hp\":5,\"mp\":4,\"speed\":5,\"attack\":6,\"CD\":4,\"defense\":5}},{\"unlocked\":7,\"skillName\":\"achilles\",\"mpComsumption\":20,\"baseStatus\":{\"hp\":50,\"mp\":50,\"speed\":50,\"attack\":50,\"CD\":50,\"defense\":50},\"statusModifier\":{\"hp\":6,\"mp\":6,\"speed\":6,\"attack\":6,\"CD\":6,\"defense\":6}}]}";

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
        AudioListener.volume = StaticVariables.saveData["volume"].n;
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
        if (StaticVariables.cardData == null)
        {
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

        JSONObject ATKCards = StaticVariables.cardData.GetField("ATK");
        for (int i = 0; i < ATKCards.list.Count; i++)
        {
            JSONObject card = new JSONObject();
            card.SetField("attribute", "ATK");
            card.SetField("id", i);
            if (ATKCards.list[i].GetField("rank").str == "C")
            {
                anyC.Add(card);
                atkC.Add(card);
            }
            else if (ATKCards.list[i].GetField("rank").str == "B")
            {
                anyB.Add(card);
                atkB.Add(card);
            }
            else if (ATKCards.list[i].GetField("rank").str == "A")
            {
                anyA.Add(card);
                atkA.Add(card);
            }
            else if (ATKCards.list[i].GetField("rank").str == "S")
            {
                anyS.Add(card);
                atkS.Add(card);
            }

        }
        JSONObject DEFCards = StaticVariables.cardData.GetField("DEF");
        for (int i = 0; i < DEFCards.list.Count; i++)
        {
            JSONObject card = new JSONObject();
            card.SetField("attribute", "DEF");
            card.SetField("id", i);
            if (DEFCards.list[i].GetField("rank").str == "C")
            {
                anyC.Add(card);
                defC.Add(card);
            }
            else if (DEFCards.list[i].GetField("rank").str == "B")
            {
                anyB.Add(card);
                defB.Add(card);
            }
            else if (DEFCards.list[i].GetField("rank").str == "A")
            {
                anyA.Add(card);
                defA.Add(card);
            }
            else if (DEFCards.list[i].GetField("rank").str == "S")
            {
                anyS.Add(card);
                defS.Add(card);
            }
        }
        JSONObject SPECards = StaticVariables.cardData.GetField("SPE");
        for (int i = 0; i < SPECards.list.Count; i++)
        {
            JSONObject card = new JSONObject();
            card.SetField("attribute", "SPE");
            card.SetField("id", i);
            if (SPECards.list[i].GetField("rank").str == "C")
            {
                anyC.Add(card);
                speC.Add(card);
            }
            else if (SPECards.list[i].GetField("rank").str == "B")
            {
                anyB.Add(card);
                speB.Add(card);
            }
            else if (SPECards.list[i].GetField("rank").str == "A")
            {
                anyA.Add(card);
                speA.Add(card);
            }
            else if (SPECards.list[i].GetField("rank").str == "S")
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
