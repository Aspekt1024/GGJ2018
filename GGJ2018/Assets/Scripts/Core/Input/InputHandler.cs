﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private GameManager gameManager;

    public InputHandler(GameManager manager)
    {
        gameManager = manager;
    }

    public void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameManager.LeftMouseClicked();
        }
    }
}

