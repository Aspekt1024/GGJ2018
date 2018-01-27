using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles key game stats and win conditions
/// </summary>
public class GameStats : MonoBehaviour
{

    public bool EndGameAfterTransmissions = true;
    public int NumTransmissionsBeforeEnd = 20;

    public bool EndGameAfterBlocks = true;
    public int NumBlocksBeforeEnd = 3;

    public static GameStats Instance;

    private int numFriends;
    private int numBlocks;
    private int netOpinion;
    private int numTransmissionsSent;

    private Planet[] allPlanets;

    private List<Planet> friends = new List<Planet>();
    private List<Planet> blocks = new List<Planet>();


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        GetAllPlanets();
    }


    public void AddFriend(Planet planet)
    {
        numFriends++;
        friends.Add(planet);
    }

    public void RemoveFriend(Planet planet)
    {
        numFriends--;
        friends.Remove(planet);
    }

    public int GetNumFriends()
    {
        return numFriends;
    }

    public void AddBlock(Planet planet)
    {
        numBlocks++;
        blocks.Add(planet);
        if (EndGameAfterBlocks && numBlocks == NumBlocksBeforeEnd)
        {
            Debug.Log("END OF GAME - ALL THE BLOCKS!!!!");
        }
    }

    public int GetNumBlocks()
    {
        return numBlocks;
    }

    public int GetNetOpinion()
    {
        CalculateNetOpinion();
        return netOpinion;
    }

    public void SentTransmission()
    {
        numTransmissionsSent++;
        if (EndGameAfterTransmissions && numTransmissionsSent > NumTransmissionsBeforeEnd)
        {
            Debug.Log("END OF GAME!");
        }
    }

    private void CalculateNetOpinion()
    {
        netOpinion = 0;
        foreach (var planet in allPlanets)
        {
            netOpinion += planet.OpinionScript.Opinion;
        }
    }

    private void GetAllPlanets()
    {
        allPlanets = FindObjectsOfType<Planet>();
    }

}
