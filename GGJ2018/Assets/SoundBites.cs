using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBites : MonoBehaviour {

    public static SoundBites Instance;


    private AudioSource audioSource;
    public List<AudioClip> soundEffects;
    // Use this for initialization
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

        private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeselectSymbol()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundEffects[1];
            audioSource.Play();
        }
    }
    public void PlayCannotPlace()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundEffects[0];
            audioSource.Play();
        }
    }
    public void PlayDislike()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundEffects[2];
            audioSource.Play();
        }
    }
    public void PlayLike()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundEffects[3];
            audioSource.Play();
        }
    }
    public void PlayMenuButton()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundEffects[4];
            audioSource.Play();
        }
    }
    public void PlaySelectSymbol()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundEffects[5];
            audioSource.Play();
        }
    }
    public void PlayTransmissionSent()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundEffects[6];
            audioSource.Play();
        }
    }
 
}
