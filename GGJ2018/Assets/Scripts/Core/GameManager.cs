﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    
    [HideInInspector] public static GameManager Instance;
    private PlanetPrefabHandler planetPrefabs;
    private PlanetUnlocker planetUnlocker;
    
    public PlanetPrefabHandler PlanetPrefabs
    {
        get { return planetPrefabs; }
    }
    
    public Player Player
    {
        get {
            if (player == null)
            {
                player = FindObjectOfType<Player>();
            }
            return player;
        }
    }

    private Player player;

    private enum States
    {
        None, Playing, Paused, InMenu
    }
    private States state;

    private InputHandler inputHandler;

#region LifeCycle
    private void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        planetUnlocker = GetComponent<PlanetUnlocker>();
        planetPrefabs = GetComponent<PlanetPrefabHandler>();
        player = FindObjectOfType<Player>();
        inputHandler = new InputHandler(this);
        state = States.Playing;
	}
	
	private void Update ()
    {
        inputHandler.ProcessInput();
    }
#endregion

#region GameEvents
    public void LeftMouseClicked()
    {
        if (state == States.Playing)
        {
            // Debug.Log("clicked");
        }
    }

    public static void UnlockPhase(PlanetUnlocker.Phase phase)
    {
        Instance.planetUnlocker.UnlockPhase(phase);
    }

    public Planet[] GetUnlockedPlanets()
    {
        return planetUnlocker.GetUnlockedPlanets();
    }
#endregion

}
