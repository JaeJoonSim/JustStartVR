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

    bool onoff = true;
    float time = 0.5f;
    public void OffLight()
    {
        SoundManager.m_instance.PlaySound(new Vector3(50, 60, 52), SoundManager.SoundType.neon);

        int blinkCount = 15;
        if (m_lampList.Count == 1) blinkCount = 16;

        for (int i = 0; i < blinkCount; i++)
        {
            time += 0.1f;
            StartCoroutine(blink());
        }
    }

    IEnumerator blink()
    {
        yield return new WaitForSeconds(time);

        
        onoff = !onoff ? true : false;
        for (int i = 0; i < m_lampList.Count; i++)
        {
            m_lampList[i].LampOn(onoff);
        }
    }


    public void KickSwitch()
    {
        if (m_isWorking == false)
        {
            SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.Engine, this.transform.root, true, 50.0f);
            m_isWorking = true;

            if(elevator != null)
            {
                elevator.setSpeed(1.0f);
            }

            OffLight();
        }
    }
}
