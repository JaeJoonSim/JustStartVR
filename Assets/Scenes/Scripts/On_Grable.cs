using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Grable : MonoBehaviour
{
    public int TextNumber;
    bool _Grab = false;
    public void OnGrab()
    {
        if (_Grab) return;
        GameObject.FindGameObjectWithTag("subtitle").GetComponent<subtitle>().ShowText(TextNumber);
        _Grab = true; ;
    }
}
