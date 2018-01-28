using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour {

    public Text EndGameHeadingText;


    public enum EndGameUITypes
    {
        TooManyBlocks, Logout, MaxTurnsReached
    }

    public void EnableUI(EndGameUITypes type)
    {
        gameObject.SetActive(true);

        switch (type)
        {
            case EndGameUITypes.TooManyBlocks:
                EndGameHeadingText.text = "No one likes you.";
                break;
            case EndGameUITypes.Logout:
                EndGameHeadingText.text = "Where did everyone go?";
                break;
            case EndGameUITypes.MaxTurnsReached:
                EndGameHeadingText.text = "End of days.";
                break;
            default:
                break;
        }

    }

    public void DisableUI()
    {
        gameObject.SetActive(false);
    }
}
