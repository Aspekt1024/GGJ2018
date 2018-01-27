using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float IntervalTime = 10f;
    public float ZoomDuration = 1f;

    private int levelCount = 0;
    private int[] scaleLevels = {10, 14, 18};
    public int[] NumberOfTransmissionsToAdvance = {5, 5, 5};
    private int transmissionCount = 0;


    private Selection selection;

    public void addTransmission()
    {
        transmissionCount++;
    }


    private float targetSize = 0;
   // private float timer = 0f;

    private void Start()
    {
        Camera.main.orthographicSize = 6;
        selection = FindObjectOfType<Selection>();
        selection.numSymbols = 1;
            
    }
    


    private void Update()
    {
       // if (levelCount >= scaleLevels.Length) return;
        //timer += Time.deltaTime;

        if(transmissionCount >= NumberOfTransmissionsToAdvance[levelCount])
        {
            targetSize = scaleLevels[levelCount];
            StartCoroutine(Grow());
            levelCount++;
            transmissionCount = 0;
        }

    }


    private IEnumerator Grow()
    {
        float zoomTimer = 0f;
        float startOrthSize = Camera.main.orthographicSize;
        selection.numSymbols++;
        while (zoomTimer < ZoomDuration)
        {
            zoomTimer += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(startOrthSize, targetSize, zoomTimer / ZoomDuration);
            
            yield return null;
        }
    }
}

