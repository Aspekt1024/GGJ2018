using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public string PlanetName = "Planet";
    public int NumSymbols = 2;

    private int opinion;
    private Dictionary<Symbols, int> symbolDict;   // symbol, weight
    private SymbolHandler symbolHandler;

    // TODO array of symbols

    private void Start()
    {
        symbolHandler = GameManager.Instance.SymbolHandler;
        symbolDict = new Dictionary<Symbols, int>();
        SetSymbolDict();
    }

    public void GiveMessage(HashSet<Symbols> symbols)
    {
        foreach (Symbols symbol in symbols)
        {
            if (symbolDict.ContainsKey(symbol))
            {
                opinion += symbolDict[symbol];
            }
        }
        Debug.Log(PlanetName + " has new opinion of you: " + opinion);
        // TODO replace with overlay
    }

    private void SetSymbolDict()
    {
        // Used when updating periodically
        
        for (int i = 0; i < NumSymbols; i++)
        {
            // This works. 1 = positive, else negative. Update if this really bugs us later
            int newWeight = Random.Range(0, 2);
            if (newWeight == 0) newWeight = -1;

            Symbols newSymbol = GetNewSymbol();
            if (newSymbol != null)
            {
                symbolDict.Add(newSymbol, newWeight);
            }
        }
    }

    private Symbols GetNewSymbol()
    {
        if (symbolHandler.Symbols.Length == symbolDict.Count)
        {
            return null;
        }
        
        int symbolIndex = Random.Range(0, symbolHandler.Symbols.Length);
        while (symbolDict.ContainsKey(symbolHandler.Symbols[symbolIndex]))
        {
            symbolIndex = Random.Range(0, symbolHandler.Symbols.Length);
        }
        return symbolHandler.Symbols[symbolIndex];
    }

}
