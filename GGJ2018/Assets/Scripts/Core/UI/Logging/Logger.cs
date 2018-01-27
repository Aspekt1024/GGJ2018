using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour {

    public static Logger Instance;
    // TODO num logs to store
    
    public class LogEntry
    {
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

    public static void AddLog(Planet planet, Symbols[] messageReceived, int messageResponse)
    {
        LogEntry newLog = new LogEntry()
        {
            Planet = planet,
            MessageReceived = messageReceived,
            MessageResponse = messageResponse
        };
        AddLog(newLog);
    }

    public static void AddLog(LogEntry logEntry)
    {
        Instance.GetLogs().Add(logEntry);
        GameUI.UpdateLog();
    }
}
