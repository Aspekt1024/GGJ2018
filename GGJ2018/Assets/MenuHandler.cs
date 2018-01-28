using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

    private float zoomTimer = 0f;
    private float ZoomDuration = 1f;
    private float targetSize = 6;
    private float startOrthSize;
    
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public AudioMixer mixer;

    Slider musicSlider;
    Slider soundSlider;

    private void Awake()
    {
        startOrthSize = Camera.main.orthographicSize;
        musicSlider = GameObject.Find("Music Volume").GetComponent<Slider>();
        soundSlider = GameObject.Find("Sound Effects Volume").GetComponent<Slider>();
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);

        float currentVol = PlayerPrefs.GetFloat("MusicVolume");
        musicSlider.value = currentVol;
        currentVol = PlayerPrefs.GetFloat("SEVolume");
        soundSlider.value = currentVol;
    }

    private void Update()
    {}

    private IEnumerator StartGame()
    {
        yield return null;
        // TODO fade out animation

        SceneManager.LoadScene("MainGame");
    }


    public void OnClickStart(string lvl)
    {
        //Setup as basic lvl loading
        SoundBites.Instance.PlayTransmissionSent();
        StartCoroutine(StartGame());
    }

    public void OnClickOptions()
    {
        SoundBites.Instance.PlayMenuButton();
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OnClickBack()
    {
        SoundBites.Instance.PlayMenuButton();
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SEVolume", soundSlider.value);
    }

    public void OnClickQuit()
    {
        SoundBites.Instance.PlayMenuButton();
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
