using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParents : MonoBehaviour
{
    public void Setparents()
    {
        transform.parent = GameObject.FindWithTag("XRRig").transform;
    }
}
