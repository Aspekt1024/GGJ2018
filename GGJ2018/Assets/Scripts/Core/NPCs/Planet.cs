using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {

    public string PlanetName = "Planet";
    public int NumSymbols = 2;
    public PlanetResponse ResponseScript;
    public PlanetOpinion OpinionScript;
    public Transform PlanetPrefab;
    public Text PlanetNameText;
    public GameObject Visuals;
    public GameObject Explosion;

    private int opinion;
    private Dictionary<Symbols, int> symbolDict;   // symbol, weight
    private SymbolHandler symbolHandler;

    private int numHates;
    private int numLikes;

    private void Awake()
    {
        symbolDict = new Dictionary<Symbols, int>();
        PlanetNameText.text = PlanetName;
    }

    private void Start()
    {
        symbolHandler = GameManager.Instance.SymbolHandler;
        GameObject planetPrefab = Instantiate(GameManager.Instance.PlanetPrefabs.GetNewPlanet());
        planetPrefab.transform.SetParent(PlanetPrefab);
        planetPrefab.transform.position = transform.position;
        SetSymbolDict();
    }

    public void GiveMessage(List<Symbols> symbols, Logger.LogEntry entry)
    {
        if (opinion <= OpinionScript.BlockedValue)
        {
            return;
        }

        int messageOpinion = 0;
        foreach (Symbols symbol in symbols)
        {
            if (symbolDict.ContainsKey(symbol))
            {
                messageOpinion += symbolDict[symbol];
            }
        }
        Logger.AddLog(this, symbols.ToArray(), messageOpinion, entry);
        ResponseScript.SetReponseImage(this, messageOpinion);
        opinion += messageOpinion;
        OpinionScript.SetOpinion(opinion, entry);
    }

    private void SetSymbolDict()
    {
        float likesBias = 0.5f;
        for (int i = 0; i < NumSymbols; i++)
        {
            int newWeight = Random.Range(0, 2);
            if (newWeight == 0) newWeight = -1;
            if (numHates >= NumSymbols * (1 - likesBias))
            {
                newWeight = 1;
            }
            else if (numLikes >= NumSymbols * likesBias)
            {
                newWeight = -1;
            }

            if (newWeight > 0) numLikes++;
            if (newWeight < 0) numHates++;

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

    public void Explode()
    {
        SoundBites.Instance.PlayExplosionSound();
        Visuals.SetActive(false);
        Explosion.SetActive(true);
    }
    
    public IEnumerator ExplodePlanet(Planet planet, float delay)
    {
        yield return new WaitForSeconds(delay);
        planet.Explode();
    }
}
