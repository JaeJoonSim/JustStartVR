using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPassWord : MonoBehaviour
{
    [SerializeField] PassWordManager manager;

    int password;
    int floor;

    [SerializeField]
    Text text;

    void Start()
    {
        
        manager = GameObject.Find("Elevator").GetComponent<PassWordManager>();
        floor = (int)this.transform.position.y / 20;
        password = manager.number[manager.count];

        int min = floor * 4;
        int max = 4 + min;


        for(int i = min; i < max; i++)
        {
            if(manager.count != i)
            {
                text.text += "*";
            }
            else
            {
                text.text += password.ToString();
            }
        }

        manager.count++;

        Debug.Log(floor + " " + password);        
    }
}