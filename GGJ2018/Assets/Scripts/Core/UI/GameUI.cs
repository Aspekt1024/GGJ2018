using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {
    
    public EndGameUI EndGameUI;
    public LogUI LogUI;
    public Selection SelectionHandler;

    public static GameUI Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    public static void UpdateLog()
    {
        Instance.LogUI.UpdateLogs();
    }

    public static void SetSelectionPhase(PlanetUnlocker.Phase phase)
    {
        Instance.SelectionHandler.SetPhase(phase);
    }
}
