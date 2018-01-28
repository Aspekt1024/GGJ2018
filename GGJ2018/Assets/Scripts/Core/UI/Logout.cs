using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Logout : MonoBehaviour {

    public void LogoutClicked()
    {
        GameUI.Instance.LogoutUI.EnableUI();
    }
}
