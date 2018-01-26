using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    
    [HideInInspector] public static GameManager Instance;

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
        }
        DontDestroyOnLoad(gameObject);

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
#endregion

}
