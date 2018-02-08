using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusLogMessage : LogMessage {
    
    public Text PlanetNameText;
    
    public override void SetMessage(Logger.LogEntry logEntry)
    {
        entry = logEntry;
        PlanetNameText.text = logEntry.Planet.PlanetName;
        Symbols[0].sprite = logEntry.StatusSprite;
    }
}
