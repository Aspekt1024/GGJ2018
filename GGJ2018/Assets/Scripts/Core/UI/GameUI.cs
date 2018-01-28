using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {
    
    public EndGameUI EndGameUI;
    public LogUI LogUI;
    public Selection SelectionHandler;
    public Battery BatteryUI;

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

        EndGameUI.DisableUI();
    }

    public static void UpdateLog()
    {
        Instance.LogUI.UpdateLogs();
    }

    public static void SetSelectionPhase(PlanetUnlocker.Phase phase)
    {
        Instance.SelectionHandler.SetPhase(phase);
    }

    public static void ShowEndGameUI(EndGameUI.EndGameUITypes endGameType)
    {
        Instance.EndGameUI.EnableUI(endGameType);
    }

    public static void SetBatteryPercent(float percent)
    {
        Instance.BatteryUI.SetBatteryPercentage(percent);
    }
}
