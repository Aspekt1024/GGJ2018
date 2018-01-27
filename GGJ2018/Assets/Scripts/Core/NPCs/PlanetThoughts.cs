using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetThoughts : MonoBehaviour {

    public SpriteRenderer Thought1;
    public SpriteRenderer Thought2;
    public SpriteRenderer Thought3;
    public SpriteRenderer Thought4;
    
    public void EnableThoughts()
    {
        gameObject.SetActive(true);
    }

    public void DisableThoughts()
    {
        gameObject.SetActive(false);
    }

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
            return Helpers.GetEmotionalTint(Helpers.Emotions.Like);
        }
        else if (opinion == -1)
        {
            return Helpers.GetEmotionalTint(Helpers.Emotions.Dislike);
        }
        else if (opinion == 2)
        {
            return Helpers.GetEmotionalTint(Helpers.Emotions.StrongLike);
        }
        else if (opinion == -2)
        {
            return Helpers.GetEmotionalTint(Helpers.Emotions.StrongDislike);
        }
        else
        {
            return Helpers.GetEmotionalTint(Helpers.Emotions.Neutral);
        }
    }

}
