using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcDistance : MonoBehaviour
{
    //��ȣ�ۿ��� �Ÿ� 
    public float distance = 10;

    //�÷��̾� ��ǥ
    [HideInInspector]
    public Transform target;

    //���� ���� �ڽ� ������Ʈ
    //�ش������Ʈ �־��ּ���~
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
