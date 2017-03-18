using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLTest_debug : MonoBehaviour {
    public bool debugSwitch = false;
	// Use this for initialization
	void Awake () {
        Load.loadState();
	}
	
	// Update is called once per frame
	void Update () {
        if (debugSwitch && StaticVariables.saveData!=null)
        {
            Debug.Log("old data = " + StaticVariables.saveData.ToString());
            foreach(JSONObject jsonObj in StaticVariables.saveData.GetField("characters").list)
            {
                jsonObj.SetField("MaxHP", jsonObj.GetField("MaxHP").n + 1);
                jsonObj.SetField("MaxMP", jsonObj.GetField("MaxMP").n + 2);
                jsonObj.SetField("SkillCD", jsonObj.GetField("SkillCD").n + 3);
                jsonObj.SetField("AttackPower", jsonObj.GetField("AttackPower").n + 4);
            }
            Save.saveState();
            Debug.Log("new data saved!!");
            debugSwitch = false;
        }
	}
}
