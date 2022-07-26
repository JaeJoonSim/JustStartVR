using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    Light m_Light;

    float m_time;

    void Start()
    {
        m_Light = this.transform.GetComponent<Light>();
        m_Light.intensity = 0;
        m_Light.range = 0;
        m_time = 0.0f;
    }

    void FixedUpdate()
    {
        m_time += Time.deltaTime;

        bool isDying = m_time < 7 ? false : true;

        if(isDying == false)
        {
            if(m_Light.range < 5)
            {
                m_Light.range += Time.deltaTime * 10;
            }
            if(m_Light.intensity < 2)
            {
                m_Light.intensity += Time.deltaTime * 2;
            }

        }
        else
        {
            m_Light.range -= Time.deltaTime * 5;
            m_Light.intensity -= Time.deltaTime * 1;

            if (m_time > 10.0f)
            {
                Destroy(this.transform.parent.gameObject);                
            }
        }
    }
}
