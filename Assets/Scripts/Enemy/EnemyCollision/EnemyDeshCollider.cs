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
            
            gameObject.SetActive(false);
            FSM.ChangeState(FSM.TraceState);
            FSM.SetAnimator("DeshToMove");
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GameObject impact = Instantiate(DashEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            
            GameObject.Destroy(impact, 1f);
            gameObject.SetActive(false);
            FSM.ChangeState(FSM.StunState);
            FSM.SetAnimator("DeshToStun");
        }
    }
 
}
