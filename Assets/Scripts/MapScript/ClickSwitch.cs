using UnityEngine;

public class ClickSwitch : MonoBehaviour
{
    public bool m_isWorking = false;
    //[SerializeField] private CalcDistance m_calc;



    void Start()
    {
        
        //m_calc.enabled = m_isWorking;
    }

    public void KickSwitch()
    {
        m_isWorking = m_isWorking ? true : false;
        //this.gameObject.transform.GetChild(0).gameObject.SetActive(m_isWorking);
        //m_calc.enabled = m_isWorking;
    }
}
