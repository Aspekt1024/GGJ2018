using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionRenderer : MonoBehaviour {

    public int DegreesPerSegment;
    [HideInInspector] public float RadialScale;

    private float currentAngle;
    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 360 / (DegreesPerSegment + 1);
	}
	
	// Update is called once per frame
	void Update () {
        CreatePoints();
	}

    public void Clear()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, Vector3.zero);
        }
    }

    private void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            x = Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            y = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            lineRenderer.SetPosition(i, new Vector3(x, y, z) * RadialScale);
            currentAngle += DegreesPerSegment;
        }
    }
}
