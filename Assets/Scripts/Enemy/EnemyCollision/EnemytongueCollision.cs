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
        if (FSM.CurrentState == FSM.Attack2State)
        {
            var Bmgr = FSM.CurrentState as BossZombieAttack2State;

            if (other.gameObject.tag != "bullet" && other.gameObject.tag != "Melee")
            {
                SoundManager.m_instance.PlaySound(other.transform.position,
                    SoundManager.SoundType.tongue);
            }


            if (other.gameObject.tag == "bullet" || other.gameObject.tag == "Melee")
            {
                //Debug.Log("Çú¹Ù´Ú  ÃÑ¾Ë Ãæµ¹");
                FSM.bulletCollision = true;
                FSM.Damaged(100, (transform.position - other.transform.position).normalized);
                FSM.characterController.enabled = true;
            }
            else if (other.gameObject.tag == "Player")
            {
                //Debug.Log("Çú¹Ù´Ú Ãæµ¹");
                SoundManager.m_instance.PlaySound(other.transform.position,
                    SoundManager.SoundType.tongueGrap);

                if (FSM.CurrentState == FSM.Attack2State && (FSM.target.position - FSM.grabPos.position).magnitude < 1f)
                {
                    FSM.attackCollision = true;
                    FSM.characterController.enabled = false;
                }

            }
            else
            {

                Bmgr.tongueBack = true;

            }
        }
        
    }
}
