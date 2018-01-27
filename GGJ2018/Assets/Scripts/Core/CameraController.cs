using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float IntervalTime = 10f;
    public float ZoomDuration = 1f;

    private int levelCount;
    private int[] scaleLevels = {6, 10, 14, 18 };
    private float targetSize = 0;
    private float tParam = 0;
    float timer = 0f;

    private void Start()
    {
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= IntervalTime)
        {
            targetSize = scaleLevels[levelCount];
            StartCoroutine(Grow());

            levelCount++;
            timer = 0f;
        }
    }


    private IEnumerator Grow()
    {
        float zoomTimer = 0f;
        float startOrthSize = Camera.main.orthographicSize;
        while (zoomTimer < ZoomDuration)
        {
            zoomTimer += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(startOrthSize, targetSize, zoomTimer / ZoomDuration);
            yield return null;
        }
    }
}

