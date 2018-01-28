using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logout : MonoBehaviour {

    public void LogoutClicked()
    {
        Planet[] planets = FindObjectsOfType<Planet>();
        foreach (var planet in planets)
        {
            float delay = Random.Range(0f, 4f);
            StartCoroutine(ExplodePlanet(planet, delay));
        }
    }

    private IEnumerator ExplodePlanet(Planet planet, float delay)
    {
        yield return new WaitForSeconds(delay);
        planet.Explode();
    }
}
