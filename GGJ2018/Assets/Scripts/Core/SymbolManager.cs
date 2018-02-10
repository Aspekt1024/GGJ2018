using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlanetUnlocker))]
public class SymbolManager : MonoBehaviour {

    private int symbolsInGame = 16;

    public static SymbolManager Instance;
    private Symbols[] symbols;
    private PlanetUnlocker planetUnlocker;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        planetUnlocker = GetComponent<PlanetUnlocker>();
        LoadSymbols();
    }

    private void Start()
    {
        AssignPlanetSymbols();
    }

    private void AssignPlanetSymbols()
    {
        Planet[] planets = GetPlanets();

        int[] weights = new int[] { 1, 1, 1, -1, -1, -1 };
        Symbols[] p1Symbols = new Symbols[] { symbols[0], symbols[1], symbols[12], symbols[8], symbols[9], symbols[4] };
        Symbols[] p2Symbols = new Symbols[] { symbols[0], symbols[2], symbols[4], symbols[8], symbols[10], symbols[12] };
        Symbols[] p3Symbols = new Symbols[] { symbols[0], symbols[3], symbols[13], symbols[8], symbols[11], symbols[5] };
        Symbols[] p4Symbols = new Symbols[] { symbols[0], symbols[1], symbols[5], symbols[8], symbols[9], symbols[13] };
        Symbols[] p5Symbols = new Symbols[] { symbols[0], symbols[2], symbols[14], symbols[8], symbols[10], symbols[6] };
        Symbols[] p6Symbols = new Symbols[] { symbols[0], symbols[3], symbols[6], symbols[8], symbols[11], symbols[14] };
        Symbols[] p7Symbols = new Symbols[] { symbols[0], symbols[1], symbols[15], symbols[8], symbols[9], symbols[7] };
        Symbols[] p8Symbols = new Symbols[] { symbols[0], symbols[2], symbols[7], symbols[8], symbols[10], symbols[15] };

        planets[0].SetSymbolDict(p1Symbols, weights);
        planets[1].SetSymbolDict(p2Symbols, weights);
        planets[2].SetSymbolDict(p3Symbols, weights);
        planets[3].SetSymbolDict(p4Symbols, weights);
        planets[4].SetSymbolDict(p5Symbols, weights);
        planets[5].SetSymbolDict(p6Symbols, weights);
        planets[6].SetSymbolDict(p7Symbols, weights);
        planets[7].SetSymbolDict(p8Symbols, weights);
    }

    private Planet[] GetPlanets()
    {
        int numPlanets = planetUnlocker.Phase0Planets.Length + planetUnlocker.Phase1Planets.Length + planetUnlocker.Phase2Planets.Length + planetUnlocker.Phase3Planets.Length;
        Planet[] planets = new Planet[numPlanets];
        int planetIndex = 0;

        for (int i = 0; i < planetUnlocker.Phase0Planets.Length; i++)
        {
            planets[planetIndex] = planetUnlocker.Phase0Planets[i];
            planetIndex++;
        }

        for (int i = 0; i < planetUnlocker.Phase1Planets.Length; i++)
        {
            planets[planetIndex] = planetUnlocker.Phase1Planets[i];
            planetIndex++;
        }

        for (int i = 0; i < planetUnlocker.Phase2Planets.Length; i++)
        {
            planets[planetIndex] = planetUnlocker.Phase2Planets[i];
            planetIndex++;
        }

        for (int i = 0; i < planetUnlocker.Phase3Planets.Length; i++)
        {
            planets[planetIndex] = planetUnlocker.Phase3Planets[i];
            planetIndex++;
        }


        return planets;
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

    private void LoadSymbols()
    {
        Symbols[] allSymbols = Resources.LoadAll<Symbols>("Symbols");
        if (allSymbols.Length < symbolsInGame)
        {
            Debug.LogError("not enough symbols to load into game");
            return;
        }

        HashSet<Symbols> gameSymbols = new HashSet<Symbols>();
        for (int i = 0; i < symbolsInGame; i++)
        {
            Symbols nextSymbol = allSymbols[Random.Range(0, allSymbols.Length)];
            while (gameSymbols.Contains(nextSymbol))
            {
                nextSymbol = allSymbols[Random.Range(0, allSymbols.Length)];
            }
            gameSymbols.Add(nextSymbol);
        }
        symbols = new Symbols[symbolsInGame];
        gameSymbols.CopyTo(symbols);
    }

    // Todo replace with hashset and use .contains
    private bool SymbolAlreadySelected(Symbols symbol)
    {
        bool symbolAlreadySelected = false;
        for (int i = 0; i <= symbols.Length; i++)
        {
            if (symbol == symbols[i])
            {
                symbolAlreadySelected = true;
                break;
            }
        }
        return symbolAlreadySelected;
    }
}
