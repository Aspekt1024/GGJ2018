using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers {
    
    public enum Layer
    {
        Planet, Player
    }

    public static int GetMaskInt(Layer layerName)
    {
        return 1 << GetMask(layerName);
    }

    public static LayerMask GetMask(Layer layerName)
    {
        return LayerMask.NameToLayer(layerName.ToString());
    }
}
