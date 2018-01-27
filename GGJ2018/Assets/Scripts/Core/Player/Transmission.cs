﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : MonoBehaviour {
    
    private HashSet<Planet> planetsHit;
    private float growSpeed;
    private float duration;
    private float size;
    private float elapsedTime;
    private TransmissionRenderer txRenderer;
    private List<Symbols> symbols;

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

    public void Activate(float speed, float duration, List<Symbols> symbols)
    {
        InitialiseComponents();
        txRenderer.Clear();
        this.duration = duration;
        this.symbols = symbols;
        growSpeed = speed;
        size = 0f;
        elapsedTime = 0f;
        gameObject.SetActive(true);

        transform.position = GameManager.Instance.Player.transform.position;
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
                        planet.GiveMessage(symbols);
                        planetsHit.Add(planet);
                    }
                }
            }
        }
    }

}
