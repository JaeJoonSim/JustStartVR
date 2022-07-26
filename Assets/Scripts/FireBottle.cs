using UnityEngine;

public class FireBottle : MonoBehaviour
{
    [SerializeField]GameObject m_FireEffect;

    public bool isHand { get; set; }

    private void Start()
    {
        isHand = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isHand == false) return;
        Destroy(this.gameObject);
        Instantiate(m_FireEffect, this.transform.position, Quaternion.identity);
    }
}
