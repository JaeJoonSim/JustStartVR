using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    FieldOfView fow;
    void Start()
    {
        fow = GetComponentInParent<FieldOfView>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet")
        {
            fow.Damaged(other.gameObject.GetComponent<BulletInfo>().damege, other.transform.forward);
            Destroy(other.gameObject);
        }
    }
}
