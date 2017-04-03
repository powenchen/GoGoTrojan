using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSkill : Skill {
    public GameObject FlameParticles;
    private GameObject FlameInstance;
    
    // Use this for initialization
    void Start () {
        isSkillUsing = false;

    }
	
	// Update is called once per frame
	void Update () {
        isSkillUsing = (FlameInstance != null);
    }

    public override void stopSkill()
    {
        //FlameParticles.gameObject.SetActive(false);
        if (FlameInstance)
        {
            isSkillUsing = false;
            Destroy(FlameInstance);
        }
        
    }

    public override void activateSkill()
    {
        if (!FlameInstance)
        {
            float itemPutOffset = 3;
            Vector3 spawnPosition = transform.position + 2 * transform.forward * itemPutOffset + 10 * transform.up;
            Quaternion spawnRotation = Quaternion.Euler(transform.rotation.eulerAngles+new Vector3(90, 0, 0));
            FlameInstance = Instantiate(FlameParticles, spawnPosition, spawnRotation,transform);
            FlameInstance.GetComponent<TrapWeapons>().attacker = GetComponent<CarStatus>();
           // Debug.Log("flame local rot = " + FlameInstance.transform.localRotation.eulerAngles.ToString());
            isSkillUsing = true;
        }
    }

 }
