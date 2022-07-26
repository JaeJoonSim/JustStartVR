using UnityEngine;

public class CalcDistance : MonoBehaviour
{
    //��ȣ�ۿ��� �Ÿ� 
    private float distance = 60;

    //�÷��̾� ��ǥ
    [HideInInspector]
    public Transform target;

    //���� ���� �ڽ� ������Ʈ
    //�ش������Ʈ �־��ּ���~
    public GameObject OnOffObject;


    // Start is called before the first frame update
    void Start()
    {
        GameObject targetOBJ = GameObject.FindGameObjectWithTag("XRRig").transform.GetChild(0).gameObject;
        target = targetOBJ.transform;

        if (OnOffObject == null)
        {
            OnOffObject = gameObject.transform.GetChild(0).gameObject;
        }
        distance = 100;
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
        Vector2 pos1 = new Vector2(target.position.x, target.position.z);
        Vector2 pos2 = new Vector2(OnOffObject.transform.position.x, OnOffObject.transform.position.z);
        return (pos1 - pos2).magnitude;
    }
}
