using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcDistance : MonoBehaviour
{
    //상호작용할 거리 
    public float distance = 10;

    //플레이어 좌표
    [HideInInspector]
    public Transform target;

    //껏다 켜질 자식 오브젝트
    //해당오브젝트 넣어주세요~
    public GameObject OnOffObject;


    // Start is called before the first frame update
    void Start()
    {
        GameObject targetOBJ = GameObject.FindGameObjectWithTag("Player");
        target = targetOBJ.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (CalcTargetDistance() > distance)
        {
            if (OnOffObject.activeSelf)
            {
                OnOffObject.SetActive(false);
            }
        }
        else
        {
            if (!OnOffObject.activeSelf)
            {
                OnOffObject.SetActive(true);
            }

        }
    }
    public float CalcTargetDistance()
    {
        return (target.position - transform.position).magnitude;
    }
}
