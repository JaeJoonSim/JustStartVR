using UnityEngine;

public class TurnOnLight : MonoBehaviour
{
    private bool m_isWorking;
    //[SerializeField] private CalcDistance m_calc;

    void Start()
    {
        m_isWorking = false;  
        //m_calc.enabled = m_isWorking;
    }

    public void ClickSwitch()
    {
        m_isWorking = m_isWorking ? true : false;
        //this.gameObject.transform.GetChild(0).gameObject.SetActive(m_isWorking);
        //m_calc.enabled = m_isWorking;
    }
}
