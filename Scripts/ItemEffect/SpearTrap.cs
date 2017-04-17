using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : TrapWeapons
{
    
    private Vector3 myTranslate;
    public AudioClip audioClip;
    public ParticleSystem explosion;
    private float damageValue = 0.15f;
    private float pierceTime;
    private float maxPierceTime = 3;
    private float elapsedTime = 0;
    // Use this for initialization


    void Start () {
        pierceTime = maxPierceTime * Random.Range(0.25f,1f);
    }
    // Update is called once per frame
    void Update () {
        if (elapsedTime < pierceTime)
        {
            transform.position += myTranslate * Time.deltaTime / pierceTime;
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= pierceTime)
            {
                if (Random.value < 0.15)
                {
                    GetComponent<AudioSource>().clip = audioClip;
                    GetComponent<AudioSource>().Play();
                }
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Car>() != null)
        {
            if (other.GetComponent<CarStatus>().Equals(attacker))
            {
                return;
            }
            // timestop mode is invincible
            if (other.GetComponent<TimeStopSkill>() != null && other.GetComponent<TimeStopSkill>().isSkillUsing)
            {
                return;
            }

            other.GetComponent<CarStatus>().isAttackedBy(attacker, damageValue);
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
        }


    }
    

    public void SetTarget(Vector3 pos)
    {

        myTranslate = pos - transform.position;
    }
}
