using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {

    public LogUI LogUI;

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
        }
    }

    public static void UpdateLog()
    {
        Instance.LogUI.UpdateLogs();
    }
}
