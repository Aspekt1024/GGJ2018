using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogUI : MonoBehaviour
{

    public int NumLogsToShow = 500;
    public RectTransform LogContent;
    public RectTransform MessagePrefab;
    public Scrollbar VericalScroll;
    public Text PlanetFeedIndicatorText;

    private Planet planetFilter;
    private List<LogMessage> messages;

    private float yPadding = 10f;

    private void Start()
    {
        messages = new List<LogMessage>();
        PlanetFeedIndicatorText.text = "";
    }

    public void SetLogFilter(Planet planet)
    {
        planetFilter = planet;
        PlanetFeedIndicatorText.text = "@" + planet.PlanetName + ":";
        UpdateLogs();
    }

    public void ClearFilter()
    {
        planetFilter = null;
        PlanetFeedIndicatorText.text = "";
        UpdateLogs();
    }

    private void ClearAllLogs()
    {
        foreach (var message in messages)
        {
            Destroy(message.gameObject);
            //message.ClearMessage();
            //message.gameObject.SetActive(false);
        }
        messages = new List<LogMessage>();
    }

    public void UpdateLogs()
    {
        ClearAllLogs();
        List<Logger.LogEntry> logs = Logger.Instance.GetLogs();

        List<Logger.LogEntry> logsToPrint = new List<Logger.LogEntry>();
        
        if (planetFilter == null)
        {
            logsToPrint = logs;
        }
        else
        {
            foreach (var log in logs)
            {
                if (log.Planet.gameObject == planetFilter.gameObject)
                {
                    logsToPrint.Add(log);
                }
            }
        }

        int numLogs = logsToPrint.Count;

        LogContent.sizeDelta = new Vector2(LogContent.sizeDelta.x, numLogs * (yPadding + MessagePrefab.rect.size.y));

        for (int i = 0; i < logsToPrint.Count; i++)
        {
            bool containsLog = false;
            foreach (var message in messages)
            {
                if (message.GetEntry() == logsToPrint[i])
                {
                    containsLog = true;
                    break;
                }
            }
            if (!containsLog)
            {
                RectTransform newMessageRt = Instantiate(MessagePrefab);
                LogMessage newMessage = newMessageRt.GetComponent<LogMessage>();
                newMessage.SetMessage(logsToPrint[i]);
                messages.Add(newMessage);

                newMessageRt.SetParent(LogContent);
                newMessageRt.localScale = Vector3.one;
                Vector3 startPos = LogContent.position + Vector3.right * newMessageRt.rect.size.x / 2 + Vector3.up * (LogContent.rect.yMin + newMessageRt.rect.size.y / 2 + yPadding);
                newMessageRt.localPosition = startPos + Vector3.up * (numLogs - i - 1) * (newMessageRt.rect.size.y + yPadding);
            }
        }
        Invoke(((Action)SetScrollBottom).Method.Name, 0.1f);
    }

    private void SetScrollBottom()
    {
        VericalScroll.value = 0;
    }
}