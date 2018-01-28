using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {
    
    public EndGameUI EndGameUI;
    public LogUI LogUI;
    public Selection SelectionHandler;
    public Battery BatteryUI;
    public GameObject TransmitButton;
    public LogoutUI LogoutUI;

    public static GameUI Instance;

    private void Awake()
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

    private void Start()
    {
        LogoutUI.DisableUI();
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

    public static void HideTransmitButton()
    {
        Instance.TransmitButton.SetActive(false);
    }

    public static void ShowTransmitButton()
    {
        Instance.TransmitButton.SetActive(true);
    }


}
