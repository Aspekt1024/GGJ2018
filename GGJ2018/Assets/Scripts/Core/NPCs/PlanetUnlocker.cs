using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetUnlocker : MonoBehaviour {

    public enum Phase
    {
        Phase0, Phase1, Phase2, Phase3
    }

    public Planet[] Phase1Planets;
    public Planet[] Phase2Planets;
    public Planet[] Phase3Planets;

    private CameraController cameraController;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

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
        float currentOrth = Camera.main.orthographicSize;
        float newOrth = cameraController.GetNewOrthographicSize();
        float sizeRatio = newOrth / currentOrth;

        Vector2 sizeWorldUnits = Camera.main.ViewportToWorldPoint(Vector2.one) - Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 newSize = new Vector2(sizeWorldUnits.x * sizeRatio, sizeWorldUnits.y * sizeRatio);

        foreach (var planet in planetsToUnlock)
        {
            planet.gameObject.SetActive(true);
            CorrectPlanetPosition(planet.gameObject, newSize);
        }
    }

    private void CorrectPlanetPosition(GameObject planet, Vector2 newSize)
    {
        Vector2 planetScale = planet.transform.localScale;
        float planetRadius = planet.GetComponent<CircleCollider2D>().radius;
        Vector2 planetPos = planet.transform.position;
        float xBound = Camera.main.transform.position.x + newSize.x / 2f - planetScale.x * planetRadius;
        float yBound = Camera.main.transform.position.y + newSize.y / 2f - planetScale.y * planetRadius;

        if (planetPos.x < -xBound)
        {
            planet.transform.position += Vector3.right * (-xBound - planetPos.x);
        }
        else if (planetPos.x > xBound)
        {
            planet.transform.position += Vector3.left * (planetPos.x - xBound);
        }

        if (planetPos.y > yBound)
        {
            planet.transform.position += Vector3.down * (planetPos.y - yBound);
        }
    }

}
