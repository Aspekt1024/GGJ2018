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
            audioSource.PlayOneShot(soundEffects[1]);
    }
    public void PlayCannotPlace()
    {
            audioSource.PlayOneShot(soundEffects[0]);
    }
    public void PlayDislike()
    {
            audioSource.PlayOneShot(soundEffects[2]);
    }
    public void PlayLike()
    {

            audioSource.PlayOneShot(soundEffects[3]);

    }
    public void PlayMenuButton()
    {

            audioSource.PlayOneShot(soundEffects[4]);

    }
    public void PlaySelectSymbol()
    {
            audioSource.PlayOneShot(soundEffects[5]);
    }
    public void PlayTransmissionSent()
    {
            audioSource.PlayOneShot(soundEffects[6]);
    }
 
    public void PlayBlockedSound()
    {
        audioSource.PlayOneShot(soundEffects[7]);
    }

    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(soundEffects[8]);
    }

    public void PlayNeutralSound()
    {
        audioSource.PlayOneShot(soundEffects[9]);
    }


}
