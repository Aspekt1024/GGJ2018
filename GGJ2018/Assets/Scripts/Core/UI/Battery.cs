using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour {

    public Image SliderImage;

    public void SetBatteryPercentage(float percent)
    {
        SliderImage.fillAmount = percent;

        Color batteryFillColour = Color.Lerp(Color.green, Color.red, 1 - percent);
        batteryFillColour.r += 0.2f;
        batteryFillColour.g += 0.2f;
        batteryFillColour.b += 0.2f;
        SliderImage.color = batteryFillColour;

    }
}
