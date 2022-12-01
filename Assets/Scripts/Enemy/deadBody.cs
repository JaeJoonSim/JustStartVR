using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadBody : MonoBehaviour
{
    void Start()
    {
        this.gameObject.transform.rotation = Random.rotation;
    }
}
