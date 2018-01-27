using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetThoughts : MonoBehaviour {

    public SpriteRenderer Thought1;
    public SpriteRenderer Thought2;
    public SpriteRenderer Thought3;
    public SpriteRenderer Thought4;

    public void SetThoughts(Dictionary<Symbols, int> thoughtsDict)
    {
        ClearThoughts();

        int thoughtNum = 0;
        foreach (var thought in thoughtsDict)
        {
            if (thoughtNum == 0) SetThoughtImage(Thought1, thought);
            if (thoughtNum == 1) SetThoughtImage(Thought2, thought);
            if (thoughtNum == 2) SetThoughtImage(Thought3, thought);
            if (thoughtNum == 3) SetThoughtImage(Thought4, thought);

            thoughtNum++;
        }
    }

    private void SetThoughtImage(SpriteRenderer thoughtRenderer, KeyValuePair<Symbols, int> thought)
    {
        thoughtRenderer.sprite = thought.Key.Sprite;
        thoughtRenderer.color = SetColourFromOpinion(thought.Value);
        thoughtRenderer.enabled = true;
    }

    private void ClearThoughts()
    {
        Thought1.enabled = false;
        Thought2.enabled = false;
        Thought3.enabled = false;
        Thought4.enabled = false;
    }

    private Color SetColourFromOpinion(int opinion)
    {
        if (opinion == 1)
        {
            return new Color(0.2f, 0.7f, 0.2f);
        }
        else if (opinion == -1)
        {
            return new Color(0.7f, 0.2f, 0.2f);
        }
        else if (opinion == 2)
        {
            return new Color(0f, 1f, 0f);
        }
        else if (opinion == -2)
        {
            return new Color(1f, 0f, 0f);
        }
        else
        {
            return Color.white;
        }
    }

}
