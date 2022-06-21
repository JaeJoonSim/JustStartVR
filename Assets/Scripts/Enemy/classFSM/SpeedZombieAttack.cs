using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZombieAttack : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("플레이어 공격");
            gameObject.SetActive(false);
            //플레이어 hp 깎음
        }
    }
}
