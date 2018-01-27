using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolGenerator : MonoBehaviour {

    private BtnSymbol[] availableSymbols;

	// Use this for initialization
	void Start () {
        availableSymbols = GetComponentsInChildren<BtnSymbol>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectAvailableButtons()
    {
        ResetSymbols();

        for (int i = 0; i < availableSymbols.Length; i++)
        {
            Symbols newSymbol = GetUniqueSymbol();
            availableSymbols[i].setSymbol(newSymbol);
        }
    }

    public void ClearSelections()
    {
        foreach (var button in availableSymbols)
        {
            button.SetStateOff();
        }
    }

    private Symbols GetUniqueSymbol()
    {
        var symbols = GameManager.Instance.SymbolHandler.GetAllSymbols();

        Symbols newSymbol = symbols[Random.Range(0, symbols.Length)];
        bool isUnique = CheckIfUnique(newSymbol);

        while (!isUnique)
        {
            newSymbol = symbols[Random.Range(0, symbols.Length)];
            isUnique = CheckIfUnique(newSymbol);
        }

        return newSymbol;
    }

    private bool CheckIfUnique(Symbols newSymbol)
    {
        for (int i = 0; i < availableSymbols.Length; i++)
        {
            if (availableSymbols[i].symbol == newSymbol)
            {
                return false;
            }
        }
        return true;
    }

    private void ResetSymbols()
    {
        for (int i = 0; i < availableSymbols.Length; i++)
        {
            availableSymbols[i].symbol = null;
        }
    }
}
