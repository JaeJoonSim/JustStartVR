using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Flags]
public enum Card
{
    Frist = (1 << 0),     // like as 0x00000001
    Second = (1 << 1),
};

public class KeyPad : MonoBehaviour
{
    public Card GetCard;
    public UnityEvent OnCarde;
    public void TouchCard(Card Card)
    {
        if ((GetCard & Card) != 0)
        {
            Debug.Log("ON_KeyCarde");   
            OnCarde.Invoke();
        }
    }
}
