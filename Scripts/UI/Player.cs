using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Text coinText;
	private int initialCoins = 1000;
	private int initialHealthCount = 10;
	private int initialManaCount = 15;
	private int initialSpeedCount = 20;
	private int initialSkillCount = 35;
	private int initialPowerCount = 25;


	// (1) MaxHP
	public Stat health;
	public Text healthText;
	private float defaultHealth;
	private float healthVal = 10;
	private int healthCost = 150;
	public Button addHealthBtn;
	public Button subHealthBtn;
	public float initialHealth;

	// (2) MaxMP
	public Stat mana;
	public Text manaText;
	private float manaVal = 20;
	private int manaCost = 200;
	public Button addManaBtn;
	public Button subManaBtn;
	public float initialMana;

	// MaxSpeed
	public Stat speed;
	public Text speedText;
	private float speedVal = 50;
	private int speedCost = 250;
	public Button addSpeedBtn;
	public Button subSpeedBtn;
	public float initialSpeed;

	// Skill CD Time
	public Stat skill;
	public Text skillText;
	private float skillVal = 15;
	private int skillCost = 50;
	public Button addSkillBtn;
	public Button subSkillBtn;
	public float initialSkill;

	// Power
	public Stat power;
	public Text powerText;
	private float powerVal = 25;
	private int powerCost = 60;
	public Button addPowerBtn;
	public Button subPowerBtn;
	public float initialPower;

	// Initialize / Get the current value of each stat
	private void Awake(){
		
		PlayerPrefs.SetInt("TotalCoins", initialCoins);
		this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");

		PlayerPrefs.SetInt("HealthCount", initialHealthCount);
		this.HealthCount = PlayerPrefs.GetInt("HealthCount");
		health.Initialize ();
		initialHealth = this.health.CurrentVal;
		addHealthBtn.interactable = true;
		subHealthBtn.interactable = true;

		PlayerPrefs.SetInt("ManaCount", initialManaCount);
		this.ManaCount = PlayerPrefs.GetInt("ManaCount");
		mana.Initialize ();
		initialMana = this.mana.CurrentVal;
		addManaBtn.interactable = true;
		subManaBtn.interactable = true;

		PlayerPrefs.SetInt("SpeedCount", initialSpeedCount);
		this.SpeedCount = PlayerPrefs.GetInt("SpeedCount");
		speed.Initialize ();
		initialSpeed = this.speed.CurrentVal;
		addSpeedBtn.interactable = true;
		subSpeedBtn.interactable = true;

		PlayerPrefs.SetInt("SkillCount", initialSkillCount);
		this.SkillCount = PlayerPrefs.GetInt("SkillCount");
		skill.Initialize ();
		initialSkill = this.skill.CurrentVal;
		addSkillBtn.interactable = true;
		subSkillBtn.interactable = true;

		PlayerPrefs.SetInt("PowerCount", initialPowerCount);
		this.PowerCount = PlayerPrefs.GetInt("PowerCount");
		power.Initialize ();
		initialPower = this.power.CurrentVal;
		addPowerBtn.interactable = true;
		subPowerBtn.interactable = true;

	}

	// Update Bar + Coin + Count values
	void Update () {

		// MAX HP
		if (ChangeValueOnClick.addHealthValue) {
			health.CurrentVal += healthVal;
			PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - healthCost);
			PlayerPrefs.SetInt("HealthCount", PlayerPrefs.GetInt("HealthCount") - 1);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			this.HealthCount = PlayerPrefs.GetInt("HealthCount");
			ChangeValueOnClick.addHealthValue = false;
		}
		if (ChangeValueOnClick.subHealthValue) {
			health.CurrentVal -= healthVal;
			PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + healthCost);
			PlayerPrefs.SetInt("HealthCount", PlayerPrefs.GetInt("HealthCount") + 1);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			this.HealthCount = PlayerPrefs.GetInt("HealthCount");
			ChangeValueOnClick.subHealthValue = false;
		}

		// MAX MP
		if (ChangeValueOnClick.addManaValue) {
			mana.CurrentVal += manaVal;
			PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - manaCost);
			PlayerPrefs.SetInt("ManaCount", PlayerPrefs.GetInt("ManaCount") - 1);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			this.ManaCount = PlayerPrefs.GetInt("ManaCount");
			ChangeValueOnClick.addManaValue = false;
		}
		if (ChangeValueOnClick.subManaValue) {
			mana.CurrentVal -= manaVal;
			PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + manaCost);
			PlayerPrefs.SetInt("ManaCount", PlayerPrefs.GetInt("ManaCount") + 1);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			this.ManaCount = PlayerPrefs.GetInt("ManaCount");
			ChangeValueOnClick.subManaValue = false;
		}

		// MAX Speed
		if (ChangeValueOnClick.addSpeedValue) {
			speed.CurrentVal += speedVal;
			PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - speedCost);
			PlayerPrefs.SetInt("SpeedCount", PlayerPrefs.GetInt("SpeedCount") - 1);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			this.SpeedCount = PlayerPrefs.GetInt("SpeedCount");
			ChangeValueOnClick.addSpeedValue = false;
		}
		if (ChangeValueOnClick.subSpeedValue) {
			speed.CurrentVal -= speedVal;
			PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + speedCost);
			PlayerPrefs.SetInt("SpeedCount", PlayerPrefs.GetInt("SpeedCount") + 1);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			this.SpeedCount = PlayerPrefs.GetInt("SpeedCount");
			ChangeValueOnClick.subSpeedValue = false;
		}

		// Skill CD Time
		if (ChangeValueOnClick.addSkillValue) {
			skill.CurrentVal += skillVal;
			PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - skillCost);
			PlayerPrefs.SetInt("SkillCount", PlayerPrefs.GetInt("SkillCount") - 1);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			this.SkillCount = PlayerPrefs.GetInt("SkillCount");
			ChangeValueOnClick.addSkillValue = false;
		}
		if (ChangeValueOnClick.subSkillValue) {
			skill.CurrentVal -= skillVal;
			PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + speedCost);
			PlayerPrefs.SetInt("SkillCount", PlayerPrefs.GetInt("SkillCount") + 1);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			this.SkillCount = PlayerPrefs.GetInt("SkillCount");
			ChangeValueOnClick.subSkillValue = false;
		}

		// Skill CD Time
		if (ChangeValueOnClick.addPowerValue) {
			power.CurrentVal += powerVal;
			PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - powerCost);
			PlayerPrefs.SetInt("PowerCount", PlayerPrefs.GetInt("PowerCount") - 1);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			this.PowerCount = PlayerPrefs.GetInt("PowerCount");
			ChangeValueOnClick.addPowerValue = false;
		}
		if (ChangeValueOnClick.subPowerValue) {
			power.CurrentVal -= powerVal;
			PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + powerCost);
			PlayerPrefs.SetInt("PowerCount", PlayerPrefs.GetInt("PowerCount") + 1);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			this.PowerCount = PlayerPrefs.GetInt("PowerCount");
			ChangeValueOnClick.subPowerValue = false;
		}

		// ResetButton
		if (ChangeValueOnClick.resetAllValues) {
			PlayerPrefs.SetInt("TotalCoins", initialCoins);
			this.TotalCoins = PlayerPrefs.GetInt("TotalCoins");
			PlayerPrefs.SetInt("HealthCount", initialHealthCount);
			this.HealthCount = PlayerPrefs.GetInt("HealthCount");
			PlayerPrefs.SetInt("ManaCount", initialManaCount);
			this.ManaCount = PlayerPrefs.GetInt("ManaCount");
			PlayerPrefs.SetInt("SpeedCount", initialSpeedCount);
			this.SpeedCount = PlayerPrefs.GetInt("SpeedCount");
			PlayerPrefs.SetInt("SkillCount", initialSkillCount);
			this.SkillCount = PlayerPrefs.GetInt("SkillCount");
			PlayerPrefs.SetInt("PowerCount", initialPowerCount);
			this.PowerCount = PlayerPrefs.GetInt("PowerCount");
			health.CurrentVal = initialHealth;
			mana.CurrentVal = initialMana;
			speed.CurrentVal = initialSpeed;
			skill.CurrentVal = initialSkill;
			power.CurrentVal = initialPower;
			ChangeValueOnClick.resetAllValues = false;
		}

		// AddButtons
		if (PlayerPrefs.GetInt ("HealthCount") <= 0 || PlayerPrefs.GetInt ("TotalCoins") - healthCost < 0) {
			addHealthBtn.interactable = false;
		} else { addHealthBtn.interactable = true; }
		if (PlayerPrefs.GetInt("ManaCount") <= 0 || PlayerPrefs.GetInt("TotalCoins") - manaCost < 0) {
			addManaBtn.interactable = false;
		} else { addManaBtn.interactable = true; }
		if (PlayerPrefs.GetInt("SpeedCount") <= 0 || PlayerPrefs.GetInt("TotalCoins") - speedCost < 0) {
			addSpeedBtn.interactable = false;
		} else { addSpeedBtn.interactable = true; }
		if (PlayerPrefs.GetInt("SkillCount") <= 0 || PlayerPrefs.GetInt("TotalCoins") - skillCost < 0) {
			addSkillBtn.interactable = false;
		} else { addSkillBtn.interactable = true; }
		if (PlayerPrefs.GetInt("PowerCount") <= 0 || PlayerPrefs.GetInt("TotalCoins") - powerCost < 0) {
			addPowerBtn.interactable = false;
		} else { addPowerBtn.interactable = true; }

		// SubButtons
		if ((health.currentVal - healthVal) < initialHealth) {
			subHealthBtn.interactable = false;
		} else {
			subHealthBtn.interactable = true;
		}
		if ((mana.currentVal - manaVal) < initialMana) {
			subManaBtn.interactable = false;
		} else {
			subManaBtn.interactable = true;
		}
		if ((speed.currentVal - speedVal) < initialSpeed) {
			subSpeedBtn.interactable = false;
		} else {
			subSpeedBtn.interactable = true;
		}
		if ((skill.currentVal - skillVal) < initialSkill) {
			subSkillBtn.interactable = false;
		} else {
			subSkillBtn.interactable = true;
		}
		if ((power.currentVal - powerVal) < initialPower) {
			subPowerBtn.interactable = false;
		} else {
			subPowerBtn.interactable = true;
		}

	}
		
	public float TotalCoins {
		set {
			string[] temp = coinText.text.Split (':');
			coinText.text = temp [0] + ": " + PlayerPrefs.GetInt("TotalCoins");
		}
	}

	public float HealthCount {
		set {
			string[] temp = healthText.text.Split ('/');
			healthText.text = PlayerPrefs.GetInt("HealthCount") + "/" + temp [1];
		}
	}

	public float ManaCount {
		set {
			string[] temp = manaText.text.Split ('/');
			manaText.text = PlayerPrefs.GetInt("ManaCount") + "/" + temp [1];
		}
	}

	public float SpeedCount {
		set {
			string[] temp = speedText.text.Split ('/');
			speedText.text = PlayerPrefs.GetInt("SpeedCount") + "/" + temp [1];
		}
	}

	public float SkillCount {
		set {
			string[] temp = skillText.text.Split ('/');
			skillText.text = PlayerPrefs.GetInt("SkillCount") + "/" + temp [1];
		}
	}

	public float PowerCount {
		set {
			string[] temp = powerText.text.Split ('/');
			powerText.text = PlayerPrefs.GetInt("PowerCount") + "/" + temp [1];
		}
	}

}


