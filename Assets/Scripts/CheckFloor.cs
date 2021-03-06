using UnityEngine;

public class CheckFloor : MonoBehaviour
{
    [SerializeField] private Transform m_playerTransform;

    private GameObject m_OnOffObject;


    private void Start()
    {
        m_OnOffObject = this.transform.GetChild(0).gameObject;
        if(m_playerTransform == null)
        {
            m_playerTransform = GameObject.Find("XR Rig").transform.GetChild(0).gameObject.transform;
        }
    }

    bool isSameFloor()
    {
        float y = m_playerTransform.position.y - this.transform.position.y;
        return Mathf.Abs(y) < 5;
    }

    void FixedUpdate()
    {
        if(isSameFloor())
        {
            if(!m_OnOffObject.activeSelf)
            {
                m_OnOffObject.SetActive(true);
            }
        }
        else
        {
            if (m_OnOffObject.activeSelf)
            {
                m_OnOffObject.SetActive(false);
            }
        }
    }
}
