using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Obj;

    public void CreaetObj(float x, float y, float z, int dir, GameObject parent, int type)
    {
        float angle = 0;
        switch (dir)
        {
            case -1:
                angle = Random.Range(0, 2) * 90;
                break;
            case 0:
                angle = 180;
                z -= 1.3f;
                break;
            case 1:
                angle = 0;
                z += 1.3f;
                break;
            case 2:
                x += 1.3f;
                angle = 90;
                break;
            case 3:
                angle = -90;
                x -= 1.3f;
                break;
        }

        int random = Random.Range(type * 2, 3);
        GameObject newObj;

        if (parent == null)
        {
            newObj = Instantiate(m_Obj[random],
                new Vector3(x, y, z), Quaternion.Euler(0, angle, 0));
        }
        else
        {
            newObj = Instantiate(m_Obj[random], parent.transform);
            newObj.transform.Rotate(new Vector3(0, angle, 0));
            newObj.transform.localPosition = new Vector3(x, y, z);
        }
    }
}
