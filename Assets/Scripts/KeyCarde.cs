using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCarde : MonoBehaviour
{
    public Text Cardname;
    public Card SetCard;
    public string Card_Name;
    private void Start()
    {
        Cardname.text = Card_Name;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "KeyPade")
        {
            other.GetComponent<KeyPad>().TouchCard(SetCard);
        }
    }
}
