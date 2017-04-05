using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : MonoBehaviour {
    public float debugExp = -1;
    private float pointsToGain;
    public float speed = 100;
	// Use this for initialization
	void Start () {
        Load.initialize();
        if (debugExp != -1)
        {
            StaticVariables.expGained = debugExp;
        }
        pointsToGain = StaticVariables.expGained;
    }
	
	// Update is called once per frame
	void Update () {
        if (pointsToGain <= 0)
        {
            return;
        }
        ExpGrow();

    }

    private void ExpGrow()
    {
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
        float width = 200*(expGrow + currExp )/ maxExp;
        StaticVariables.saveData.GetField("characters").list[StaticVariables.characterID].SetField("exp", expGrow + currExp);
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, GetComponent<RectTransform>().sizeDelta.y);
        pointsToGain -= expGrow;
        if (expGrow + currExp == maxExp)
        {
            LevelUp();
        }
        //StaticVariables.characterID
    }

    private void LevelUp()
    {
        StaticVariables.saveData.GetField("characters").list[StaticVariables.characterID].SetField("exp", 0);
        float currLevel = StaticVariables.GetCharacterAttribute(StaticVariables.characterID).GetField("level").n;
        StaticVariables.saveData.GetField("characters").list[StaticVariables.characterID].SetField("level", currLevel+1);
    }
}
