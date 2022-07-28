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
    [SerializeField]
    GameObject GameOver_UI;

    [HideInInspector]
    public float HP;

    public Image HP_Bar;
    GameObject Player;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0, 0, 0);
        RenderSettings.fogDensity = 0.1f;
        HP = Set_HP;
    }

    void GameOver()
    {
        Player.GetComponent<CharacterController>().enabled = false;
        RenderSettings.fogColor = GameOverColor;
        RenderSettings.fogDensity = 0.7f;
        GameOver_UI.SetActive(true);
    }

    void Show_UI()
    {
        HP_Bar.fillAmount = HP / Set_HP;
    }
    public void change_HP(float Val)
    {
        if (Val < 0) CenterEye_UI.Blood_Effect();

        if (Val < 0)
            SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.playerHit);

        HP += Val;
        Show_UI();
        if (HP < 0) GameOver();
    }
}


