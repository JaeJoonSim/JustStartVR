using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSwitch : MonoBehaviour
{
    public bool m_isWorking = false;
    [SerializeField] private TurnOnLamp m_lamp;
    [SerializeField] private ElevatorPowerOn elevator;

    private List<TurnOnLamp> m_lampList = new List<TurnOnLamp>();

    private void Start()
    {
        if(m_lamp != null)
        {
            m_lampList.Add(m_lamp);
        }
    }
    public void AddNewLamp(TurnOnLamp add)
    {
        m_lampList.Add(add);
    }


    public void KickSwitch()
    {
        if (m_isWorking == false)
        {
            m_isWorking = true;

            if(elevator != null)
            {
                elevator.setSpeed(1.0f);
            }

            for(int i = 0; i < m_lampList.Count; i++)
            {                
                m_lampList[i].LampOn(m_isWorking);
            }
        }
    }
}
