using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XR_MainMenu_MGR : MonoBehaviour
{
    public Animator Ani;
    public string Scene_Name = "MainMenu";
    bool Active = false;
    public GameObject optionMenu;


    public void ONEnter()
    {
        if (!Active)
        {
            Ani.SetBool("Wrist_Ani", true);
            Active = true;
        }
        else
        {
            Ani.SetBool("Wrist_Ani", false);
            Active = false;
        }

        if(optionMenu.activeSelf == true)
            optionMenu.SetActive(false);

    }

    public void LoadScene(string Scene)
    {
        //Debug.Log("Lode");
        SceneLoader.LoadScene(Scene);
    }

    public void MainMenu()
    {
        //Debug.Log("Lode");
        SceneLoader.LoadScene(Scene_Name);
    }

    public void Setting()
    {
        Debug.Log("Setting");
    }

    public void ShowMenu()
    {
        if (!optionMenu.activeSelf)
            optionMenu.SetActive(true);
        else if (optionMenu.activeSelf)
            optionMenu.SetActive(false);
    }
}
