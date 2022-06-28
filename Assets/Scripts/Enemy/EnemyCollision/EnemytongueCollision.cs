using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemytongueCollision : MonoBehaviour
{
    EnemyBaseFSMMgr FSM;
    Rigidbody gid;
    void Start()
    {
        FSM = GetComponentInParent<EnemyBaseFSMMgr>();
        gid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (FSM.Status.Hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet")
        {
            //Debug.Log("���ٴ�  �Ѿ� �浹");
            FSM.bulletCollision = true;
            FSM.Damaged(100, (transform.position - other.transform.position).normalized, gid);
        }
        else if (other.gameObject.tag == "Player")
        {
            //Debug.Log("���ٴ� �浹");
            FSM.attackCollision = true;
        }
        
    }
}