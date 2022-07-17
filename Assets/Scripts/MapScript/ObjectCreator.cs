using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    private GameObject[] m_Obj;
    private bool[,] m_TileisEmpty;
    public bool[,] m_Object;
    private RoomCreator roomCreator;
    private Transform m_Parent;
    private int maxCount = 20;

    int maxSize;

    public void initTile(int max, bool[,] value, RoomCreator room, Transform parent)
    {
        roomCreator = room;
        maxSize = max;

        m_Parent = parent;

        m_TileisEmpty = new bool[max, max];
        m_Object = new bool[max, max];

        m_TileisEmpty = value;

        for(int i = 0; i < max; i++)
        {
            for (int j = 0; j < max; j++)
            {
                m_Object[i, j] = true;
            }
        }

        m_Obj = new GameObject[6];
        m_Obj[0] = Resources.Load<GameObject>("Room/Cabinet");
        m_Obj[1] = Resources.Load<GameObject>("Room/Drawer");
        m_Obj[2] = Resources.Load<GameObject>("Room/Shelf");
        m_Obj[3] = Resources.Load<GameObject>("Room/table");
        m_Obj[4] = Resources.Load<GameObject>("Room/tube(withzombie)");
        m_Obj[5] = Resources.Load<GameObject>("Room/tube(withoutzombie)");

        int x = 0;
        int z = 0;

        int count = 0;

        int dir = -1;

        while (count < maxCount)
        {
            dir = -1;
            x = Random.Range(0, maxSize);
            z = Random.Range(0, maxSize);


            if (!m_TileisEmpty[x, z] && m_Object[x, z])
            {
                m_Object[x, z] = false;
                if (!(x % (max / 2) == 0 || z % (max / 2) == 0))
                {
                    dir = checkWall(x, z);
                    CreateObj(x * 2, 0.5f, z * 2, dir, m_Parent);
                    count++;
                }
            }
        }
    }

    private int checkWall(int _x, int _z)
    {
        int dir;

        int x = 0;
        int z = 0;

        for (dir = 0; dir < 4; dir++)
        {
            x = _x;
            z = _z;
            switch (dir)
            {
                case 0:
                    z += 1;
                    break;
                case 1:
                    z -= 1;
                    break;
                case 2:
                    x -= 1;
                    break;
                case 3:
                    x += 1;
                    break;
            }

            if (x < 0 || x >= maxSize)
            {
                return dir;
            }
            if (z < 0 || z >= maxSize)
            {
                return dir;
            }

            if (m_TileisEmpty[x, z])
            {
                return dir;
            }            
        }
        return -1;
    }



    public void CreateObj(float x, float y, float z, int dir, Transform parent)
    {
        float angle = 0;
        int type;


        float _x = parent.transform.position.x / roomCreator.m_mapinterval / roomCreator.m_TileSize;
        float _z = parent.transform.position.z / roomCreator.m_mapinterval / roomCreator.m_TileSize; ;
        bool isCardRoom = roomCreator.isCardRoom(_x, _z);

        int min = 0;
        int max = 2;

        switch (dir)
        {
            case -1:
                angle = Random.Range(0, 360);
                min = 2;
                max = 6;
                if (isCardRoom == true)
                {
                    max = 4;
                }
                break;
            case 0:
                angle = 180;
                z += 0.7f;
                break;
            case 1:
                angle = 0;
                z -= 0.7f;
                break;
            case 2:
                x -= 0.7f;
                angle = 90;
                break;
            case 3:
                angle = -90;
                x += 0.7f;
                break;
        }

        type = Random.Range(min, max);
        GameObject newObj;

        newObj = Instantiate(m_Obj[type], parent);
        newObj.transform.Rotate(new Vector3(0, angle, 0));
        newObj.transform.localPosition = new Vector3(x, roomCreator.m_Y + y, z);
        newObj.SetActive(true);

    }
}
