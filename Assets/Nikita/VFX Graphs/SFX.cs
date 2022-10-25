using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    //public AudioSource explosion;
    //
    ////method start explosion,
    //
    //AudioSource explosion = GetComponent<explosion>();
    //public void PlayExplosion()
    //{
    //
    //    explosion.PlayDelayed(5.0f);
    //}
    

    public AudioSource explosion;
    public float soundDelay = 0;

    private void Start()
    {
        explosion = GetComponent<AudioSource>();
        explosion.PlayDelayed(soundDelay);
        //Invoke("PlayAduio", 2.0f);

    }

    void PlayAudio()
    {
        explosion.Play();
    }

    private void Update()
    {
        
    }
}
