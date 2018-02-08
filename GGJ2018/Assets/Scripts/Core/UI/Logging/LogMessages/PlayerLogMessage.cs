using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogMessage : LogMessage {

    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public override void SetMessage(Logger.LogEntry logEntry)
    {
        entry = logEntry;

        if (entry.MessageReceived.Length > 0) { Symbols[0].sprite = entry.MessageReceived[0].Sprite; Symbols[0].enabled = true; } else { Symbols[0].enabled = false; }
        if (entry.MessageReceived.Length > 1) { Symbols[1].sprite = entry.MessageReceived[1].Sprite; Symbols[1].enabled = true; } else { Symbols[1].enabled = false; }
        if (entry.MessageReceived.Length > 2) { Symbols[2].sprite = entry.MessageReceived[2].Sprite; Symbols[2].enabled = true; } else { Symbols[2].enabled = false; }
        if (entry.MessageReceived.Length > 3) { Symbols[3].sprite = entry.MessageReceived[3].Sprite; Symbols[3].enabled = true; } else { Symbols[3].enabled = false; }

        float xEdgePadding = 10f;
        float xPadding = 8f;
        float imageWidth = Symbols[0].rectTransform.rect.size.x * Symbols[0].rectTransform.localScale.x;
        float width = xEdgePadding * 2f + entry.MessageReceived.Length * imageWidth + (entry.MessageReceived.Length - 1) * xPadding;
        rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);

        for (int i = 0; i < entry.MessageReceived.Length; i++)
        {
            float xPos = rt.rect.min.x + xEdgePadding + i * xPadding + (i + 0.5f) * imageWidth;
            Symbols[i].rectTransform.localPosition = new Vector2(xPos, 0f);
        }
    }
}
