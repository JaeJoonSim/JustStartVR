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
            //Debug.Log("�÷��̾� ���� - ������ = " + FSM.Status.Atk );
            //�÷��̾� hp ����
            //�÷��̾��� hp ��� �Լ� ȣ��
            //player_hp -= FSM.Status.Atk;
        }
    }
}
