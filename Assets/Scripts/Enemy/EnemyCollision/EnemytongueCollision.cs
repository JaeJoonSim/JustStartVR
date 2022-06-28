using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemytongueCollision : MonoBehaviour
{
    EnemyBaseFSMMgr FSM;
    void Start()
    {
        FSM = GetComponentInParent<EnemyBaseFSMMgr>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Debug.Log("���ٴ�  �Ѿ� �浹");
            FSM.bulletCollision = true;
        }
        else if (other.gameObject.tag == "Player")
        {
            Debug.Log("���ٴ� �浹");
            FSM.attackCollision = true;
        }
        
    }
}
