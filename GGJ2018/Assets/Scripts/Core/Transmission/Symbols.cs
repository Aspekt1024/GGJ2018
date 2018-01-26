using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Symbols : ScriptableObject {

    public enum SymbolName
    {
        Symbol1, Symbol2
    }

    public Sprite Sprite;
    public bool IsEnabled;
    public SymbolName Name;
}
