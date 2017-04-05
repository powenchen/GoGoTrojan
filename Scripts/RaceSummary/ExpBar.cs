using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour {
    public float debugExp = -1;
    private float pointsToGain;
    public float speed = 100;
	public float maxWidth = 500;
	// Use this for initialization

	public Text description;
	void Start () {
        Load.initialize();
        if (debugExp != -1)
        {
            StaticVariables.expGained = debugExp;
        }
        pointsToGain = StaticVariables.expGained;

		description.text = "Level = " + StaticVariables.GetCharacterAttribute(StaticVariables.characterID).GetField("level").n;
		description.text += "\nHP = " + StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("hp").n;
		description.text += "\nMP = " + StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("mp").n;
		description.text += "\nAttack = " + StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("attack").n;
		description.text += "\nDefense = " + StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("defense").n;
		description.text += "\nSpeed = " + StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("speed").n;
		description.text += "\nMP Regen. = " + StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("CD").n;


    }
	
	// Update is called once per frame
	void Update () {
        if (pointsToGain <= 0)
        {
            return;
        }
        ExpGrow();

		float currExp = StaticVariables.GetCharacterAttribute(StaticVariables.characterID).GetField("currentExp").n;
		Debug.Log("currentExp = " + currExp);
    }

    private void ExpGrow()
    {

		Debug.Log("path = " + Application.persistentDataPath + "/savedata.json");
        float maxExp = StaticVariables.GetCharacterAttribute(StaticVariables.characterID).GetField("maxExpForThisLevel").n;
        Debug.Log("maxExp = " + maxExp);
        float currExp = StaticVariables.GetCharacterAttribute(StaticVariables.characterID).GetField("currentExp").n;
        Debug.Log("currentExp = " + currExp);
        float level = StaticVariables.GetCharacterAttribute(StaticVariables.characterID).GetField("level").n;
        Debug.Log("level = " + level);

        float expGrowInUpdate =
            Mathf.Min(Time.deltaTime * speed, pointsToGain);
        float expGrow = Mathf.Min(expGrowInUpdate, maxExp - currExp);
        Debug.Log("expGrow = " + expGrow);
		float width = maxWidth*(expGrow + currExp )/ maxExp;
        StaticVariables.saveData.GetField("characters").list[StaticVariables.characterID].SetField("exp", expGrow + currExp);
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, GetComponent<RectTransform>().sizeDelta.y);
        pointsToGain -= expGrow;
        if (expGrow + currExp == maxExp)
        {
			LevelUp();
			float previousLv = StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("level").n;
			if (previousLv >= StaticVariables.characterData.GetField ("maxLevel").n) 
			{
				StaticVariables.saveData.GetField ("characters").list [StaticVariables.characterID].SetField ("exp", 0);
				pointsToGain = 0;
			}
        }
        //StaticVariables.characterID
    }

    private void LevelUp()
	{
		
		float previousLv = StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("level").n;
		if (previousLv >= StaticVariables.characterData.GetField ("maxLevel").n) {
			pointsToGain = 0;
			return;
		}
		float previousHp = StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("hp").n;
		float previousMp = StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("mp").n;
		float previousAttack = StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("attack").n;
		float previousDefense = StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("defense").n;
		float previousSpeed = StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("speed").n;
		float previousCD = StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("CD").n;

        StaticVariables.saveData.GetField("characters").list[StaticVariables.characterID].SetField("exp", 0);
        float currLevel = StaticVariables.GetCharacterAttribute(StaticVariables.characterID).GetField("level").n;
        StaticVariables.saveData.GetField("characters").list[StaticVariables.characterID].SetField("level", currLevel+1);

		description.text = "Level = " +previousLv+"  →  "+ StaticVariables.GetCharacterAttribute(StaticVariables.characterID).GetField("level").n;
		description.text += "\nHP = " +previousHp+"  →  "+ StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("hp").n;
		description.text += "\nMP = " +previousMp+"  →  "+ StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("mp").n;
		description.text += "\nAttack = " +previousAttack+"  →  "+ StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("attack").n;
		description.text += "\nDefense = " +previousDefense+"  →  "+ StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("defense").n;
		description.text += "\nSpeed = " +previousSpeed+"  →  "+ StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("speed").n;
		description.text += "\nMP Regen. = " +previousCD+"  →  "+ StaticVariables.GetCharacterAttribute (StaticVariables.characterID).GetField ("CD").n;

    }
}
