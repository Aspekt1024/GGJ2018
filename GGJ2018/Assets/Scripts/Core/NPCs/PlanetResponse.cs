using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetResponse : MonoBehaviour {

    public float ShowResponseDuration = 2f;
    public Sprite PositiveResponseImage;
    public Sprite NegativeResponseImage;
    public Sprite NeutralResponseImage;

    private SpriteRenderer responseImage;

    private void Start()
    {
        responseImage = GetComponent<SpriteRenderer>();
        responseImage.enabled = false;
    }

    public void SetReponseImage(Planet planet, int opinion)
    {
        if (opinion > 0)
        {
            responseImage.sprite = PositiveResponseImage;
        }
        else if (opinion < 0)
        {
            responseImage.sprite = NegativeResponseImage;
        }
        else
        {
            responseImage.sprite = NeutralResponseImage;
        }

        responseImage.enabled = true;
        Invoke(((Action)HideResponse).Method.Name, ShowResponseDuration);
    }

    private void HideResponse()
    {
        responseImage.enabled = false;
    }

}
