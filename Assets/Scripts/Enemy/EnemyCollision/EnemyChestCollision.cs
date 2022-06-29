using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JustStartVR;

public class EnemyChestCollision : MonoBehaviour
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
                damage = 70;
                break;
            case 2:
                damage = 70;
                break;
            case 3:
                damage = 0;
                break;
            case 4:
                damage = 10;
                break;
            default:
                damage = 0;
                break;
        }
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "bullet")
        {
            FSM.Damaged(damage, (transform.position - other.transform.position).normalized);
            //Destroy(other.gameObject);
        }
    }
}
