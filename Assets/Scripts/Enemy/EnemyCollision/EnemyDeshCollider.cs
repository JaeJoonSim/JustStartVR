using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeshCollider : MonoBehaviour
{
    BossZombieFSMMgr FSM;
    public GameObject DashEffect;
    void Start()
    {
        FSM = GetComponentInParent<BossZombieFSMMgr>();
    }
    private void Update()
    {
        if (FSM.CurrentState != FSM.DeshState)
        {
            gameObject.SetActive(false);
        }
      
    }

    private void OnCollisionEnter(Collision other)
    { 

        if (other.gameObject.tag == "Player")
        {
            other.transform.GetComponent<Player_HP>().change_HP(-FSM.BStatus.DeshAtk);
            Collision();
            FSM.ChangeState(FSM.TraceState);
            FSM.SetAnimator("DeshToMove");
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Collision();
            FSM.ChangeState(FSM.StunState);
            FSM.SetAnimator("DeshToStun");

            
        }
    } 
    private void Collision()
    {
        GameObject impact = Instantiate(DashEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        Destroy(impact, 5f);
        this.gameObject.SetActive(false);
        SoundManager.m_instance.PlaySound(this.transform.position, SoundManager.SoundType.BossSkill
            , transform.parent, false, 100.0f);
    }
}
