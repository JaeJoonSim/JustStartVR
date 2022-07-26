using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Clear : MonoBehaviour
{
    [SerializeField]
    GameObject Tatget;

    GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = Tatget.transform.position;
        Player.transform.rotation = Tatget.transform.rotation;

        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(1, 1, 1);
        RenderSettings.fogDensity = 0.1f;
    }


}
