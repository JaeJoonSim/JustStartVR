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
            Debug.Log("Çú¹Ù´Ú  ÃÑ¾Ë Ãæµ¹");
            FSM.bulletCollision = true;
        }
        else if (other.gameObject.tag == "Player")
        {
            Debug.Log("Çú¹Ù´Ú Ãæµ¹");
            FSM.attackCollision = true;
        }
        
    }
}
