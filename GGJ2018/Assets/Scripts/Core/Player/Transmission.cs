using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : MonoBehaviour {
    
    private HashSet<Planet> planetsHit;
    private float growSpeed;
    private float duration;
    private float size;
    private float elapsedTime;
    private TransmissionRenderer txRenderer;

    //private HashSet<Symbol> symbols; // TODO add when we have symbols

    private void Awake()
    {
        InitialiseComponents();
    }

    private void InitialiseComponents()
    {
        if (txRenderer == null)
        {
            txRenderer = GetComponent<TransmissionRenderer>();
        }
        planetsHit = new HashSet<Planet>();
    }

    public void Activate(float speed, float duration)
    {
        InitialiseComponents();
        txRenderer.Clear();
        this.duration = duration;
        growSpeed = speed;
        size = 0f;
        elapsedTime = 0f;
        gameObject.SetActive(true);

        transform.position = GameManager.Instance.Player.transform.position;
        Debug.Log("sending transmission at " + speed + " lightyears per second");
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= duration)
        {
            gameObject.SetActive(false);
            return;
        }

        size = elapsedTime * growSpeed;
        
        Collider2D[] hit = Physics2D.OverlapCircleAll(GameManager.Instance.Player.transform.position, size, Helpers.GetMaskInt(Helpers.Layer.Planet));
        txRenderer.RadialScale = size;

        if (hit.Length > 0)
        {
            foreach (Collider2D collider in hit)
            {
                Planet planet = collider.GetComponent<Planet>();
                if (planet != null)
                {
                    if (!planetsHit.Contains(planet))
                    {
                        Debug.Log("hit new planet: " + planet.PlanetName);
                        HashSet<Symbols> symbols = new HashSet<Symbols>();
                        symbols.Add(GameManager.Instance.SymbolHandler.Symbols[0]);
                        symbols.Add(GameManager.Instance.SymbolHandler.Symbols[1]);
                        planet.GiveMessage(symbols);
                        planetsHit.Add(planet);
                    }
                }
            }
        }
    }

}
