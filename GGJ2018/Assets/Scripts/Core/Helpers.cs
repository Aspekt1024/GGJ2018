using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers {
    
    public enum Layer
    {
        Planet, Player
    }

    public enum Emotions
    {
        Like, Dislike, StrongLike, StrongDislike, Neutral
    }

    public static int GetMaskInt(Layer layerName)
    {
        return 1 << GetMask(layerName);
    }

    public static LayerMask GetMask(Layer layerName)
    {
        return LayerMask.NameToLayer(layerName.ToString());
    }

    public static Color GetEmotionalTint(Emotions emotion)
    {
        switch (emotion)
        {
            case Emotions.Like:
                return new Color(0.2f, 0.7f, 0.2f);
            case Emotions.Dislike:
                return new Color(0.7f, 0.2f, 0.2f);
            case Emotions.StrongLike:
                return new Color(0f, 1f, 0f);
            case Emotions.StrongDislike:
                return new Color(1f, 0f, 0f);
            case Emotions.Neutral:
                return new Color(1f, 1f, 1f);
            default:
                return new Color(1f, 1f, 1f);
        }
    }
}
