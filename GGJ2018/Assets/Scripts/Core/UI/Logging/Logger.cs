using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour {

    public bool GroupResponsesWithParent = true;

    public static Logger Instance;
    // TODO num logs to store
    
    public enum MessageTypes
    {
        Player, Planet, Status
    }

    public class LogEntry
    {
        public MessageTypes MessageType;
        public Sprite StatusSprite;
        public Planet Planet;
        public Symbols[] MessageReceived;
        public int MessageResponse;
    }
    private List<LogEntry> log;

    public List<LogEntry> GetLogs()
    {
        return log;
    }

	private void Awake ()
    {
		if (Instance == null)
        {
            Instance = this;
            log = new List<LogEntry>();
        }
        else
        {
            Destroy(this);
        }
	}

    public static LogEntry AddStatusLog(Planet planet, Sprite statusSprite, LogEntry parentEntry)
    {
        LogEntry newLog = new LogEntry()
        {
            MessageType = MessageTypes.Status,
            Planet = planet,
            StatusSprite = statusSprite
        };

        int index = GetLogIndex(parentEntry);
        AddLog(newLog, index);
        return newLog;
    }

    public static LogEntry AddLog(Symbols[] messagesSent)
    {
        LogEntry newLog = new LogEntry()
        {
            MessageType = MessageTypes.Player,
            MessageReceived = messagesSent
        };
        AddLog(newLog);
        return newLog;
    }

    public static LogEntry AddLog(Planet planet, Symbols[] messageReceived, int messageResponse, LogEntry parentEntry)
    {
        LogEntry newLog = new LogEntry()
        {
            MessageType = MessageTypes.Planet,
            Planet = planet,
            MessageReceived = messageReceived,
            MessageResponse = messageResponse
        };

        int index = GetLogIndex(parentEntry);
        AddLog(newLog, index);
        return newLog;
    }

    public static void AddLog(LogEntry logEntry, int index = -1)
    {
        if (index >= 0 && Instance.GroupResponsesWithParent)
        {
            Instance.GetLogs().Insert(index, logEntry);
        }
        else
        {
            Instance.GetLogs().Add(logEntry);
        }
        GameUI.UpdateLog();
    }

    private static int GetLogIndex(LogEntry entry)
    {
        List<LogEntry> logs = Instance.GetLogs();
        int index = logs.IndexOf(entry) + 1;
        for (int i = index; i < Instance.GetLogs().Count; i++)
        {
            if (logs[index].MessageType == MessageTypes.Player)
            {
                break;
            }
            index++;
        }
        return index;
    }
}
