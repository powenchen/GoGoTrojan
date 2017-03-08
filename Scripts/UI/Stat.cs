using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Stat {

	[SerializeField]
	private AttributeBar bar;
	[SerializeField]
	private float maxVal; 
	[SerializeField]
	public float currentVal; 

	// Property of Stat
	public float CurrentVal {
		get {
			return currentVal;
		}
		set {
			this.currentVal = Mathf.Clamp(value, 0, MaxValue);
			bar.Value = currentVal;
		}
	}

	// Property of Stat
	public float MaxValue {
		get {
			return maxVal;
		}
		set {
			this.maxVal = value;
			bar.MaxValue = maxVal;
		}
	}


	public void Initialize() {

		this.MaxValue = maxVal;
		this.CurrentVal = currentVal;

	}

}