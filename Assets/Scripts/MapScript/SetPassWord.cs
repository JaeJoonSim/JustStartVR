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
        playertablet.inputPassWord(password, curIndex, floor);
    }

    void Start()
    {
        playertablet = GameObject.Find("Player Tablet").GetComponent<PlayerTablet>();

        manager = GameObject.Find("Elevator 1").GetComponentInChildren<PassWordManager>();

        

        floor = (int)this.transform.position.y / 20;
        password = manager.number[floor, manager.count];
        curIndex = manager.count;



        for(int i = 0; i < 4; i++)
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
                
        if(++manager.count >= 4)
        {
            manager.count = 0;
        }
    }
}