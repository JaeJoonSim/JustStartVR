using JustStartVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampOnOffManager : MonoBehaviour
{
    public Keypad keyPadScript;
    public Lamp[] lamp;

    void Start()
    {
       StartCoroutine(LampOnOffStart());
    }

    IEnumerator LampOnOffStart()
    {
        for(int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(1.0f + i * keyPadScript.password[i] * 0.2f);
            for (int j = 0; j < lamp.Length; j++)
            {
                lamp[j].StartOnOff(keyPadScript.password[i] * 2);
            }
        }
    }
}
