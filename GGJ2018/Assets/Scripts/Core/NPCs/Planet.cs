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
    
    private void Awake()
    {
        PlanetNameText.text = PlanetName;
    }

    private void Start()
    {
        GameObject planetPrefab = Instantiate(GameManager.Instance.PlanetPrefabs.GetNewPlanet());
        planetPrefab.transform.SetParent(PlanetPrefab);
        planetPrefab.transform.position = transform.position;
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
    
    public void SetSymbolDict(Symbols[] symbols, int[] weights)
    {
        symbolDict = new Dictionary<Symbols, int>();
        for (int i = 0; i < symbols.Length; i++)
        {
            symbolDict.Add(symbols[i], weights[i]);
        }
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
