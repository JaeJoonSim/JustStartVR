using UnityEngine;

public class TurnOnLamp : MonoBehaviour
{
    CalcDistance m_calcDistance;

    public void Awake()
    {
        m_calcDistance = this.gameObject.GetComponent<CalcDistance>();
        m_calcDistance.enabled = false;
    }

    public void LampOn(bool onoff)
    {
        m_calcDistance.enabled = onoff;
        this.transform.GetChild(0).gameObject.SetActive(onoff);
    }
}
