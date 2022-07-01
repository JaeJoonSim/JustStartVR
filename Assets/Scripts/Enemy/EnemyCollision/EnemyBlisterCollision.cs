using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlisterCollision : MonoBehaviour
{
    EnemyBaseFSMMgr FSM;
    public GameObject BlisterEffect;
    void Start()
    {
        FSM = GetComponentInParent<EnemyBaseFSMMgr>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            //Debug.Log("¼öÆ÷");
            GameObject impact = Instantiate(BlisterEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            GameObject.Destroy(impact, 1.5f);
            FSM.Damaged(50, (transform.position - other.transform.position).normalized);
            gameObject.SetActive(false);
        }
    }
}
