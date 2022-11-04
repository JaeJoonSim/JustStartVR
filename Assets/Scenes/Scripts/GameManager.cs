using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;

    void Awake()
    {
        if (m_instance == null)
            m_instance = this;
    }

    public void SetQualityLow()
    {
        QualitySettings.SetQualityLevel(0);
    }
    public void SetQualityMedium()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void SetQualityHigh()
    {
        QualitySettings.SetQualityLevel(2);
    }

    public void SetActiveObject()
    {
        if(gameObject.activeSelf == true)
            gameObject.SetActive(false);
        else if (gameObject.activeSelf == false)
            gameObject.SetActive(true);
    }
}
