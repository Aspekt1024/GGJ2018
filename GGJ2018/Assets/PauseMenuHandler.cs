﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour {

    public AudioMixer mixer;

    public GameObject pausePanel;
    bool paused = false;

    Slider musicSlider;
    Slider soundSlider;

    private void Awake()
    {
        musicSlider = GameObject.Find("Music Volume").GetComponent<Slider>();
        soundSlider = GameObject.Find("Sound Effects Volume").GetComponent<Slider>();
        float currentVol = PlayerPrefs.GetFloat("MusicVolume");
        musicSlider.value = currentVol;
        currentVol = PlayerPrefs.GetFloat("SEVolume");
        soundSlider.value = currentVol;
        //mixer.GetFloat("SoundEffectsVolume", out currentVol);
        //mixer.SetFloat("Music Volume", currentVol);
    }

    // Use this for initialization
    private void Start () {
        pausePanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            paused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        }
	}

    public void onClickReturn()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
    
    public void onClickQuit(string lvl)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SEVolume", soundSlider.value);
        SceneManager.LoadScene(lvl);
    }

    public void ChangeMusicVolume()
    {
        float val = musicSlider.value;
        mixer.SetFloat("MusicVolume", val);
    }

    public void ChangeSEVolume()
    {
        float val = soundSlider.value;
        mixer.SetFloat("SoundEffectsVolume", val);
    }
}
