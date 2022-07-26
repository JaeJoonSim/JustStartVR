using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParents : MonoBehaviour
{
    Transform m_Parent;

    private void Awake()
    {
        m_Parent = transform.parent;
    }

    public void ReturnSetParents()
    {
        if(m_Parent != null)
            transform.parent = m_Parent;
    }

    public void SetparentNull()
    {
        m_Parent = transform.parent;
        transform.parent = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_Parent == null && collision.collider.tag != "XRRig" && collision.collider.tag != "Player" && collision.collider.tag != "Grabbable")
            transform.parent = collision.transform.parent;
    }
}
