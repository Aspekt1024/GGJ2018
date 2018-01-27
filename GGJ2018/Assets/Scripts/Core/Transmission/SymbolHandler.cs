using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolHandler : MonoBehaviour {

    private Symbols[] symbols;

    private void Awake()
    {
        symbols = Resources.LoadAll<Symbols>("Symbols");
    }

    public Symbols[] GetAllSymbols()
    {
        return symbols;
    }

    public Symbols GetSymbol(string symbolName)
    {
        for (int i = 0; i < symbols.Length; i++)
        {
            if (symbols[i].Name == symbolName)
            {
                return symbols[i];
            }
        }
        return null;
    }
}
