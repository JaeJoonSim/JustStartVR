using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZombieAttack : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("�÷��̾� ����");
            gameObject.SetActive(false);
            //�÷��̾� hp ����
        }
    }
}
