using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCarde : MonoBehaviour
{
    public Text Cardname;
    public Card CardType;
    public string Card_Name;

    private void Awake()
    {
        Transform world = this.transform.root;

        float posy = world.position.y;

        switch (posy)
        {
            case 0.0f:
                CardType = Card.First;
                Cardname.text = "First";
                break;
            case 20.0f:
                CardType = Card.Second;
                Cardname.text = "Second";
                break;
            case 40.0f:
                CardType = Card.Third;
                Cardname.text = "Third";
                break;
            case 60.0f:
                CardType = Card.Exit;
                Cardname.text = "Exit";
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if(other.tag == "KeyPade")
        {
            other.GetComponent<KeyPad>().TouchCard(CardType);
        }
    }
}
