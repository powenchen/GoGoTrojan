using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : TrapWeapons
{

    public float missileVelocity = 150;
    public float turn = 1;
    private Rigidbody homingMissile;
    private float fuseDelay;//==0 for now
    //private GameObject missileMod;
    //private ParticleSystem SmokePrefab;
    public float damageValue = 2;

    public Transform target;
    //var missileClip : AudioClip;

    private float lifetime = 5;


    public GameObject explosion;

    private void Start()
    {
        Destroy(gameObject, lifetime);
        homingMissile = GetComponent<Rigidbody>();
        Fire();
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            //set target to the closest car
            float dist = Mathf.Infinity;
            foreach (CarStatus status in FindObjectsOfType<CarStatus>())
            {
                if (!status.Equals(attacker))
                {
                    float diff = (status.transform.position - transform.position).sqrMagnitude;
                    if (diff < dist)
                    {
                        dist = diff;
                        target = status.transform;
                    }
                }
            }

            if(target == null)
            {
                return;
            }
        }

        homingMissile.velocity = transform.forward * missileVelocity;

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);


        homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
    }

    private IEnumerable Fire()
    {
        yield return new WaitForSeconds(fuseDelay);
        //AudioSource.PlayClipAtPoint(missileClip, transform.position);

        float distance = Mathf.Infinity;

        foreach (CarStatus status in FindObjectsOfType<CarStatus>())
        {
            if (!status.Equals(attacker))
            {
                float diff = (status.transform.position - transform.position).sqrMagnitude;

                if (diff < distance)
                {

                    distance = diff;
                    target = status.transform;// change target
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarStatus>() != null && other.GetComponent<CarStatus>() != attacker)
        {

            // timestop mode is invincible
            if (other.GetComponent<TimeStopSkill>() != null && other.GetComponent<TimeStopSkill>().isSkillUsing)
            {
                return;
            }
            Debug.Log("before missile HP = " + other.GetComponent<CarStatus>().getHP());
            if (explosion)
            {
                Instantiate(explosion, other.transform.position, other.transform.rotation);
            }

            other.GetComponent<CarStatus>().isAttackedBy(attacker,damageValue);

            Debug.Log("after missile HP = " + other.GetComponent<CarStatus>().getHP());
            Destroy(gameObject);
        }

    }

    
}
