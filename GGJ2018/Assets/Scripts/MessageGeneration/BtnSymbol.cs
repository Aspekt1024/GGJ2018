using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSymbol : MonoBehaviour {

    public Symbols symbol;
    public Selection selection;
    public Image ImageRenderer;

    Toggle toggleState;

    private bool state = false;
    private bool hasBeenAdded = false;
    private Color originalColor;

    private void Awake()
    {
        toggleState = GetComponent<Toggle>();
    }

    public void SetSymbol(Symbols symbols)
    {
        symbol = symbols;
        state = false;
        ImageRenderer.sprite = symbol.Sprite;
        originalColor = ImageRenderer.color;
    }
    
    public void ButtonClicked()
    {
        state = !state;

        if (state)
            SetStateOn();
        else if (hasBeenAdded)
        {
            SoundBites.Instance.PlayDeselectSymbol();
            SetStateOff();
        }
    }

    public void SetStateOn()
    {
        if (selection.isFull())
        {
            SoundBites.Instance.PlayCannotPlace();
            return;
        };

        
        ColorBlock cb = toggleState.colors;
        cb.normalColor = cb.highlightedColor = Color.green;
        ImageRenderer.color = cb.normalColor;
        selection.addSymbol(symbol);
        hasBeenAdded = true;
    }

    public void SetStateOff()
    {

        
        ColorBlock cb = toggleState.colors;
        cb.normalColor = cb.highlightedColor = originalColor;
        ImageRenderer.color = cb.normalColor;
        selection.removeSymbol(symbol);
        hasBeenAdded = false;
    }
}
