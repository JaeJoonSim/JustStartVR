using UnityEngine;

public class FireBottle : MonoBehaviour
{
    [SerializeField]GameObject m_FireEffect;
    [SerializeField] GameObject fireReadyEffect;

    public bool isHand { get; set; }

    private void Start()
    {
        isHand = false;
        fireReadyEffect.SetActive(false);
    }

    private void Update()
    {
        fireReadyEffect.SetActive(isHand);
        if (isHand)
            gameObject.name = "FireBottle";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isHand == false) return;
        Destroy(this.gameObject);
        Instantiate(m_FireEffect, this.transform.position, Quaternion.identity);
        SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.fire);
        SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.motolov);
        SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.Burning);
    }
}
