using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorMgr : MonoBehaviour
{
    //엘레베이터 오브젝트
    public Elevator elevator;
    //버튼 오브젝트
    public Text[] ElevatorBtn;
    //비밀번호 관리[층][순서]
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
