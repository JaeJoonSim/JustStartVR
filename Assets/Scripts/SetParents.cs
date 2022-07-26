using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParents : MonoBehaviour
{
    Transform m_parent;

    private void Awake()
    {
        m_parent = transform.parent;
    }

    public void SetParentsNull()
    {
        m_parent = transform.parent;
        transform.parent = null;
    }

    public void SetParentsReturn()
    {
        if(m_parent != null)
            transform.parent = m_parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(m_parent == null && 
            transform.parent == null &&
            collision.collider.tag == "Gun" &&
            collision.collider.tag != "XRRig" && 
            collision.collider.tag != "Player" && 
            collision.collider.tag != "Hand" &&
            collision.collider.tag != "ClipInsert")
        {
            m_parent = collision.transform.parent;
            transform.parent = m_parent;
        }
    }
}
