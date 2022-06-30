using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_MGR : MonoBehaviour
{
    public void Button(string a)
    {
        switch (a)
        {
            case "Start":
                SceneLoader.LoadScene("VR InGame");
                break;
        }
    }
}
