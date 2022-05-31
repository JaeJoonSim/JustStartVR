using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JustStartVR;

public class EnemyHeadCollision : MonoBehaviour
{

    EnemyBaseFSMMgr FSM;
    EnemyStatus Es;
    Rigidbody gid;
    float damage;
    void Start()
    {
        FSM = GetComponentInParent<EnemyBaseFSMMgr>();
        Es = GetComponentInParent<EnemyStatus>();
        gid = GetComponent<Rigidbody>();
        switch (Es.EnemyType)
        {
            case 1:
                damage = 70;
                break;
            case 2:
                damage = 100;
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
            FSM.Damaged(other.gameObject.GetComponent<Projectile>().Damage,
               other.gameObject.GetComponent<Projectile>().AddRigidForce,
               (transform.position - other.transform.position).normalized, gid);
        }
    }

}
