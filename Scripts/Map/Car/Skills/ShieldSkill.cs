using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : Skill
{
    public GameObject shieldParticle;
    private float timer = 0;
    private float skillTime = 3;
    private bool isUsingSkill = false;
    private GameObject shieldInstance;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isUsingSkill)
        {
            timer += Time.deltaTime;
            if (timer > skillTime)
            {
                GetComponent<CarStatus>().damageReduction -= 1;
                isUsingSkill = false;
                timer = 0;
                Destroy(shieldInstance);
            }
        }
	}
    public override void activateSkill()
    {
        if (!isUsingSkill)
        {
            GetComponent<CarStatus>().damageReduction += 1;
            isUsingSkill = true;
            Quaternion spawnRot = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(90, 0, 0));
            shieldInstance = Instantiate(shieldParticle, transform.position, spawnRot, transform);
            shieldInstance.GetComponent<ParticleSystem>().Play();
            timer = 0;
        }
    }


}
