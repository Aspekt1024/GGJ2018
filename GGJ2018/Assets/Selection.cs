using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    public Image[] SelectionList;
    public int numSymbols;

    private List<Symbols> symbolsList = new List<Symbols>();
    private int imageCount = 0;


    private void Start()
    {
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
        symbolsList.Remove(symbols);
        SelectionList[imageCount-1].sprite = null;
        imageCount--;
    }

    public void resetList()
    {
        symbolsList = new List<Symbols>();
    }

    public void printSize()
    {
        print(symbolsList.Count);
    }

    public void OnSubmit()
    {
        GameManager.Instance.Player.SendTransmission(symbolsList);
    }

}
