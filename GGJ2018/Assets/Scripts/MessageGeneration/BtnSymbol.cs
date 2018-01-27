using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSymbol : MonoBehaviour {

    public Symbols symbol;
    public Selection selection;
    Toggle toggleState;

    private bool state = false;
    private bool hasBeenAdded = false;

    private void Awake()
    {
        toggleState = GetComponent<Toggle>();
    }

    public void setSymbol(Symbols symbols)
    {
        symbol = symbols;
        state = false;
        toggleState.image.sprite = symbol.Sprite;
    }
    
    public void ButtonClicked()
    {
        state = !state;

        if (state)
            SetStateOn();
        else if (hasBeenAdded)
        {
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

        SoundBites.Instance.PlaySelectSymbol();
        ColorBlock cb = toggleState.colors;
        cb.normalColor = cb.highlightedColor = Color.green;
        toggleState.colors = cb;
        selection.addSymbol(symbol);
        hasBeenAdded = true;
    }

    public void SetStateOff()
    {

        SoundBites.Instance.PlayDeselectSymbol();
        ColorBlock cb = toggleState.colors;
        cb.normalColor = cb.highlightedColor = Color.white;
        toggleState.colors = cb;
        selection.removeSymbol(symbol);
        hasBeenAdded = false;
    }
}
