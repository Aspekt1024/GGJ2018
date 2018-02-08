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

    private CanvasGroup canvasGroup;
    bool paused = false;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        DisablePanel();

        float currentVol = PlayerPrefs.GetFloat("MusicVolume");
        MusicVolumeSlider.value = currentVol;
        currentVol = PlayerPrefs.GetFloat("SEVolume");
        SoundEffectsVolumeSlider.value = currentVol;
    }
    
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Unpause();
        }
	}

    public void onClickReturn()
    {
        Unpause();
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

    private void Pause()
    {
        EnablePanel();
        Time.timeScale = 0;
        paused = true;
    }

    private void Unpause()
    {
        DisablePanel();
        Time.timeScale = 1;
        paused = false;
    }

    private void EnablePanel()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    private void DisablePanel()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
