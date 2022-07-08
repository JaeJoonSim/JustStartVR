using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    private GameObject[] m_Obj;
    private bool[,] m_TileisEmpty;
    private bool[,] m_Object;
    private RoomCreator roomCreator;
    private Transform m_Parent;

    const int maxCount = 20;

    int maxSize;

    public void initTile(int max, bool[,] value, RoomCreator room)
    {
        roomCreator = room;
        maxSize = max;

        m_Parent = room.m_Parents.transform;

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

        m_Obj = new GameObject[4];
        m_Obj[0] = Resources.Load<GameObject>("Room/Cabinet");
        m_Obj[1] = Resources.Load<GameObject>("Room/Drawer");
        m_Obj[2] = Resources.Load<GameObject>("Room/Shelf");
        m_Obj[3] = Resources.Load<GameObject>("Room/test tube-1");

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
                    CreateObj(x * 2, 0.5f, z * 2, dir, this.gameObject, Random.Range(0, 2));
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



    public void CreateObj(float x, float y, float z, int dir, GameObject parent, int type)
    {
        float angle = 0;
        
        switch (dir)
        {
            case -1:
                if(type == 2)
                angle = Random.Range(0, 2) * 90;
                type = Random.Range(2, 4);
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
                x +=0.7f;
                break;
        }

        GameObject newObj;

        newObj = Instantiate(m_Obj[type], parent.transform);
        newObj.transform.Rotate(new Vector3(0, angle, 0));
        newObj.transform.localPosition = new Vector3(x, roomCreator.m_Y + y, z);
        newObj.transform.parent = m_Parent;
        newObj.SetActive(true);

    }
}
