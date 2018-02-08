using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogUI : MonoBehaviour
{
    public RectTransform LogContent;
    public RectTransform ViewPort;
    public Scrollbar VerticalScroll;
    public Text PlanetFeedIndicatorText;

    public RectTransform PlayerMessagePrefab;
    public RectTransform PlanetMessagePrefab;
    public RectTransform StatusMessagePrefab;
    
    private Planet planetFilter;

    private Vector2 startPos;
    private float yPadding = 10f;
    private float xPadding = 14f;

    private int numMessageObjects = 8;
    private LogMessage[] messageObjects;

    private int numPlayerMessageObjects = 8;
    private int numPlanetMessageObjects = 8;
    private int numStatusMessageObjects = 8;
    private PlayerLogMessage[] playerMessageObjects;
    private PlanetLogMessage[] planetMessageObjects;
    private StatusLogMessage[] statusMessageObjects;

    private Range prevRange;
    private bool resetLogs;
    private List<Logger.LogEntry> logsToPrint;

    private Coroutine scrollToBottomRoutine;

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

        int planetObjectIndex = 0;
        int playerObjectIndex = 0;
        int statusObjectIndex = 0;
        for (int i = 0; i < indexRange.max - indexRange.min + 1; i++)
        {
            Logger.LogEntry entry = logsToPrint[indexRange.max - i];
            if (entry.MessageType == Logger.MessageTypes.Planet)
            {
                messageObjects[i] = planetMessageObjects[planetObjectIndex];
                planetObjectIndex++;
            }
            else if (entry.MessageType == Logger.MessageTypes.Status)
            {
                messageObjects[i] = statusMessageObjects[statusObjectIndex];
                statusObjectIndex++;
            }
            else
            {
                messageObjects[i] = playerMessageObjects[playerObjectIndex];
                playerObjectIndex++;
            }

            messageObjects[i].gameObject.SetActive(true);
            messageObjects[i].SetMessage(entry);
            PositionMessageObject(messageObjects[i], logsToPrint.Count - 1 + i - indexRange.max);
        }
        for (int i = planetObjectIndex; i < planetMessageObjects.Length; i++)
        {
            planetMessageObjects[i].gameObject.SetActive(false);
        }
        for (int i = playerObjectIndex; i < playerMessageObjects.Length; i++)
        {
            playerMessageObjects[i].gameObject.SetActive(false);
        }
        for (int i = statusObjectIndex; i < statusMessageObjects.Length; i++)
        {
            statusMessageObjects[i].gameObject.SetActive(false);
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
                if (log.Planet == null || log.Planet.gameObject == planetFilter.gameObject)
                {
                    logsToPrint.Add(log);
                }
            }
        }

        // Note: All message prefabs must be the same height for this to work
        float ySize = logsToPrint.Count * (yPadding + PlayerMessagePrefab.rect.size.y) + 10f;
        if (ySize < ViewPort.rect.height)
        {
            ySize = ViewPort.rect.height;
        }

        LogContent.sizeDelta = new Vector2(LogContent.sizeDelta.x, ySize);
        RecalculateStartPos();

        if (scrollToBottomRoutine != null)
        {
            StopCoroutine(scrollToBottomRoutine);
        }
        scrollToBottomRoutine = StartCoroutine(SetScrollBottom());
    }

    private void CreateMessagePool()
    {
        messageObjects = new LogMessage[numMessageObjects];
        playerMessageObjects = new PlayerLogMessage[numPlayerMessageObjects];
        planetMessageObjects = new PlanetLogMessage[numPlanetMessageObjects];
        statusMessageObjects = new StatusLogMessage[numStatusMessageObjects];
        for (int i = 0; i < playerMessageObjects.Length; i++)
        {
            playerMessageObjects[i] = (PlayerLogMessage)CreateMessageObject(PlayerMessagePrefab);
        }
        for (int i = 0; i < planetMessageObjects.Length; i++)
        {
            planetMessageObjects[i] = (PlanetLogMessage)CreateMessageObject(PlanetMessagePrefab);
        }
        for (int i = 0; i < statusMessageObjects.Length; i++)
        {
            statusMessageObjects[i] = (StatusLogMessage)CreateMessageObject(StatusMessagePrefab);
        }
    }

    private LogMessage CreateMessageObject(RectTransform prefab)
    {
        RectTransform newMessageRt = Instantiate(prefab);
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

        float xPos = messageRt.rect.size.x / 2;
        float xScrollbarPadding = 4f;
        if (messageObject.Alignment == LogMessage.MessageAlignment.Right)
        {
            xPos = LogContent.rect.xMax - xPadding - xPos - xScrollbarPadding;
        }
        else
        {
            xPos = LogContent.rect.xMin + xPadding + xPos;
        }
        
        float yPos = startPos.y + messageRt.rect.size.y / 2 + (yPadding + messageRt.rect.size.y) * posFromBottom;
        messageObject.transform.localPosition = new Vector2(xPos, yPos);
    }

    private void RecalculateStartPos()
    {
        float xPos = LogContent.rect.xMin + xPadding;
        float yPos = LogContent.rect.yMin + yPadding;

        startPos = new Vector2(xPos, yPos);
    }

    private IEnumerator SetScrollBottom()
    {
        yield return null;
        float scrollDuration = 0.3f;
        float timer = 0;
        float originalPos = VerticalScroll.value;

        while (timer < scrollDuration)
        {
            timer += Time.deltaTime;

            VerticalScroll.value = Mathf.Lerp(originalPos, 0, timer / scrollDuration);
            yield return null;
        }
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

        int rMin = logsToPrint.Count - 1 - rng.max;
        int rMax = logsToPrint.Count - 1 - rng.min;

        rng.min = rMin;
        rng.max = rMax;

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