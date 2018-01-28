using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOpinion : MonoBehaviour {
    
    public int NeutralValue = 0;
    public int HappyValue = 5;
    public int FriendsValue = 10;
    public int UnhappyValue = -5;
    public int AngryValue = -9;
    public int BlockedValue = -11;

    public Sprite HappySprite;
    public Sprite NeutralSprite;
    public Sprite UnhappySprite;
    public Sprite AngrySprite;

    public Sprite FriendsSprite;
    public Sprite BlockedSprite;

    public SpriteRenderer RelationshipSprite;

    private int opinion;

    private enum States
    {
        Neutral, Happy, Friends, Unhappy, Angry, Blocked
    }
    private States state;

    private void Start()
    {
        UpdateOpinionGraphic();
    }

    public int Opinion
    {
        get { return opinion; }
    }

    public bool IsBlocked()
    {
        return state == States.Blocked;
    }

    public void SetOpinion(int opinion)
    {
        this.opinion = opinion;
        if (opinion > FriendsValue)
        {
            GameStats.Instance.AddFriend(GetComponentInParent<Planet>());
            state = States.Friends;
        }
        else if (opinion > HappyValue)
        {
            if (state == States.Friends)
            {
                GameStats.Instance.RemoveFriend(GetComponentInParent<Planet>());
            }
            state = States.Happy;
        }
        else if (opinion > UnhappyValue)
        {
            state = States.Neutral;
        }
        else if (opinion > AngryValue)
        {
            state = States.Unhappy;
        }
        else if (opinion > BlockedValue)
        {
            state = States.Angry;
        }
        else
        {
            GameStats.Instance.AddBlock(GetComponentInParent<Planet>());
            state = States.Blocked;
            SoundBites.Instance.PlayBlockedSound();
        }
        UpdateOpinionGraphic();
    }

    private void UpdateOpinionGraphic()
    {
        switch (state)
        {
            case States.Neutral:
                RelationshipSprite.sprite = NeutralSprite;
                break;
            case States.Happy:
                RelationshipSprite.sprite = HappySprite;
                break;
            case States.Friends:
                RelationshipSprite.sprite = FriendsSprite;
                break;
            case States.Unhappy:
                RelationshipSprite.sprite = UnhappySprite;
                break;
            case States.Angry:
                RelationshipSprite.sprite = AngrySprite;
                break;
            case States.Blocked:
                RelationshipSprite.sprite = BlockedSprite;
                break;
            default:
                break;
        }
    }
}
