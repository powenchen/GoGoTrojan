using UnityEngine;
using System.Collections;
public class DestroyByBoundary : MonoBehaviour
{
	public GameObject explosion;
	void OnTriggerEnter(Collider other) 
	{
		
		if (other.tag == "Player0")
		{
			Instantiate(explosion, other.transform.position, other.transform.rotation);

			//implement takeDamage();
			other.gameObject.SendMessage("takeDamage", 50);

			//implement Stop()

			//Destroy(gameObject);
		}
	}
}