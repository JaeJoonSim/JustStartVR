using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPassWord : MonoBehaviour
{
    [SerializeField] PassWordManager manager;

    [HideInInspector]
    public int password;
    [HideInInspector]
    public int curIndex;

    [SerializeField]
    PlayerTablet playertablet;

    int floor;

    [SerializeField]
    Text text;

    public void inputPassWord()
    {
        playertablet.inputPassWord(password, curIndex);
    }

    void Start()
    {
        playertablet = GameObject.Find("Player Tablet").GetComponent<PlayerTablet>();
        manager = GameObject.Find("Elevator").GetComponent<PassWordManager>();
        floor = (int)this.transform.position.y / 20;
        password = manager.number[manager.count];
        curIndex = manager.count;

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

        //if(playertablet != null)
        //playertablet.inputPassWord(password, curIndex);

        manager.count++;

        Debug.Log(floor + " " + password);        
    }
}