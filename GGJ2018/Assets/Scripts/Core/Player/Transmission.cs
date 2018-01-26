using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : MonoBehaviour {

    public Transform Sprites;

    private HashSet<Planet> planetsHit;
    private float growSpeed;
    private float size;
    private Transform playerTf;

    //private HashSet<Symbol> symbols; // TODO add when we have symbols

    private void Awake()
    {
        planetsHit = new HashSet<Planet>();
        playerTf = GameManager.Instance.Player.transform;
    }

    public void Activate(float speed)
    {
        growSpeed = speed;
        size = 0f;
        transform.position = playerTf.position;
        Debug.Log("sending transmission at " + speed + "lightyears per second");
    }

    private void Update()
    {
        size += Time.deltaTime * growSpeed;

        transform.localScale = Vector3.one * size / 2;

        Collider2D[] hit = Physics2D.OverlapCircleAll(playerTf.position, size, Helpers.GetMaskInt(Helpers.Layer.Planet));

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
