using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {


    public AudioClip skillAudio;
    protected AudioSource skillSound;
        
    public virtual void activateSkill()
    {
    }

    public virtual void stopSkill()
    {
    }

    protected AudioSource SetUpEngineAudioSource(AudioClip clip)
    {
        if (skillAudio == null)
        {
            return null;
        }
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
}
