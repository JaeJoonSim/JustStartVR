using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowLine : MonoBehaviour
{
    [SerializeField]
    private Transform TargetPos, UIPos;

    private LineRenderer line;
    //private Transform MyPos;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        line.SetPosition(0, UIPos.position);
        line.SetPosition(1, TargetPos.position);
    }
}
