using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player_HP : MonoBehaviour
{
    [SerializeField]
    float Set_HP = 100;
    [SerializeField]
    Color GameOverColor;
    [SerializeField]
    CenterEye_UI CenterEye_UI;

    [HideInInspector]
    public float HP;

    public Image HP_Bar;

    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0, 0, 0);
        RenderSettings.fogDensity = 0.7f;
        HP = Set_HP;
    }

    void GameOver()
    {
        RenderSettings.fogColor = GameOverColor;
        RenderSettings.fogDensity = 0.7f;
    }

    void Show_UI()
    {
        HP_Bar.fillAmount = HP / Set_HP;
    }
    public void change_HP(float Val)
    {
        if (Val < 0) CenterEye_UI.Blood_Effect();
        HP += Val;
        Show_UI();
        if (HP < 0) GameOver();
    }
}


