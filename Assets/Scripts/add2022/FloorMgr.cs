using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorMgr : MonoBehaviour
{
    //���������� ������Ʈ
    public Elevator elevator;
    //��ư ������Ʈ
    public Text[] ElevatorBtn;
    //��й�ȣ ����[��][����]
    bool[,] Number = new bool[4, 4];

    void Start()
    {
        foreach (var item in ElevatorBtn)
        {
            item.color = Color.black;
        }
        ElevatorBtn[0].color = Color.green;
    }
    public void isGetPassword(int floor, int index)
    {
        if (Number[floor, index]) return;
        Number[floor,index] = true;

        bool check = true;
        for (int i = 0; i < 4; i++)
        {
            if (!Number[floor, i])
                check = false;
        }

        if (check)
        {
            if (floor == 0)
                floor = 1;
            ElevatorBtn[floor+1].color = Color.green;
            elevator.setLocked(floor+1);
        }
    }

        void Update()
    {
        
    }
}
