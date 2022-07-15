using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XR_MainMenu_MGR : MonoBehaviour
{
    public Animator Ani;

    bool Active = false;

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
    }
}
