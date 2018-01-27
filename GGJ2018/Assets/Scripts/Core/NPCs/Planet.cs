using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public string PlanetName = "Planet";
    public int NumSymbols = 2;
    public bool ShowThoughts = false;
    public PlanetResponse ResponseScript;
    public PlanetOpinion OpinionScript;
    public PlanetThoughts ThoughtsScript;

    private int opinion;
    private Dictionary<Symbols, int> symbolDict;   // symbol, weight
    private SymbolHandler symbolHandler;

    // TODO array of symbols

    private void Start()
    {
        symbolHandler = GameManager.Instance.SymbolHandler;
        symbolDict = new Dictionary<Symbols, int>();
        SetSymbolDict();
        ThoughtsScript.SetThoughts(symbolDict);
    }

    public void GiveMessage(List<Symbols> symbols)
    {
        int messageOpinion = 0;
        foreach (Symbols symbol in symbols)
        {
            if (symbolDict.ContainsKey(symbol))
            {
                messageOpinion += symbolDict[symbol];
            }
        }
        ResponseScript.SetReponseImage(this, messageOpinion);
        opinion += messageOpinion;
        OpinionScript.SetOpinion(opinion);
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
        if (symbolHandler.GetAllSymbols().Length == symbolDict.Count)
        {
            return null;
        }
        
        int symbolIndex = Random.Range(0, symbolHandler.GetAllSymbols().Length);
        while (symbolDict.ContainsKey(symbolHandler.GetAllSymbols()[symbolIndex]))
        {
            symbolIndex = Random.Range(0, symbolHandler.GetAllSymbols().Length);
        }
        return symbolHandler.GetAllSymbols()[symbolIndex];
    }

}
