using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeBar : MonoBehaviour {

	private float fillAmount; 

	[SerializeField]
	private Image content;

	[SerializeField]
	private Text valueText;

	// Bar Color Settings:
	private float lerpSpeed = 2;
	private bool lerpColors = true;
	private Color fullColor = new Color32(255, 219, 0, 255);
	private Color lowColor = new Color(255, 0, 0, 255);

	// property
	public float MaxValue { get; set;}

	public float Value {
		set {
			string[] temp = valueText.text.Split (':');
			valueText.text = temp [0] + ": " + value;
			fillAmount = ValueTranslate (value, MaxValue);
		}
	}

	// Use this for initialization
	void Start () {

		if (lerpColors) {
			content.color = fullColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();
	}

	public void HandleBar(){
		if (fillAmount != content.fillAmount) {
			{
				content.fillAmount = Mathf.Lerp (content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
			}
		}
		if (lerpColors) {
			content.color = Color.Lerp (lowColor, fullColor, fillAmount);
		}
	}

	// Mapping current value to scale 0-1;
	private float ValueTranslate(float currValue, float maxValue) {
		return currValue/maxValue;
	}
}
