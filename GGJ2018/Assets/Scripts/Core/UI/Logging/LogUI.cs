using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogUI : MonoBehaviour
{
    public RectTransform LogContent;
    public RectTransform ViewPort;
    public RectTransform MessagePrefab;
    public Scrollbar VerticalScroll;
    public Text PlanetFeedIndicatorText;

    private Planet planetFilter;

    private Vector2 startPos;
    private float yPadding = 10f;

    private int numMessageObjects = 10;
    private LogMessage[] messageObjects;

    private Range prevRange;
    private bool resetLogs;
    private List<Logger.LogEntry> logsToPrint;

    private void Start()
    {
        PlanetFeedIndicatorText.text = "";
        CreateMessagePool();
    }

    private void Update()
    {
        if (logsToPrint == null || logsToPrint.Count == 0) return;

        Range indexRange = GetMinMaxIndex();
        if (!resetLogs && prevRange.Equals(indexRange)) return;

        resetLogs = false;
        prevRange = indexRange;
        for (int i = 0; i < indexRange.max - indexRange.min + 1; i++)
        {
            messageObjects[i].gameObject.SetActive(true);
            PositionMessageObject(messageObjects[i], indexRange.min + i);
            messageObjects[i].SetMessage(logsToPrint[indexRange.min + i]);
        }
        for (int i = indexRange.max - indexRange.min + 1; i < messageObjects.Length; i++)
        {
            messageObjects[i].gameObject.SetActive(false);
        }
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

    public void UpdateLogs()
    {
        resetLogs = true;
        List<Logger.LogEntry> logs = Logger.Instance.GetLogs();

        logsToPrint = new List<Logger.LogEntry>();
        
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
        
        float ySize = logsToPrint.Count * (yPadding + MessagePrefab.rect.size.y) + 10f;
        if (ySize < ViewPort.rect.height)
        {
            ySize = ViewPort.rect.height;
        }

        LogContent.sizeDelta = new Vector2(LogContent.sizeDelta.x, ySize);
        RecalculateStartPos();
        Invoke(((Action)SetScrollBottom).Method.Name, 0.02f);
    }

    private void CreateMessagePool()
    {
        messageObjects = new LogMessage[numMessageObjects];
        for (int i = 0; i < messageObjects.Length; i++)
        {
            messageObjects[i] = CreateMessageObject();
        }
    }

    private LogMessage CreateMessageObject()
    {
        RectTransform newMessageRt = Instantiate(MessagePrefab);
        LogMessage newMessage = newMessageRt.GetComponent<LogMessage>();

        newMessageRt.SetParent(LogContent);
        newMessageRt.localScale = Vector3.one;

        newMessageRt.localPosition = startPos;
        newMessageRt.gameObject.SetActive(false);
        return newMessage;
    }

    private void PositionMessageObject(LogMessage messageObject, int posFromBottom)
    {
        var messageRt = messageObject.GetComponent<RectTransform>();
        messageObject.transform.localPosition = startPos + Vector2.up * posFromBottom * (messageRt.rect.size.y + yPadding);
    }

    private void RecalculateStartPos()
    {
        float xOffset = 14f;
        float xPos = LogContent.rect.xMin + xOffset + MessagePrefab.rect.size.x / 2;
        float yPos = LogContent.rect.yMin + MessagePrefab.rect.size.y / 2 + yPadding;

        startPos = new Vector2(xPos, yPos);
    }

    private void SetScrollBottom()
    {
        VerticalScroll.value = 0;
    }

    private Range GetMinMaxIndex()
    {
        float pos = VerticalScroll.value;
        Range rng = new Range()
        {
            min = Mathf.RoundToInt(logsToPrint.Count * pos - numMessageObjects / 2f),
            max = Mathf.RoundToInt(logsToPrint.Count * pos + numMessageObjects / 2f)
        };
        rng.min = Mathf.Clamp(rng.min, 0, logsToPrint.Count - 1);
        rng.max = Mathf.Clamp(rng.max, 0, logsToPrint.Count - 1);
        if (rng.max - rng.min + 1 > numMessageObjects)
        {
            if (rng.max == numMessageObjects - 1)
            {
                rng.min++;
            }
            else
            {
                rng.max--;
            }
        }
        return rng;
    }

    private struct Range
    {
        public int min;
        public int max;

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(Range)))
            {
                return ((Range)obj).min == min && ((Range)obj).max == max;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}