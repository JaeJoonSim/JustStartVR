using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCarde : MonoBehaviour
{
    public Text Cardname;
    public Card CardType;
    public string Card_Name;

    Material material;

    private void Awake()
    {
        Transform world = this.transform.root;
        material = this.transform.GetComponentInChildren<MeshRenderer>().material;
        float posy = world.position.y;

        switch (posy)
        {
            case 20.0f:
                CardType = Card.First;
                Cardname.text = "First";
                material.color = new Color(255, 255, 255);
                break;
            case 40.0f:
                CardType = Card.Second;
                Cardname.text = "Second";
                material.color = new Color(255 / 2, 255 / 2, 255 / 2);
                break;
            case 60.0f:
                CardType = Card.Third;
                Cardname.text = "Third";
                material.color = new Color(255 / 3, 255 / 3, 255 / 3);
                break;
            case 80.0f:
                CardType = Card.Exit;
                Cardname.text = "Exit";
                material.color = new Color(255 / 4, 255 / 4, 255 / 4);
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
