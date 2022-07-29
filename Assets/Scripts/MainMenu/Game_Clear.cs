using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Clear : MonoBehaviour
{
    [SerializeField]
    Transform Tatget;
    [SerializeField]
    Image Image;
    [SerializeField]
    GameObject Credit;

    public GameObject Player;
    Camera MainCamera;


    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        //Player = GameObject.FindGameObjectWithTag("Player");

        Image.gameObject.SetActive(true);
        Color color = Image.GetComponent<Image>().color;
        color.a += 0f;
        Image.GetComponent<Image>().color = color;


        //MainCamera.backgroundColor = Color.white;

        StartCoroutine(fade());
    }

    IEnumerator fade()
    {
        bool re = true;
        while (re)
        {
            yield return new WaitForSeconds(0.05f);
            Color color = Image.GetComponent<Image>().color;
            color.a += 0.05f;
            Image.GetComponent<Image>().color = color;
            if (color.a >= 1)
            {
                re = false; 
            }
        }

        yield return new WaitForSeconds(0.05f);
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = Tatget.transform.position;

        bool r = true;
        while (r)
        {
            yield return new WaitForSeconds(0.05f);
            Color color = Image.GetComponent<Image>().color;
            color.a -= 0.1f;
            Image.GetComponent<Image>().color = color;
            if (color.a <= 0)
            {
                r = false;
            }

        }

            yield return new WaitForSeconds(0.1f);
        Player.transform.rotation = Tatget.transform.rotation;
        Image.gameObject.SetActive(false);
        Credit.SetActive(true);
        RenderSettings.fogEndDistance = 100f;
    }


}
