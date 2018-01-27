using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogUI : MonoBehaviour {

    public int NumLogsToShow = 500;

    private Planet planetFilter;

    public void SetLogFilter(Planet planet)
    {
        planetFilter = planet;
        UpdateLogs();
    }

    public void ClearFilter()
    {
        planetFilter = null;
    }

    public void UpdateLogs()
    {
        Logger.Instance.GetLogs();
    }
}
