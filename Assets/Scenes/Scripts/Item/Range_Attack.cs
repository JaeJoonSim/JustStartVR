using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_Attack : MonoBehaviour
{
    Transform target;

    [SerializeField]
    float scope = 10f;
    void Start()
    {
        
    }
    private void UpdateTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, scope, 1 << 8);

        if (cols.Length > 0)
        {

            for (int i = 0; i < cols.Length; i++)
            {

            }
        }
        else
        {

        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, scope);
    }
}
