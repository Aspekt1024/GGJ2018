using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Symbols : ScriptableObject {

    public enum SymbolName
    {
        Symbol1, Symbol2, Symbol3, Symbol4, Symbol5, Symbol6, Symbol7
    }

    public Sprite Sprite;
    public bool IsEnabled;
    public SymbolName Name;
}
