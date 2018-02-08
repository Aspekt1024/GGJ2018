using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogMessage : MonoBehaviour {

    public Image[] Symbols;
    public int MessageIndex;

    public enum MessageAlignment
    {
        Left, Right, Center
    }
    public MessageAlignment Alignment;

    protected Logger.LogEntry entry;

    public virtual void SetMessage(Logger.LogEntry logEntry)
    {
        entry = logEntry;
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
