using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetClicker : MonoBehaviour
{

    private void Update()
    {
        bool clicked = false;
        Vector2 mousePos = Vector2.zero;

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            clicked = true;
            mousePos = Input.touches[0].position;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            mousePos = Input.mousePosition;
        }


        if (clicked)
        {
            CheckPlanetClicked(mousePos);
        }
    }

    private void CheckPlanetClicked(Vector2 mousePos)
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        LayerMask layers = Helpers.GetMaskInt(Helpers.Layer.Background) | Helpers.GetMaskInt(Helpers.Layer.Planet) | 1 << LayerMask.NameToLayer("UI");
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector3.forward, layers);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == Helpers.GetMask(Helpers.Layer.Planet))
            {
                GameUI.Instance.LogUI.SetLogFilter(hit.collider.GetComponent<Planet>());
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