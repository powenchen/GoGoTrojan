using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeValueOnClick : MonoBehaviour {

	// MAX HP
	static public bool addHealthValue = false;
	static public bool subHealthValue = false;
	public void AddHealthValue() { addHealthValue = true; }
	public void SubHealthValue() { subHealthValue = true; }

	// MAX MP
	static public bool addManaValue = false;
	static public bool subManaValue = false;
	public void AddManaValue() { addManaValue = true; }
	public void SubManaValue() { subManaValue = true; }

	// MAX Speed
	static public bool addSpeedValue = false;
	static public bool subSpeedValue = false;
	public void AddSpeedValue() { addSpeedValue = true; }
	public void SubSpeedValue() { subSpeedValue = true; }

	// Skill CD Time
	static public bool addSkillValue = false;
	static public bool subSkillValue = false;
	public void AddSkillValue() { addSkillValue = true; }
	public void SubSkillValue() { subSkillValue = true; }

	// Power
	static public bool addPowerValue = false;
	static public bool subPowerValue = false;
	public void AddPowerValue() { addPowerValue = true; }
	public void SubPowerValue() { subPowerValue = true; }

	// Discard/Reset

	static public bool resetAllValues = false;
	public void resetValue() { resetAllValues = true; }
}
