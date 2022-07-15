using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRegCollision : MonoBehaviour
{
    EnemyBaseFSMMgr FSM;
    EnemyStatus Es;

    float damage;
    void Start()
    {
        FSM = GetComponentInParent<EnemyBaseFSMMgr>();
        Es = GetComponentInParent<EnemyStatus>();

        switch (Es.EnemyType)
        {
            case 1:
                damage = 40;
                break;
            case 2:
                damage = 40;
                break;
            case 3:
                damage = 20;
                break;
            case 4:
                damage = 20;
                break;
            default:
                damage = 0;
                break;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet" || other.gameObject.tag == "Melee")
        {
            FSM.Damaged(damage, (transform.position - other.transform.position).normalized);
            //Destroy(other.gameObject);
        }
    }
}
