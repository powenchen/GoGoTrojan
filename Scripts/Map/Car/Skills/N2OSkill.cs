using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N2OSkill : Skill
{

    private Rigidbody rb;
    private float N2OPower = 5;
    private float N2OTopSpeedModifier = 1.5f;
    public ParticleSystem[] N2OParticles;

    private float N2OTimer = 0;
    private float N2OTime = 3;
    private bool isN2OEmitting = false;
    private bool isN2OReady = true;
    

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        skillSound = SetUpEngineAudioSource(skillAudio);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isN2OEmitting)
        {
            if (Time.time - N2OTimer < N2OTime)
            { 
                rb.AddForce(transform.forward * N2OPower, ForceMode.Acceleration);
            }
            else
            {
                stopSkill();

            }
        }
    }

    public override void stopSkill()
    {
        skillSound.volume = 0;
        isN2OReady = true;
        isN2OEmitting = false;
        foreach (ParticleSystem N2O in N2OParticles)
        {
            N2O.Stop();
        }

        GetComponent<CarStatus>().topSpeedModifier = 1;
        N2OTimer = 0;
    }

    public override void activateSkill()
    {
        //Debug.Log("inheritance called");
        skillSound.volume = 1;

        //originalTopSpeed = GetComponent<CarStatus>().topSpeed;
        GetComponent<CarStatus>().topSpeedModifier = N2OTopSpeedModifier ;
        if (isN2OReady && !isN2OEmitting)
        {
            isN2OReady = false;
            isN2OEmitting = true;
            foreach (ParticleSystem N2O in N2OParticles)
            {
                N2O.Play();
            }
            N2OTimer = Time.time;
        }
        
    }
}
