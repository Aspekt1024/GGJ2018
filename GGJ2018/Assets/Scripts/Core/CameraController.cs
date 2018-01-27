using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    int[] scaleLevels = {6, 10, 14, 18 };
    private float sizeIncrease = 0;
    float zoomSpeed = 0.0015f;
    float tParam = 0;

    private void Start()
    {
        StartCoroutine("CameraGrow");
    }

    private void Update()
    {
        if (Camera.main.orthographicSize < sizeIncrease)
        {
            tParam += zoomSpeed * Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, sizeIncrease, tParam);
        }
        else
            tParam = 0;
    }


    IEnumerator CameraGrow()
    {
        
        for (int LevelCount = 0; LevelCount < scaleLevels.Length; LevelCount++)
        {
            sizeIncrease = scaleLevels[LevelCount];
            yield return new WaitForSeconds(60);
        }
        StopCoroutine("CameraGrow");
        
    }
}

