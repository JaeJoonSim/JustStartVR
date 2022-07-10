using UnityEngine;

public class CheckFloor : MonoBehaviour
{
    [SerializeField] private Transform m_playerTransform;

    private GameObject m_OnOffObject;


    private void Start()
    {
        m_OnOffObject = this.transform.GetChild(0).gameObject;
    }

    bool isSameFloor()
    {
        float y = m_playerTransform.position.y - this.transform.position.y;
        return Mathf.Abs(y) < 3;
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
