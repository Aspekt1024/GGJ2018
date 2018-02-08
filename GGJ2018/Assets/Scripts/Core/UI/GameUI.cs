using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
    
    public LogUI LogUI;
    public Selection SelectionHandler;
    public Battery BatteryUI;
    public GameObject TransmitButton;

    public EndGameUI EndGameUI;
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

    public static void DisableTransmitButton()
    {
        Instance.TransmitButton.GetComponent<Animator>().SetBool("IsOn", false);
    }

    public static void EnableTransmitButton()
    {
        Instance.TransmitButton.GetComponent<Animator>().SetBool("IsOn", true);
    }

}
