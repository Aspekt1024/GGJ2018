using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : MonoBehaviour {
    
    private float growSpeed;
    private float duration;
    private float size;
    private float elapsedTime;
    private TransmissionRenderer txRenderer;
    private List<Symbols> symbols;
    private Logger.LogEntry logEntry;

    private Dictionary<Planet, bool> planetsToReach;
    private bool endAfterPlanetsReached;

    private void Awake()
    {
        InitialiseComponents();
    }

    private void InitialiseComponents()
    {
        if (txRenderer == null)
        {
            txRenderer = GetComponent<TransmissionRenderer>();
        }
    }

    public void SetPlanetsToReach(Planet[] planets)
    {
        planetsToReach = new Dictionary<Planet, bool>();
        foreach (var planet in planets)
        {
            planetsToReach.Add(planet, false);
        }
    }

    public void Activate(float speed, float duration, List<Symbols> symbols)
    {
        InitialiseComponents();
        txRenderer.Clear();
        this.duration = duration;
        this.symbols = symbols;
        growSpeed = speed;
        size = 0f;
        elapsedTime = 0f;
        gameObject.SetActive(true);

        transform.position = GameManager.Instance.Player.transform.position;
    }

    public void SetLogEntry(Logger.LogEntry entry)
    {
        logEntry = entry;
    }

    public void EndAfterAllPlanetsReached()
    {
        endAfterPlanetsReached = true;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= duration)
        {
            gameObject.SetActive(false);
            return;
        }

        size = elapsedTime * growSpeed;
        
        Collider2D[] hit = Physics2D.OverlapCircleAll(GameManager.Instance.Player.transform.position, size, Helpers.GetMaskInt(Helpers.Layer.Planet));
        txRenderer.RadialScale = size;

        if (hit.Length > 0)
        {
            foreach (Collider2D collider in hit)
            {
                Planet planet = collider.GetComponent<Planet>();
                if (planet != null && planetsToReach.ContainsKey(planet) && planetsToReach[planet] == false)
                {
                    planet.GiveMessage(symbols, logEntry);
                    planetsToReach[planet] = true;
                }
            }
        }

        CheckAllPlanetsReached();
    }

    private void CheckAllPlanetsReached()
    {
        if (endAfterPlanetsReached && !planetsToReach.ContainsValue(false))
        {
            Invoke("ShowEndGame", 2f);
        }
    }

    private void ShowEndGame()
    {
        GameUI.ShowEndGameUI(EndGameUI.EndGameUITypes.MaxTurnsReached);
    }

}
