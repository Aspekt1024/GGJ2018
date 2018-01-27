using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetResponse : MonoBehaviour {

    public float ShowResponseDuration = 2f;
    public Sprite VeryPositiveResponseImage;
    public Sprite PositiveResponseImage;
    public Sprite NeutralResponseImage;
    public Sprite NegativeResponseImage;
    public Sprite VeryNegativeResponseImage;
    public SpriteRenderer ResponseImage;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetReponseImage(Planet planet, int opinion)
    {
        if (opinion > 0)
        {
            ResponseImage.sprite = PositiveResponseImage;
        }
        else if (opinion < 0)
        {
            ResponseImage.sprite = NegativeResponseImage;
        }
        else
        {
            ResponseImage.sprite = NeutralResponseImage;
        }

        gameObject.SetActive(true);
        Invoke(((Action)HideResponse).Method.Name, ShowResponseDuration);
    }

    private void HideResponse()
    {
        gameObject.SetActive(false);
    }

}
