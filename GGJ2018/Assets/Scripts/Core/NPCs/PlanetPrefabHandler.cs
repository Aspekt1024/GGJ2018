using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPrefabHandler : MonoBehaviour {

    public GameObject[] PlanetPrefabs;

    private HashSet<GameObject> usedPlanets = new HashSet<GameObject>();
    
    public GameObject GetNewPlanet()
    {
        foreach (var planet in PlanetPrefabs)
        {
            if (usedPlanets.Contains(planet))
            {
                continue;
            }
            else
            {
                usedPlanets.Add(planet);
                return planet;
            }
        }
        return null;
    }


}
