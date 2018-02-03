using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogMessage : MonoBehaviour {

    public Image Symbol1;
    public Image Symbol2;
    public Image Symbol3;
    public Image Symbol4;
    public Text ResponseText;
    public int MessageIndex;

    private Logger.LogEntry entry;
    

    public void SetMessage(Logger.LogEntry logEntry)
    {
        entry = logEntry;
        string response = "";
        if (logEntry.MessageResponse > 0)
        {
            response = "positively";
        }
        else if (logEntry.MessageResponse < 0)
        {
            response = "negatively";
        }
        else
        {
            response = "neutral";
        }
        ResponseText.text = string.Format("{0} responded {1}", entry.Planet.PlanetName, response);

        if (logEntry.MessageReceived.Length > 0) { Symbol1.sprite = logEntry.MessageReceived[0].Sprite; Symbol1.enabled = true; } else { Symbol1.enabled = false; }
        if (logEntry.MessageReceived.Length > 1) { Symbol2.sprite = logEntry.MessageReceived[1].Sprite; Symbol2.enabled = true; } else { Symbol2.enabled = false; }
        if (logEntry.MessageReceived.Length > 2) { Symbol3.sprite = logEntry.MessageReceived[2].Sprite; Symbol3.enabled = true; } else { Symbol3.enabled = false; }
        if (logEntry.MessageReceived.Length > 3) { Symbol4.sprite = logEntry.MessageReceived[3].Sprite; Symbol4.enabled = true; } else { Symbol4.enabled = false; }
    }

    public Logger.LogEntry GetEntry()
    {
        return entry;
    }

    public void ClearMessage()
    {
        entry = null;
    }
}
