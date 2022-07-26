using UnityEngine;

public class CalcDistance : MonoBehaviour
{
    //상호작용할 거리 
    private float distance = 60;

    //플레이어 좌표
    [HideInInspector]
    public Transform target;

    //껏다 켜질 자식 오브젝트
    //해당오브젝트 넣어주세요~
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
