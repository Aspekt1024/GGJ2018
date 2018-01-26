using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolHandler : MonoBehaviour {

    public Symbols[] Symbols;


    public Symbols GetSymbols(Symbols.SymbolName symbolName)
    {
        for (int i = 0; i < Symbols.Length; i++)
        {
            if (Symbols[i].Name == symbolName)
            {
                return Symbols[i];
            }
        }
        return null;
    }
}
