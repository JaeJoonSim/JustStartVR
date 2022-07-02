using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player_HP : MonoBehaviour
{
    [SerializeField]
    int Set_HP = 100;
    [HideInInspector]
    public int HP;

    public Image HP_Bar;


    void Start()
    {
        HP = Set_HP;
    }

    void Show_UI()
    {
        HP_Bar.fillAmount = HP / Set_HP;
    }
    public void change_HP(int Val)
    {
        HP += Val;
        Show_UI();
        if (HP < 0) GameOver();

    }
    void GameOver()
    {

    }
}


