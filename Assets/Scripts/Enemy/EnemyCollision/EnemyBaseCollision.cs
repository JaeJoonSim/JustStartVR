using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JustStartVR;

public abstract class EnemyBaseCollision : MonoBehaviour
{
    protected EnemyBaseFSMMgr FSM;
    protected EnemyStatus Es;

    protected float damage;

    protected void Start()
    {
        FSM = GetComponentInParent<EnemyBaseFSMMgr>();
        Es = GetComponentInParent<EnemyStatus>();
        setDamage();
    }

    public abstract void setDamage();

    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet")
        {
            FSM.Damaged(other.gameObject.GetComponent<Projectile>().Damage * damage);   
        }
        else if (other.gameObject.tag == "Melee")
        {
            // Debug.Log(other.gameObject.GetComponent<DamageCollider>().Damage);
            if (other.gameObject.GetComponent<DamageCollider>().Cooldown >= 1)
            {
                FSM.Damaged(other.gameObject.GetComponent<DamageCollider>().Set_Damage * damage);
                other.gameObject.GetComponent<DamageCollider>().Cooldown = 0;
            }
           
                   
        }
    }

}
