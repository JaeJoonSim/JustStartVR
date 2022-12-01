using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test01 : MonoBehaviour
{

    Rigidbody[] allrig;

    public bool tset;
    void Start()
    {
        allrig = GetComponentsInChildren<Rigidbody>();
        tset = true;
    }

    // Update is called once per frame
    void Update()
    {

            foreach (var item in allrig)
            {
                item.isKinematic = tset;
            }

    }
}
