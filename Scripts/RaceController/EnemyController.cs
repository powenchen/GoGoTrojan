using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public Image HealthBar;
	public Image CDBar;

	private bool isDead = false;

	public float maxHealthPoint = 100;
	public float maxCDPoint = 100;

	public float currentHealthPoint = 100;
	public float currentCDPoint = 0;

	public int CDspeed = 10;

	private bool CDstarts = false;


	// Use this for initialization
	void Start () {
		UpdateHealthBar ();
		UpdateCDBar ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead && CDstarts) {
			
			if (currentCDPoint >= maxCDPoint) {
				currentCDPoint = maxCDPoint;

				// implement USEMOVE();
				currentCDPoint = 0;
			} else {
				currentCDPoint += Time.deltaTime * CDspeed;
			}

			UpdateCDBar ();
		}

	}

	private void UpdateHealthBar() {

		float ratio = currentHealthPoint / maxHealthPoint;
		HealthBar.rectTransform.localScale = new Vector3 (ratio, 1, 1);
	
	}

	private void UpdateCDBar() {

		float ratio = currentCDPoint / maxCDPoint;
		CDBar.rectTransform.localScale = new Vector3 (ratio, 1, 1);

	}

	void reduceCDtoZero() {
		currentCDPoint = 0;
		UpdateCDBar ();
	}

	public void takeDamage (float damage) {

		currentHealthPoint -= damage;

		if (currentHealthPoint <= 0) {
			currentHealthPoint = 0;

			Death();
		}

		UpdateHealthBar ();
	}

	public void healDamage(float heal) {

		if (!isDead) {
			
			currentHealthPoint += heal;

			if (currentHealthPoint > maxHealthPoint) {
				currentHealthPoint = maxHealthPoint;
			}

			UpdateHealthBar ();
		}
	}

	private void Death() {
		isDead = true;
		reduceCDtoZero ();
		GameObject.Find ("RaceC").SendMessage("setStopRacing");
		GameObject.Find ("RaceC").SendMessage("setDead");
	}


	public void SetCDstarts() {
		CDstarts = true;
	}

	public void SetCDstops() {
		CDstarts = false;
	}
}
