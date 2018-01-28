using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoutUI : MonoBehaviour {

    private bool LogoutConfirmedEndOfGameOHMYGOD;

    public void DisableUI()
    {
        gameObject.SetActive(false);
    }

    public void EnableUI()
    {
        if (LogoutConfirmedEndOfGameOHMYGOD) return;
        gameObject.SetActive(true);
    }

    public void LogoutYesClicked()
    {
        LogoutConfirmedEndOfGameOHMYGOD = true;
        LogoutClicked();
        DisableUI();
    }

    public void LogoutNoClicked()
    {
        DisableUI();
    }


    private void LogoutClicked()
    {
        Planet[] planets = FindObjectsOfType<Planet>();
        foreach (var planet in planets)
        {
            float delay = UnityEngine.Random.Range(0f, 4f);
            planet.StartCoroutine(planet.ExplodePlanet(planet, delay));
        }
        Invoke(((Action)ShowEndGameUI).Method.Name, 5f);
    }

    private void ShowEndGameUI()
    {
        GameUI.ShowEndGameUI(EndGameUI.EndGameUITypes.Logout);
    }
}
