using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningPlayer : MonoBehaviour {

    public MovieTexture movie;
    public AudioClip audioClip;
    public GameObject openingCanvas;

    // Use this for initialization
    void Start()
    {
        Restart();
    }

    public void Restart()
    {
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
    void Update()
    {
        if (!movie.isPlaying || Input.GetButton("Fire1"))
        {
            OnQuitVideo();
        }
        AudioSource audio = GetComponent<AudioSource>();
        if (movie.isPlaying && !audio.isPlaying)
        {
            audio.Play();
        }

    }

    void OnQuitVideo()
    {
        openingCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
    
}
