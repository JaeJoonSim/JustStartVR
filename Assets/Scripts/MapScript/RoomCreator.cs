using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomCreator : MonoBehaviour
{
    public GameObject[,] m_GroupOBJ;
    public Vector2 m_keyCardRoom;

    [SerializeField] private Transform playerTransform;

    public GameObject m_Parents;
    public GameObject m_TileOBJ;
    public GameObject m_CellingOBJ;
    public int m_RoomSize;
    public int m_TileCount;
    public int m_RoomCountX;
    public int m_Y;
    public int m_RoomCountZ;
    public int m_TileSize;

    public int m_hintCount = 0;

    public bool[,] m_TileisEmpty;
    public bool[,] m_WorldTileisEmpty;

    private Stack<Tile> m_tileStack = new Stack<Tile>();

    public int m_mapinterval;

    public int m_MaxCount;


    void Start()
    {
        m_TileisEmpty = new bool[m_RoomSize, m_RoomSize];
        m_GroupOBJ = new GameObject[m_RoomCountX, m_RoomCountZ];
        m_mapinterval = m_RoomSize + 6;

        m_MaxCount = m_mapinterval * m_RoomCountX;

        m_WorldTileisEmpty = new bool[m_MaxCount, m_MaxCount];

        m_keyCardRoom.x = -1;
        m_keyCardRoom.y = -1;

        for (int x = 0; x < m_MaxCount; x++)
        {
            for (int z = 0; z < m_MaxCount; z++)
            {
                m_WorldTileisEmpty[x, z] = true;
            }
        }


        initTileEmpty();

        int count = 2;
        int random = 0;
        int random2 = 0;
        int value = 0;
        int roomCount = 10;

        for (int i = 0; i < m_RoomCountX; i++)
        {
            value = 2;
            for(int j = 0; j < m_RoomCountZ; j++)
            {
                random = Random.Range(0, value);
                initTileEmpty();
                roomCount--;

                if (i == 1 && j == 1)
                {
                    CreateRoom(i, j, true);
                    count--;
                    continue;
                }
                if (random == 0 && count > 0)
                {
                    random2 = Random.Range(0, roomCount);

                    if(random2 == 0 && m_keyCardRoom.x == -1)
                    {
                        m_keyCardRoom.x = i;
                        m_keyCardRoom.y = j;
                    }

                    CreateRoom(i, j, true);
                    count--;
                }
                else
                {
                    CreateRoom(i, j, false);
                    value = 0;
                }
            }
            count = m_RoomCountZ - 1;
        }
    }

    private void initTileEmpty()
    {
        for(int i = 0; i < m_RoomSize; i++)
        {
            for (int j = 0; j < m_RoomSize; j++)
            {
                m_TileisEmpty[i, j] = true;
            }
        }
    }

    private void CreateRoom(int x, int z, bool isCreate)
    {
        string name = "Room (" + x + ", " + z + ")";
        if (x == m_keyCardRoom.x && z == m_keyCardRoom.y)
        {
            name = "keyCardRoom (" + x + ", " + z + ")";
        }

        GameObject parent = new GameObject(name);
        parent.transform.parent = m_Parents.transform;

        if (x == m_keyCardRoom.x && z == m_keyCardRoom.y)
        {
            parent.tag = "keycardroom";
        }


        m_GroupOBJ[x, z] = new GameObject(name);
        m_GroupOBJ[x, z].transform.parent = parent.transform;
        m_GroupOBJ[x, z].transform.localPosition =
            new Vector3(x * m_mapinterval * m_TileSize, 0, z * m_mapinterval * m_TileSize);

        if (!isCreate) return;
        TileCreator(m_GroupOBJ[x, z]);
        m_GroupOBJ[x, z].AddComponent<ObjectCreator>();
        m_GroupOBJ[x, z].AddComponent<EnemyCreator>();
        ObjectCreator room = m_GroupOBJ[x, z].GetComponent<ObjectCreator>();
        EnemyCreator enemy = m_GroupOBJ[x, z].GetComponent<EnemyCreator>();
        //parent.AddComponent<CalcDistance>();
        room.initTile(m_RoomSize, m_TileisEmpty, this, m_GroupOBJ[x, z].transform, x, z);
        enemy.CreateEnemy(m_RoomSize, m_TileisEmpty, room.m_Object, m_GroupOBJ[x, z].transform, this);
    }

    private void TileCreator(GameObject parent)
    {
        int CreateX = 0;
        int CreateZ = 0;

        int count = 0;

        int x = (int)parent.transform.localPosition.x / m_TileSize;
        int z = (int)parent.transform.localPosition.z / m_TileSize;

        
        //6, 6
        if(parent.name == "Room (" + 1 + ", " + 1 + ")")
        {
            for(int i = 5; i <= 7; i++)
            {
                for(int j = 5; j <= 7; j++)
                {
                    m_WorldTileisEmpty[i + m_mapinterval, j + m_mapinterval] = false;
                    m_TileisEmpty[i, j] = false;
                    if (i == 6 && j == 6) continue;
                    AddNewTile(i * m_TileSize, j * m_TileSize, parent);
                    count++;
                }
            }
        }

        while (count < m_TileCount)
        {
            if (FindEmpty(CreateX, CreateZ, parent))
            {
                CreateX = m_tileStack.Peek().x;
                CreateZ = m_tileStack.Peek().z;

                count++;
            }
            else
            {
                do
                {
                    Tile lastTile = m_tileStack.Pop();

                    CreateX = lastTile.x;
                    CreateZ = lastTile.z;

                } while (!FindEmpty(CreateX, CreateZ, parent));
                count++;
            }
        }

        int halfSize = m_RoomSize / 2 * m_TileSize;

        int max = m_RoomSize / 2 + 1;

        for (int i = 0; i < max; i++)
        {
            if (m_TileisEmpty[m_RoomSize / 2, i])
            {
                AddNewTile(halfSize, i * m_TileSize, parent);
                m_WorldTileisEmpty[x + m_RoomSize / 2, z + i] = false;
                m_TileisEmpty[m_RoomSize / 2, i] = false;
            }

            if (m_TileisEmpty[i, m_RoomSize / 2])
            {
                AddNewTile(i * m_TileSize, halfSize, parent);
                m_WorldTileisEmpty[x + i, z + m_RoomSize / 2] = false;
                m_TileisEmpty[i, m_RoomSize / 2] = false;
            }
        }

        for (int i = 1; i < max; i++)
        {
            if (m_TileisEmpty[m_RoomSize / 2, m_RoomSize - i])
            {
                AddNewTile(halfSize, (m_RoomSize - i) * m_TileSize, parent);
                m_WorldTileisEmpty[x + m_RoomSize / 2, z + m_RoomSize - i] = false;
                m_TileisEmpty[m_RoomSize / 2, m_RoomSize - i] = false;
            }

            if (m_TileisEmpty[m_RoomSize - i, m_RoomSize / 2])
            {
                AddNewTile((m_RoomSize - i) * m_TileSize, halfSize, parent);
                m_WorldTileisEmpty[x + m_RoomSize - i, z + m_RoomSize / 2] = false;
                m_TileisEmpty[m_RoomSize - i, m_RoomSize / 2] = false;
            }
        }
    }

    private bool FindEmpty(int CreateX, int CreateZ, GameObject parent)
    {
        int count = 0;

        int x = CreateX;
        int z = CreateZ;

        Vector2[] position = new Vector2[4];


        int dir;

        for(dir = 0; dir < 4; dir++)
        {
            x = CreateX;
            z = CreateZ;
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
        
            if (x < 0) x = 0;
            else if (x >= m_RoomSize) x = m_RoomSize - 1;
            if (z < 0) z = 0;
            else if (z >= m_RoomSize) z = m_RoomSize - 1;
        
            if(m_TileisEmpty[x, z])
            {
                position[count++] = new Vector2(x, z);
            }
        }        
        
        if(count > 0)
        {
            int value = Random.Range(0, count);            

            AddNewTile((int)position[value].x * m_TileSize, (int)position[value].y * m_TileSize, parent);

            m_TileisEmpty[(int)position[value].x, (int)position[value].y] = false;

            Tile newTile = new Tile((int)position[value].x, (int)position[value].y);

            m_tileStack.Push(newTile);

            int _x = (int)parent.transform.localPosition.x / m_TileSize + (int)position[value].x;
            int _z = (int)parent.transform.localPosition.z / m_TileSize + (int)position[value].y;
            m_WorldTileisEmpty[_x, _z] = false;
            return true;
        }
        return false;
    }

    public bool isCardRoom(float x, float z)
    {
        return (m_keyCardRoom.x == x && m_keyCardRoom.y == z);
    }

    public void AddNewTile(int x, int z, GameObject parent)
    {
        float _x = parent.transform.position.x /  m_mapinterval / m_TileSize;
        float _z = parent.transform.position.z /  m_mapinterval / m_TileSize;
        GameObject newOBJ;
        newOBJ = Instantiate(m_TileOBJ, parent.transform);
        newOBJ.transform.localPosition = new Vector3(x, m_Y, z);
        newOBJ = Instantiate(m_CellingOBJ, parent.transform);
        newOBJ.transform.localPosition = new Vector3(x, m_Y + 4, z);
    }
}