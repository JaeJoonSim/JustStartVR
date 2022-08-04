using UnityEngine;


public class EnemyCreator : MonoBehaviour
{
    private GameObject[] m_EnemyObj;
    private bool[,] m_TileisEmpty;
    private bool[,] m_Object;
    private Transform m_Parent;
    private int maxCount = 10;
    private RoomCreator m_roomCreator;

    private int RoomCountX;
    private int RoomCountZ;

    int maxSize;

    public void CreateEnemy(int max, bool[,] value, bool[,] value2, Transform parent, RoomCreator room, int _x, int _z)
    {
        m_Parent = parent;
        maxSize = max;
        m_roomCreator = room;

        m_TileisEmpty = new bool[max, max];
        m_Object = new bool[max, max];

        m_TileisEmpty = value;
        m_Object = value2;

        RoomCountX = _x;
        RoomCountZ = _z;


        m_EnemyObj = new GameObject[4];
        m_EnemyObj[0] = Resources.Load<GameObject>("Enemy/zombie_S");
        m_EnemyObj[1] = Resources.Load<GameObject>("Enemy/zombie_L");
        m_EnemyObj[2] = Resources.Load<GameObject>("Enemy/zombie_Blister");
        m_EnemyObj[3] = Resources.Load<GameObject>("Enemy/tongueHole");

        int x = 0;
        int z = 0;

        int count = 0;


        while (count < maxCount)
        {
            x = Random.Range(0, maxSize);
            z = Random.Range(0, maxSize);

            if ((x >= 5 && x <= 7) || (z >= 5 && z <= 7)) continue;

            if (!m_TileisEmpty[x, z] && m_Object[x, z])
            {
                m_Object[x, z] = false;
                Spawn(x, z);
                count++;
            }
        }
    }

    void Spawn(float x, float z)
    {
        GameObject newObj;

        int max = 1 + m_roomCreator.m_Y / 20;
        int random = Random.Range(0, max);

        bool isTongue = false;
        if (random == 3) isTongue = true;

        float angle = Random.Range(0, 360);

        const int mapinterval = 19;

        newObj = Instantiate(m_EnemyObj[random],
            new Vector3((x + mapinterval * RoomCountX) * 2, isTongue ? m_roomCreator.m_Y + 3.468f : m_roomCreator.m_Y + 1f, (z + mapinterval * RoomCountZ) * 2)
            , Quaternion.Euler(isTongue ? 180 : 0, isTongue ? 0 : angle, 0), m_Parent);        
        newObj.transform.SetParent(m_Parent.root.GetChild(0).transform);

    }
}