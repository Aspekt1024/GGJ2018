using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolHandler : MonoBehaviour {

    public int NumSymbolsToLoad = 2;
    
    [HideInInspector] public Symbols[] Symbols;
    
    private void Start()
    {
        Symbols[] symbols = Resources.LoadAll<Symbols>("Symbols");
        // TODO load NumSymbolsToLoad symbols into the Symbols[] array randomly
        foreach (var item in symbols)
        {
            Debug.Log(item.Name);
        }
    }

    public Symbols GetSymbols(string symbolName)
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
