using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthValue : MonoBehaviour {


	public int startingHealth = 100; 
	public int currentHealth;
	public int hurtAmount = 30;
	public int addAmount = 25;

	public Slider HealthSlider;
	public Image damageImage;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


	bool isDead;	// True when the player health <= 0
	bool damaged;	// True when the player gets damaged


	void Awake () {

		currentHealth = startingHealth;
	}


	// Use this for initialization
	void Start () {
		
		HealthSlider.value = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {

		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		if (!isDead) {
			damaged = false;
		}
		
	}

	public void TakeDamage (int amount)
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= amount;

		// Set the health bar's value to the current health.
		HealthSlider.value = currentHealth;

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
		}
	}

	public void AddHealth (int amount)
	{


		// Reduce the current health by the damage amount.
		if ((currentHealth + amount) <= 100) {
			currentHealth += amount;
		} 
		else {
			currentHealth = 100;	
		}
			
		// Set the health bar's value to the current health.
		HealthSlider.value = currentHealth;

	}



	void Death ()
	{
		// Set the death flag so this function won't be called again.
		isDead = true;
		GameObject.Find ("RegularC").SendMessage("DeathFinnish");
	}
}
