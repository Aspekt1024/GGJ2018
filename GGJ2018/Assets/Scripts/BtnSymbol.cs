﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSymbol : MonoBehaviour {

    public Symbols symbol;
    public Selection selection;
    Toggle toggleState;

    private bool state = false;

    private void Start()
    {
        toggleState = GetComponent<Toggle>();
        setSymbol(symbol);
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
        else
            SetStateOff();

        selection.printSize();
    }

    private void SetStateOn()
    {
        ColorBlock cb = toggleState.colors;
        cb.normalColor = cb.highlightedColor = Color.green;
        toggleState.colors = cb;
        if (!selection.isFull())
            selection.addSymbol(symbol);
    }

    private void SetStateOff()
    {
        ColorBlock cb = toggleState.colors;
        cb.normalColor = cb.highlightedColor = Color.white;
        toggleState.colors = cb;
        selection.removeSymbol(symbol);
    }
}
