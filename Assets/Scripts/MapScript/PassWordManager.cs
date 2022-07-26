using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassWordManager : MonoBehaviour
{
    [HideInInspector]
    public int[] number = new int[12];
    [HideInInspector]
    public int count = 0;

    void Start()
    {        
        for (int i = 0; i < 12; i++)
        {
            number[i] = Random.Range(0, 10);
        }
    }
}