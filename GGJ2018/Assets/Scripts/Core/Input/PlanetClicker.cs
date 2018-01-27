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
            LayerMask layers = Helpers.GetMaskInt(Helpers.Layer.Background) | Helpers.GetMaskInt(Helpers.Layer.Planet) | 1 << LayerMask.NameToLayer("UI");
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector3.forward, layers);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == Helpers.GetMask(Helpers.Layer.Planet))
                {
                    GameUI.Instance.LogUI.SetLogFilter(hit.collider.GetComponent<Planet>());
                    hit.collider.GetComponent<Planet>().Explode();
                }
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    return;
                }
                else
                {
                    GameUI.Instance.LogUI.ClearFilter();
                }
            }
        }
    }
}