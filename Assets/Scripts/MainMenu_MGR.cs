using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_MGR : MonoBehaviour
{
    public string Scene_Name;
    public void Button(string a)
    {
        switch (a)
        {
            case "Start":
                SceneLoader.LoadScene(Scene_Name);
                break;
        }
    }
}
