using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoPlayer : MonoBehaviour
{
    public MovieTexture[] movies;
    public GameObject UIStart;
    public AudioClip audioClip;
    private MovieTexture movie;
	// Use this for initialization
	void Start () {
        Restart();
    }

    public void Restart()
    {
        movie = movies[StaticVariables.mapID];
        GetComponent<RawImage>().texture = movie as MovieTexture;
        AudioSource audio = GetComponent<AudioSource>();
        if (audioClip)
        {
            audio.clip = audioClip;
        }
        else
        {
            audio.clip = movie.audioClip;
        }
        movie.Stop();
        movie.Play();

    }
	// Update is called once per frame
	void Update () {
        if (!movie.isPlaying)
        {
            UIStart.SetActive(true);
            gameObject.SetActive(false);
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
        gameObject.SetActive(false);
    }
}
