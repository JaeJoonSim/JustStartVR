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
    private void Update()
    {
        if (FSM.CurrentState != FSM.AttackState)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            //Debug.Log("�÷��̾� ���� - ������ = " + FSM.Status.Atk );
            //�÷��̾� hp ����
            //�÷��̾��� hp ��� �Լ� ȣ��
            other.transform.GetComponent<Player_HP>().change_HP(-FSM.Status.Atk);
            //player_hp -= FSM.Status.Atk;
        }
    }
}
