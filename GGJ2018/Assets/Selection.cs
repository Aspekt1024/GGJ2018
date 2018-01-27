using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    public Image[] SelectionList;
    public int numSymbols;

    private List<Symbols> symbolsList = new List<Symbols>();
    private int imageCount = 0;
    private SymbolGenerator generator;


    private void Start()
    {
        generator = GetComponent<SymbolGenerator>();
        generator.SelectAvailableButtons();
    }


    public bool isFull()
    {
        return symbolsList.Count >= numSymbols;
    }

    public void addSymbol(Symbols symbols)
    {
        symbolsList.Add(symbols);
        if(imageCount <= numSymbols)
        {
            SelectionList[imageCount].sprite = symbols.Sprite;
            imageCount++;
        }
    }

    public void removeSymbol(Symbols symbols)
    {
        foreach (var selectionItem in SelectionList)
        {
            if (selectionItem.sprite == symbols.Sprite)
            {
                selectionItem.sprite = null;
                break;
            }
        }

        symbolsList.Remove(symbols);
        RepositionSelections();
    }

    public void resetList()
    {
        symbolsList = new List<Symbols>();
        generator.ClearSelections();
    }

    public void OnSubmit()
    {
        int numSelectedSymbols = 0;
        for (int i = 0; i < symbolsList.Count; i++)
        {
            if (symbolsList[i].Sprite != null)
            {
                numSelectedSymbols++;
            }
        }

        if (numSelectedSymbols > 0)
        {
            GameManager.Instance.Player.SendTransmission(symbolsList);
            resetList();
            RepositionSelections();
            generator.SelectAvailableButtons();
        }
    }

    private void RepositionSelections()
    {
        List<Symbols> activeSymbols = new List<Symbols>();
        for (int i = 0; i < symbolsList.Count; i++)
        {
            if (symbolsList[i] != null)
            {
                activeSymbols.Add(symbolsList[i]);
            }
        }

        for (int i = 0; i < SelectionList.Length; i++)
        {
            SelectionList[i].sprite = null;
        }

        for (int i = 0; i < activeSymbols.Count; i++)
        {
            symbolsList[i] = activeSymbols[i];
            SelectionList[i].sprite = symbolsList[i].Sprite;
        }

        imageCount = symbolsList.Count;
    }

}
