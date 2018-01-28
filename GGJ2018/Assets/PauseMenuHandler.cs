using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour {

    public AudioMixer mixer;
    public Slider MusicVolumeSlider;
    public Slider SoundEffectsVolumeSlider;

    public GameObject pausePanel;
    bool paused = false;

    private void Awake()
    {
        float currentVol = PlayerPrefs.GetFloat("MusicVolume");
        MusicVolumeSlider.value = currentVol;
        currentVol = PlayerPrefs.GetFloat("SEVolume");
        SoundEffectsVolumeSlider.value = currentVol;
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
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.value);
        PlayerPrefs.SetFloat("SEVolume", SoundEffectsVolumeSlider.value);
        SceneManager.LoadScene(lvl);
    }

    public void ChangeMusicVolume()
    {
        float val = MusicVolumeSlider.value;
        mixer.SetFloat("MusicVolume", val);
    }

    public void ChangeSEVolume()
    {
        float val = SoundEffectsVolumeSlider.value;
        mixer.SetFloat("SoundEffectsVolume", val);
    }
}
