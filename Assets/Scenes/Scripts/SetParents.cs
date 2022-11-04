using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParents : MonoBehaviour
{
    public void SetParentsNull()
    {
        transform.parent = null;
    }

    public void SetParentsReturn()
    {
        if (transform.parent.tag != "ClipInsert" && transform.parent != null)
            transform.parent = null;

            
    }

    public void ActiveGripTrue(GameObject grip)
    {
        if(grip.activeSelf == false)
            grip.SetActive(true);
    }

    public void ActiveGripFalse(GameObject grip)
    {
        if(grip.activeSelf == true)
            grip.SetActive(false);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (m_parent == null &&
    //        transform.parent == null &&
    //        collision.collider.tag != "Gun" &&
    //        collision.collider.tag != "XRRig" &&
    //        collision.collider.tag != "Player" &&
    //        collision.collider.tag != "Hand" &&
    //        collision.collider.tag != "ClipInsert" &&
    //        collision.collider.tag == "Tile")
    //    {
    //        m_parent = collision.transform;
    //        transform.SetParent(m_parent);
    //    }
    //}
}
