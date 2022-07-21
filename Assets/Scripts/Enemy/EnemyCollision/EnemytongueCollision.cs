using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemytongueCollision : MonoBehaviour
{
    BossZombieFSMMgr FSM;

    void Start()
    {
        FSM = GetComponentInParent<BossZombieFSMMgr>();

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
        if (other.gameObject.tag == "bullet" || other.gameObject.tag == "Melee")
        {
            //Debug.Log("���ٴ�  �Ѿ� �浹");
            FSM.bulletCollision = true;
            FSM.Damaged(100, (transform.position - other.transform.position).normalized);
            FSM.characterController.enabled = true;
        }
        else if (other.gameObject.tag == "Player")
        {
            //Debug.Log("���ٴ� �浹");
           
            if (FSM.CurrentState == FSM.Attack2State)
            {
                FSM.attackCollision = true;
                FSM.characterController.enabled = false;
            }
            
        }
        
    }
}
