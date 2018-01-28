using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Logout : MonoBehaviour {

    public void LogoutClicked()
    {
        Planet[] planets = FindObjectsOfType<Planet>();
        foreach (var planet in planets)
        {
            float delay = UnityEngine.Random.Range(0f, 4f);
            StartCoroutine(ExplodePlanet(planet, delay));
        }
        Invoke(((Action)ShowEndGameUI).Method.Name, 5f);
    }

    private IEnumerator ExplodePlanet(Planet planet, float delay)
    {
        yield return new WaitForSeconds(delay);
        planet.Explode();
    }

    private void ShowEndGameUI()
    {
        GameUI.ShowEndGameUI(EndGameUI.EndGameUITypes.Logout);
    }
}
