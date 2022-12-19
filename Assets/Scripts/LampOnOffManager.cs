using JustStartVR;
using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LampOnOffManager : MonoBehaviour
{
    public Keypad keyPadScript;
    public Lamp[] lamp;

    bool isActive;

    public void ButtonDown()
    {
        if(isActive == false)
        {
            StartCoroutine(LampOnOffStart());
        }
    }

    IEnumerator LampOnOffStart()
    {
        isActive = true;
        for(int i = 1; i < 5; i++)
        {
            yield return new WaitForSeconds(keyPadScript.password[i - 1] * 2 * 0.2f + 1.0f);
            for (int j = 0; j < lamp.Length; j++)
            {
                lamp[j].StartOnOff(keyPadScript.password[i] * 2);
            }
        }
        isActive = false;
    }
}
