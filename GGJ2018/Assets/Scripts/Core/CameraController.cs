using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float GrowRate = 1f;
    public float IntervalTime = 1f;

    private float size;

    private void Start()
    {
        size = Camera.main.orthographicSize;
    }

    private void Update()
    {
        return;
        size += GrowRate * Time.deltaTime;
        Camera.main.orthographicSize = size;
    }

}
