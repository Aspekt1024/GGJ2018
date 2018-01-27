using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject optionsPanel;
    public AudioMixer mixer;

    Slider musicSlider;
    Slider soundSlider;

    private void Awake()
    {
        musicSlider = GameObject.Find("Music Volume").GetComponent<Slider>();
        soundSlider = GameObject.Find("Sound Effects Volume").GetComponent<Slider>();
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void OnClickStart(string lvl)
    {
        //Setup as basic lvl loading
        SceneManager.LoadScene(lvl);
    }

    public void OnClickOptions()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OnClickBack()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
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
