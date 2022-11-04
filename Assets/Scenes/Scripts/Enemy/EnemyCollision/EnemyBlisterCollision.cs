using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlisterCollision : MonoBehaviour
{
    EnemyBaseFSMMgr FSM;
    public GameObject BlisterEffect;

    private bool isDestroy;

    void Start()
    {
        isDestroy = false;
        FSM = GetComponentInParent<EnemyBaseFSMMgr>();
    }

    void Update()
    {
        if (FSM.Status.Hp <= 0 && !isDestroy)
        {
            BlisterDestroy();
        }
    }

    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet" || other.gameObject.tag == "Melee")
        {
            //Debug.Log("¼öÆ÷");
            BlisterDestroy();
            FSM.Damaged(50);
        }
    }

    private void BlisterDestroy()
    {
        isDestroy = true;
        GameObject impact = Instantiate(BlisterEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        GameObject.Destroy(impact, 1.5f);
        Destroy(gameObject);
    }
}
