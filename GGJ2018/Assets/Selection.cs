using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    private List<Symbols> symbolsList = new List<Symbols>();
    private Image[] imageList;
    private int imageCount = 0;

    public int numSymbols;

    private void Start()
    {
        imageList = GetComponentsInChildren<Image>();
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
            imageList[imageCount].sprite = symbols.Sprite;
            imageCount++;
        }
    }

    public void removeSymbol(Symbols symbols)
    {
        symbolsList.Remove(symbols);
        imageList[imageCount-1].sprite = null;
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

    public List<Symbols> OnSubmit()
    {
        return symbolsList;
    }

}
