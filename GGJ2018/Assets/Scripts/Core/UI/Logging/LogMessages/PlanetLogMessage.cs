using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetLogMessage : LogMessage {
    
    public Text PlanetNameText;

    public Sprite PositiveMessage;
    public Sprite NeutralMessage;
    public Sprite NegativeMessage;
    
    public override void SetMessage(Logger.LogEntry logEntry)
    {
        entry = logEntry;

        PlanetNameText.text = logEntry.Planet.PlanetName;

        if (logEntry.MessageResponse > 0)
        {
            Symbols[0].sprite = PositiveMessage;
        }
        else if (logEntry.MessageResponse < 0)
        {
            Symbols[0].sprite = NegativeMessage;
        }
        else
        {
            Symbols[0].sprite = NeutralMessage;
        }
    }
}
