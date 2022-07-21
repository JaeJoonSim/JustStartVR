using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSwitch : MonoBehaviour
{
    public bool m_isWorking = false;


    private List<TurnOnLamp> m_lampList = new List<TurnOnLamp>();

    public void AddNewLamp(TurnOnLamp add)
    {
        m_lampList.Add(add);
    }


    public void KickSwitch()
    {
        if (m_isWorking == false)
        {
            m_isWorking = true;
            for(int i = 0; i < m_lampList.Count; i++)
            {
                m_lampList[i].LampOn(m_isWorking);
            }
        }
    }
}
