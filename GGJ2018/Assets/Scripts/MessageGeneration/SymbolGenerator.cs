using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolGenerator : MonoBehaviour {

    private BtnSymbol[] availableSymbols;

	// Use this for initialization
	void Start () {
        availableSymbols = GetComponentsInChildren<BtnSymbol>();
        SelectAvailableButtons();

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

    private Symbols GetUniqueSymbol()
    {
        var symbols = GameManager.Instance.SymbolHandler.GetAllSymbols();
        Symbols newSymbol = symbols[Random.Range(0, symbols.Length)];

        return newSymbol;
    }

    private void ResetSymbols()
    {
        for (int i = 0; i < availableSymbols.Length; i++)
        {
            availableSymbols[i].symbol = null;
        }
    }
}
