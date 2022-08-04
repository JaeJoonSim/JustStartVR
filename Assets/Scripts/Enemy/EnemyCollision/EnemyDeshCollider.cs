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

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            other.transform.GetComponent<Player_HP>().change_HP(-FSM.BStatus.DeshAtk);

            GameObject impact = Instantiate(DashEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            Destroy(impact,5f);

            gameObject.SetActive(false);

            FSM.ChangeState(FSM.TraceState);
            FSM.SetAnimator("DeshToMove");
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GameObject impact = Instantiate(DashEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            Destroy(impact, 5f);

            gameObject.SetActive(false);
            FSM.ChangeState(FSM.StunState);
            FSM.SetAnimator("DeshToStun");

            SoundManager.m_instance.PlaySound(this.transform.position, SoundManager.SoundType.BossSkill
            , transform.parent, false, 100.0f);
        }
    } 
}
