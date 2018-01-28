using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetUnlocker : MonoBehaviour {

    public enum Phase
    {
        Phase0, Phase1, Phase2, Phase3
    }

    public Planet[] Phase1Planets;
    public Planet[] Phase2Planets;
    public Planet[] Phase3Planets;

    public void UnlockPhase(Phase phase)
    {
        switch (phase)
        {
            case Phase.Phase0:
                break;
            case Phase.Phase1:
                UnlockPlanets(Phase1Planets);
                break;
            case Phase.Phase2:
                UnlockPlanets(Phase2Planets);
                break;
            case Phase.Phase3:
                UnlockPlanets(Phase3Planets);
                break;
            default:
                break;
        }
    }

    private void UnlockPlanets(Planet[] planetsToUnlock)
    {
        foreach (var planet in planetsToUnlock)
        {
            planet.gameObject.SetActive(true);
        }
    }

}
