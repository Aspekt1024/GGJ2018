using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetClicker : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector3.forward, Helpers.GetMaskInt(Helpers.Layer.Planet));
            if (hit.collider != null)
            {
                GameUI.Instance.LogUI.SetLogFilter(hit.collider.GetComponent<Planet>());
            }
        }
    }
}