using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour {

    public Image BatteryImage;
    public Image LogoffImage;
    public Image BlockedImage;
    public GameObject ReturnToMenuButton;

    public Text ScoreText;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        BlockedImage.enabled = false;
        LogoffImage.enabled = false;
        BatteryImage.enabled = false;
        ScoreText.text = "";
        ReturnToMenuButton.SetActive(false);
    }

    public enum EndGameUITypes
    {
        TooManyBlocks, Logout, MaxTurnsReached
    }

    public void EnableUI(EndGameUITypes type)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        switch (type)
        {
            case EndGameUITypes.TooManyBlocks:
                BlockedImage.enabled = true;
                break;
            case EndGameUITypes.Logout:
                LogoffImage.enabled = true;
                break;
            case EndGameUITypes.MaxTurnsReached:
                BatteryImage.enabled = true;
                break;
            default:
                break;
        }

        ReturnToMenuButton.SetActive(true);
        ScoreText.enabled = true;
        ScoreText.text = string.Format("Number of friends: {0}\nNumber of blocks: {1}\n\nNet popularity: {2}",
                            GameStats.Instance.GetNumFriends(),
                            GameStats.Instance.GetNumBlocks(),
                            GameStats.Instance.GetNetOpinion());

    }

    public void DisableUI()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void ReturnToMenuClicked()
    {
        Application.Quit();
    }
}
