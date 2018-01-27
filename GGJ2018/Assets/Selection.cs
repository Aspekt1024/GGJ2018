using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    public GameObject[] SelectionList;
    public int numSymbols;

    private List<Symbols> symbolsList = new List<Symbols>();
    private int imageCount = 0;
    private SymbolGenerator generator;
    private Image[] selectionIndicatorImages;


    private void Start()
    {
        generator = GetComponent<SymbolGenerator>();
        generator.SelectAvailableButtons();
        SetPhase(PlanetUnlocker.Phase.Phase0);

        selectionIndicatorImages = new Image[SelectionList.Length];
        for (int i = 0; i < SelectionList.Length; i++)
        {
            selectionIndicatorImages[i] = SelectionList[i].transform.GetChild(0).GetComponent<Image>();
            selectionIndicatorImages[i].sprite = null;
        }
    }

    public void SetPhase(PlanetUnlocker.Phase phase)
    {
        switch (phase)
        {
            case PlanetUnlocker.Phase.Phase0:
                numSymbols = 1;
                break;
            case PlanetUnlocker.Phase.Phase1:
                numSymbols = 2;
                break;
            case PlanetUnlocker.Phase.Phase2:
                numSymbols = 3;
                break;
            case PlanetUnlocker.Phase.Phase3:
                numSymbols = 4;
                break;
            default:
                break;
        }
        UpdateSymbolsUI();
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
            selectionIndicatorImages[imageCount].sprite = symbols.Sprite;
            imageCount++;
        }
    }

    public void removeSymbol(Symbols symbols)
    {
        
        foreach (var selectionItem in selectionIndicatorImages)
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
        SoundBites.Instance.PlayTransmissionSent();
        int numSelectedSymbols = 0;
        for (int i = 0; i < symbolsList.Count; i++)
        {
            if (symbolsList[i].Sprite != null)
            {
                numSelectedSymbols++;
            }
        }

        if (numSelectedSymbols == numSymbols)
        {
            GameManager.Instance.Player.SendTransmission(symbolsList);
            resetList();
            RepositionSelections();
            generator.SelectAvailableButtons();
        }
        else
        {
            // TODO set warning message for player (shakescreen?)
            Debug.LogWarning("need to send full message");
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

        for (int i = 0; i < selectionIndicatorImages.Length; i++)
        {
            selectionIndicatorImages[i].sprite = null;
        }

        for (int i = 0; i < activeSymbols.Count; i++)
        {
            symbolsList[i] = activeSymbols[i];
            selectionIndicatorImages[i].sprite = symbolsList[i].Sprite;
        }

        imageCount = symbolsList.Count;
    }

    private void UpdateSymbolsUI()
    {
        SelectionList[0].SetActive(numSymbols > 0);
        SelectionList[1].SetActive(numSymbols > 1);
        SelectionList[2].SetActive(numSymbols > 2);
        SelectionList[3].SetActive(numSymbols > 3);
    }

}
