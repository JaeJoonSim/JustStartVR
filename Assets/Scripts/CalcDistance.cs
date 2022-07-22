using UnityEngine;

public class CalcDistance : MonoBehaviour
{
    //��ȣ�ۿ��� �Ÿ� 
    private float distance = 30;

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

        if (OnOffObject == null)
        {
            distance = 60;
            OnOffObject = gameObject.transform.GetChild(0).gameObject;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
        return (target.position - OnOffObject.transform.position).magnitude;
    }
}
