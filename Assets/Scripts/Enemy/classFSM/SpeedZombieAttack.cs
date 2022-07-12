using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZombieAttack : MonoBehaviour
{
    EnemyBaseFSMMgr FSM;
    private void Start()
    {
        FSM = GetComponentInParent<EnemyBaseFSMMgr>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            //Debug.Log("플레이어 공격 - 데미지 = " + FSM.Status.Atk );
            //플레이어 hp 깎음
            //플레이어의 hp 깍는 함수 호출
            //player_hp -= FSM.Status.Atk;
        }
    }
}
