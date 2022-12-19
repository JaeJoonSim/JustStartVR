using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour
{
    [SerializeField]private Enemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyCollider")
        {
            enemy.ChangeEnemyAct(EnemyAct.Act.STOP);
            enemy.isDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyCollider")
        {
            enemy.ChangeEnemyAct(EnemyAct.Act.MOVE);
            enemy.isDetected = false;
        }
    }
}