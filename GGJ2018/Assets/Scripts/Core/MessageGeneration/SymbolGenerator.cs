using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolGenerator : MonoBehaviour {

    private int numSymbols = 8;
    private BtnSymbol[] availableSymbols;

	// Use this for initialization
	void Awake () {
        availableSymbols = GetComponentsInChildren<BtnSymbol>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectAvailableButtons()
    {
        ResetSymbols();
        var symbols = SymbolManager.Instance.GetAllSymbols();
        Symbols[] selectableSymbols = new Symbols[numSymbols];

        selectableSymbols[0] = symbols[0];
        selectableSymbols[1] = symbols[8];

        int r = Random.Range(0, 3);
        switch (r)
        {
            case 0:
                selectableSymbols[2] = symbols[2];
                selectableSymbols[3] = symbols[3];
                break;
            case 1:
                selectableSymbols[2] = symbols[1];
                selectableSymbols[3] = symbols[3];
                break;
            case 2:
                selectableSymbols[2] = symbols[1];
                selectableSymbols[3] = symbols[2];
                break;
            default:
                break;
        }

        r = Random.Range(0, 3);
        switch (r)
        {
            case 0:
                selectableSymbols[4] = symbols[10];
                selectableSymbols[5] = symbols[11];
                break;
            case 1:
                selectableSymbols[4] = symbols[9];
                selectableSymbols[5] = symbols[11];
                break;
            case 2:
                selectableSymbols[4] = symbols[9];
                selectableSymbols[5] = symbols[10];
                break;
            default:
                break;
        }

        r = Random.Range(0, 4);
        selectableSymbols[6] = symbols[4 + r];

        r = Random.Range(0, 4);
        selectableSymbols[7] = symbols[12 + r];

        System.Random rnd = new System.Random();
        selectableSymbols = selectableSymbols.OrderBy(x => rnd.Next()).ToArray();

        for (int i = 0; i < availableSymbols.Length; i++)
        {
            availableSymbols[i].SetSymbol(selectableSymbols[i]);
        }
    }

    public void ClearSelections()
    {
        foreach (var button in availableSymbols)
        {
            button.SetStateOff();
        }
    }

    private void ResetSymbols()
    {
        for (int i = 0; i < availableSymbols.Length; i++)
        {
            availableSymbols[i].symbol = null;
        }
    }
}
