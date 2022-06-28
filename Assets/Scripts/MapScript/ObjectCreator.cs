using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Obj;

    public void CreaetObj(float x, float y, float z, int dir)
    {
        float angle = 0;
        switch (dir)
        {
            case 0:
                angle = 180;
                z -= 1.2f;
                break;
            case 1:
                angle = 0;
                z += 1.2f;
                break;
            case 2:
                x += 1.2f;
                angle = 90;
                break;
            case 3:
                angle = -90;
                x -= 1.2f;
                break;
        }

        int random = Random.Range(0, 3);
        GameObject newObj = Instantiate(m_Obj[random],
            new Vector3(x, y, z), Quaternion.Euler(0, angle, 0));
    }
}
