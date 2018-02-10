using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float IntervalTime = 10f;
    public float ZoomDuration = 1f;

    private int levelCount = 0;
    private int[] scaleLevels = {6, 10, 14, 18};
    public int[] NumberOfTransmissionsToAdvance = {3, 5, 5};
    private int transmissionCount = 0;
    
    private Selection selection;

    public void AddTransmission()
    {
        transmissionCount++;
        if (levelCount >= NumberOfTransmissionsToAdvance.Length) return;

        if (transmissionCount >= NumberOfTransmissionsToAdvance[levelCount])
        {
            IncrementPhase();
        }
    }

    public int GetNewOrthographicSize()
    {
        return scaleLevels[levelCount];
    }

    private void Start()
    {
        Camera.main.orthographicSize = scaleLevels[0];
        selection = FindObjectOfType<Selection>();
        selection.numSymbols = 1;
    }
    
    private void IncrementPhase()
    {
        StartCoroutine(Grow());
        transmissionCount = 0;

        switch (levelCount)
        {
            case 1:
                GameManager.UnlockPhase(PlanetUnlocker.Phase.Phase1);
                GameUI.SetSelectionPhase(PlanetUnlocker.Phase.Phase1);
                break;
            case 2:
                GameManager.UnlockPhase(PlanetUnlocker.Phase.Phase2);
                GameUI.SetSelectionPhase(PlanetUnlocker.Phase.Phase2);
                break;
            case 3:
                GameManager.UnlockPhase(PlanetUnlocker.Phase.Phase3);
                GameUI.SetSelectionPhase(PlanetUnlocker.Phase.Phase3);
                break;
            default:
                break;
        }
    }

    private IEnumerator Grow()
    {
        float zoomTimer = 0f;
        float startOrthSize = Camera.main.orthographicSize;
        selection.numSymbols++;

        levelCount++;
        float targetSize = scaleLevels[levelCount];
        while (zoomTimer < ZoomDuration)
        {
            zoomTimer += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(startOrthSize, targetSize, zoomTimer / ZoomDuration);
            
            yield return null;
        }
    }

}

