using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    private GameObject[] m_Obj;
    private bool[,] m_TileisEmpty;
    public bool[,] m_Object;
    private RoomCreator roomCreator;
    private Transform m_Parent;
    const int markCount = 5;
    private int maxCount = 30 + markCount;
    int count = 0;

    int maxSize;

    int roomType;

    public void initTile(int max, bool[,] value, RoomCreator room, Transform parent, int roomx, int roomy, int _roomType)
    {
        roomType = _roomType;
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

        m_Obj = new GameObject[18];
        m_Obj[0] = Resources.Load<GameObject>("Room/Cabinet");
        m_Obj[1] = Resources.Load<GameObject>("Room/Cabinet2");
        m_Obj[2] = Resources.Load<GameObject>("Room/Shelf");
        m_Obj[3] = Resources.Load<GameObject>("Room/table");
        m_Obj[4] = Resources.Load<GameObject>("Room/tube(withzombie)");
        m_Obj[5] = Resources.Load<GameObject>("Room/blood3");
        m_Obj[6] = Resources.Load<GameObject>("Room/blood4");
        m_Obj[7] = Resources.Load<GameObject>("Room/blood1");
        m_Obj[8] = Resources.Load<GameObject>("Room/blood2");
        m_Obj[9] = Resources.Load<GameObject>("Room/table(withItem)");
        m_Obj[10] = Resources.Load<GameObject>("Room/table(withHint)");
        m_Obj[11] = Resources.Load<GameObject>("Room/Light Control Panel");

        m_Obj[12] = Resources.Load<GameObject>("Room/Mark(bishop)");
        m_Obj[13] = Resources.Load<GameObject>("Room/Mark(horse)");
        m_Obj[14] = Resources.Load<GameObject>("Room/Mark(king)");
        m_Obj[15] = Resources.Load<GameObject>("Room/Mark(queen)");
        m_Obj[16] = Resources.Load<GameObject>("Room/Mark(pawn)");
        m_Obj[17] = Resources.Load<GameObject>("Room/Mark(rook)");

        int x = 0;
        int z = 0;


        int dir = -1;

        while (count < maxCount)
        {
            dir = -1;
            x = Random.Range(0, maxSize);
            z = Random.Range(0, maxSize);

            int _x = x + roomx * roomCreator.m_mapinterval;
            int _z = z + roomy * roomCreator.m_mapinterval;

            if (m_Object[x, z] && !m_TileisEmpty[x, z])
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


    int CountingMark = 0;
    public void CreateObj(float x, float y, float z, int dir, Transform parent)
    {
        float angle = 0;
        int type;


        float _x = parent.transform.position.x / roomCreator.m_mapinterval / roomCreator.m_TileSize;
        float _z = parent.transform.position.z / roomCreator.m_mapinterval / roomCreator.m_TileSize;
        bool isCardRoom = roomCreator.isCardRoom(_x, _z);

        int min = 0;
        int max = 2;


        switch (dir)
        {
            case -1:
                angle = Random.Range(0, 360);
                min = 2;
                max = 9;
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


        if (type == 0 && CountingMark < markCount)
        {
            type = 12 + roomType;
            CountingMark++;
        }

        if (type == 3 && isCardRoom == true)
        {
            type = 9;
        }

        float _y = 0.0f;
        if (roomCreator.m_Panel == false && dir == -1)
        {
            roomCreator.m_Panel = true;
            type = 11;
            _y = 0.5f;
        }

        GameObject newObj;

        max = 4 - roomCreator.m_hintCount;

        int random = Random.Range(0, max);
        if (count == 29 && roomCreator.m_hintCount < 4 && random <= 1)
        {
            type = 10;
            roomCreator.m_hintCount++;
        }

        newObj = Instantiate(m_Obj[type], parent);
        newObj.transform.Rotate(new Vector3(0, angle, 0));

        if(type >= 4 && type <= 7)
        {
            _y = 0.01f;
        }
        newObj.transform.localPosition = new Vector3(x, roomCreator.m_Y + y + _y, z);
        newObj.SetActive(true);

    }
}
