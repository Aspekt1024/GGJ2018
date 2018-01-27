using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetResponse : MonoBehaviour {

    public float ShowResponseDuration = 2f;
    private const string positiveAnimationString = "Positive";
    private const string negativeAnimationString = "Negative";
    private const string neutralAnimationString = "neautralreaction";
    public Animator ResponseAnimator;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetReponseImage(Planet planet, int opinion)
    {
        gameObject.SetActive(true);
        if (opinion > 0)
        {
            ResponseAnimator.Play(positiveAnimationString, 0, 0f);
            SoundBites.Instance.PlayLike();
        }
        else if (opinion < 0)
        {
            ResponseAnimator.Play(negativeAnimationString, 0, 0f);
            SoundBites.Instance.PlayDislike();
        }
        else
        {
            ResponseAnimator.Play(neutralAnimationString, 0, 0f);
        }

        Invoke(((Action)HideResponse).Method.Name, ShowResponseDuration);
    }

    private void HideResponse()
    {
        gameObject.SetActive(false);
    }

}
