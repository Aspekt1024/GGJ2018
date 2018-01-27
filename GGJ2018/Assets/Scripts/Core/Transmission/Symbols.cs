using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Symbols : ScriptableObject {

<<<<<<< HEAD
    public enum SymbolName
    {
        Symbol1, Symbol2, Symbol3, Symbol4, Symbol5, Symbol6, Symbol7
    }

=======
    public string Name;
>>>>>>> 4f9b48b8764456cbec4841d947bc7995474572d6
    public Sprite Sprite;
    public bool IsEnabled;
}
