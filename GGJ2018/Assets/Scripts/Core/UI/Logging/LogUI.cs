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

    private Planet planetFilter;
    private List<LogMessage> messages;

    private float yPadding = 10f;

    private void Start()
    {
        messages = new List<LogMessage>();
    }

    public void SetLogFilter(Planet planet)
    {
        planetFilter = planet;
        UpdateLogs();
    }

    public void ClearFilter()
    {
        planetFilter = null;
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
        int numLogs = logs.Count;
        
        LogContent.sizeDelta = new Vector2(LogContent.sizeDelta.x, numLogs * (yPadding + MessagePrefab.rect.size.y));

        for (int i = 0; i < logs.Count; i++)
        {
            bool containsLog = false;
            foreach (var message in messages)
            {
                if (message.GetEntry() == logs[i])
                {
                    containsLog = true;
                    break;
                }
            }
            if (!containsLog)
            {
                RectTransform newMessageRt = Instantiate(MessagePrefab);
                LogMessage newMessage = newMessageRt.GetComponent<LogMessage>();
                newMessage.SetMessage(logs[i]);
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