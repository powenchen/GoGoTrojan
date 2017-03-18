using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public float rpm;
    private WheelCollider wheel;
    public Vector3 wheelAxis = new Vector3(1, 0, 0);
    public AudioClip accelAudio, brakeAudio;
    private AudioSource accelSound, brakeSound;

	public float speedThreshold = 1;
	public int stepsBelowThreshold = 15, stepsAboveThreshold = 12;
	public float  forwardStiffness = 5,sidewayStiffness = 10;
    private bool meshRotationEnabled = true;
    // Use this for initialization
    void Start()
    {
        accelSound = SetUpEngineAudioSource(accelAudio);
        brakeSound = SetUpEngineAudioSource(brakeAudio);
		WheelInit ();
        meshRotationEnabled = true;
    }

	void WheelInit()
	{
		wheel = GetComponent<WheelCollider>();

		ConfigureWheelSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
		ConfigureFriction(forwardStiffness, sidewayStiffness);
		
	}
    // Update is called once per frame
    void Update()
    {
        rpm = wheel.rpm;
        if (meshRotationEnabled)
        {
            transform.Rotate(wheelAxis * (Time.deltaTime * wheel.rpm / 60f * 360f));
        }
    }

    private void FixedUpdate()
    {
        accelSound.volume = Mathf.Clamp(wheel.rpm / 10000, 0, 0.5f);
        brakeSound.volume = Mathf.Clamp(wheel.brakeTorque / 10000, 0, 0.5f);

    }

   /* public void SlowDown(float debuff)
    {
        slowDownDebuff *= debuff;
        ApplyBrake(wheel.brakeTorque);
        Debug.Log("slow down " + wheel.brakeTorque);
    }*/

   

    public void ApplyBrake(float brakeTorque)
    {
        wheel.brakeTorque = brakeTorque;
    }

    public void ApplyThrottle(float motorTorque)
    {
        wheel.motorTorque = motorTorque;
    }

    public void ApplySteer(float steerAngle)
    {
        wheel.steerAngle = steerAngle;
    }

    public void ConfigureFriction(float forwardStiffness, float sideStiffness)
    {
        WheelFrictionCurve fFriction = wheel.forwardFriction, sFriction = wheel.sidewaysFriction;
        fFriction.stiffness = forwardStiffness;
        sFriction.stiffness = sideStiffness;

        wheel.sidewaysFriction = sFriction;
        wheel.forwardFriction = fFriction;

    }

    public void ConfigureWheelSubsteps(float speedThreshold, int stepsBelowThreshold, int stepsAboveThreshold)
    {
		if (wheel == null) {
		
			Debug.Log ("wheel is null "+name);
		}
		else
        wheel.ConfigureVehicleSubsteps(speedThreshold, stepsBelowThreshold, stepsAboveThreshold);
    }

    private AudioSource SetUpEngineAudioSource(AudioClip clip)
    {
        // create the new audio source component on the game object and set up its properties
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0;
        source.loop = true;

        // start the clip from a random point
        source.time = Random.Range(0f, clip.length);
        source.Play();
        //source.minDistance = 5;
        //source.dopplerLevel = 0;
        return source;
    }

    public void setMeshRotationFlag(bool flag)
    {
        meshRotationEnabled = flag;
    }

}
