using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nitro : MonoBehaviour {
	public ParticleSystem[] nitros;
	public float playTime = 3;
	private Rigidbody rb;
	public float addForcetest = 2000;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKey (KeyCode.N)) {
//			StartCoroutine (NitroStart ());
//		}
	}

	public void NitroStart()
	{
		rb.AddForce (transform.forward * addForcetest,ForceMode.Acceleration);
		foreach (ParticleSystem N2O in nitros) {
			N2O.Play ();

			Invoke ("NitroStop", 2.0f);
		}
//		yield return new WaitForSeconds (playTime);
//		foreach (ParticleSystem N2O in nitros) {
//			N2O.Stop ();
//		}
	}

	void NitroStop() {
		foreach (ParticleSystem N2O in nitros) {
			N2O.Stop ();
		}
	}
}
