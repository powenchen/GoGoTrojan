using UnityEngine;
using System.Collections;
public class DestroyByBoundary : MonoBehaviour
{
	public GameObject explosion;
	void OnTriggerEnter(Collider other) 
	{
		
		if (other.tag == "Enemy")
		{
			Instantiate(explosion, other.transform.position, other.transform.rotation);
			//Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}