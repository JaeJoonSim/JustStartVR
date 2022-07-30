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

    public bool isDead;


    void Start()
    {
        isDead = false;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject G in gameObjects)
        {
            CharacterController P = G.GetComponent<CharacterController>();

            if (P != null)
            {
                Player = G;
            }
        }
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0, 0, 0);
        RenderSettings.fogDensity = 0.1f;
        HP = Set_HP;
    }

    void GameOver()
    {
        SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.gameOver);
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
        if (isDead == true) return;
        
        HP += Val;
        Show_UI();
        if (HP >= Set_HP)
        {
            HP = Set_HP;
            return;
        }
        if (Val > 0) return;

        CenterEye_UI.Blood_Effect();
        int min = (int)SoundManager.SoundType.playerHit;
        int max = (int)SoundManager.SoundType.playerHit2 + 1;
        int random = Random.Range(min, max);
        SoundManager.m_instance.PlaySound(transform.position, (SoundManager.SoundType)random);

        if (HP <= 0)
        {
            isDead = true;
            GameOver();
        }

    }
}


