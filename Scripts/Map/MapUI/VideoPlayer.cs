using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoPlayer : MonoBehaviour
{
    public MovieTexture movie;
    public GameObject UIStart;
	// Use this for initialization
	void Start () {
        Restart();
    }

    public void Restart()
    {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        movie.Stop();
        movie.Play();

    }
	// Update is called once per frame
	void Update () {
        if (!movie.isPlaying)
        {
            UIStart.SetActive(true);
            Destroy(gameObject);
        }
        AudioSource audio = GetComponent<AudioSource>();
        if (movie.isPlaying && !audio.isPlaying)
        {
            audio.Play();
        }

        
	}

    public void OnClickSkip()
    {
        UIStart.SetActive(true);
        Destroy(gameObject);
    }
}
