using UnityEngine;

public class FireBottle : MonoBehaviour
{
    [SerializeField]GameObject m_FireEffect;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        Instantiate(m_FireEffect, this.transform.position, this.transform.rotation);
    }
}
