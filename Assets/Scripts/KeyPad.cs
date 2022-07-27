using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Card
{
    First ,     
    Second,
    Third ,
    Exit  ,
};

public class KeyPad : MonoBehaviour
{

    public Card CardType;
    public UnityEvent OnCard;
    private void Awake()
    {
        Transform world = this.transform.root;

        float posy = world.position.y;

        switch (posy)
        {
            case 20.0f:
                CardType = Card.First;
                break;
            case 40.0f:
                CardType = Card.Second;
                break;
            case 60.0f:
                CardType = Card.Third;
                break;
            case 80.0f:
                CardType = Card.Exit;
                break;
        }
    } 
    public void TouchCard(Card Card)
    {
        if (CardType == Card)
        {
            SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.CardKeySucess);
            OnCard.Invoke();
        }
        else
        {
            SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.CardKeyFailed);
        }
    }
}
