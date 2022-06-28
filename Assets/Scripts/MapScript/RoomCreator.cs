using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    public GameObject[,] m_GroupOBJ;

    [SerializeField]private GameObject m_Parents;
    [SerializeField]public GameObject m_TileOBJ;

    public int m_RoomSize;
    public int m_TileCount;
    public int m_RoomCountX;
    public int m_RoomCountZ;
    public int m_TileSize;

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

        for(int x = 0; x < m_MaxCount; x++)
        {
            for (int z = 0; z < m_MaxCount; z++)
            {
                m_WorldTileisEmpty[x, z] = true;
            }
        }


        initTileEmpty();

        CreateRoom(0, 0, true);

        int j = 1;

        int count = 2;
        int random = 0;
        int value = 0;

        for (int i = 0; i < m_RoomCountX; i++)
        {
            value = 2;

            for(; j < m_RoomCountZ; j++)
            {
                random = Random.Range(0, value);
                initTileEmpty();

                if (random == 0 && count > 0)
                {                
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
            j = 0;
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
        m_GroupOBJ[x, z] = new GameObject("Room (" + x + ", " + z + ")");
        m_GroupOBJ[x, z].transform.parent = m_Parents.transform;
        m_GroupOBJ[x, z].transform.localPosition =
            new Vector3(x * m_mapinterval * m_TileSize, 0, z * m_mapinterval * m_TileSize);

        if (!isCreate) return;
        TileCreator(m_GroupOBJ[x, z]);
    }

    private void TileCreator(GameObject parent)
    {
        int CreateX = 0;
        int CreateZ = 0;

        int count = 0;

        int x = (int)parent.transform.localPosition.x / m_TileSize;
        int z = (int)parent.transform.localPosition.z / m_TileSize;


        for (int i = 0; i < 6; i++)
        {
            AddNewTile(m_RoomSize / 2 * m_TileSize, i * m_TileSize, parent);
            m_WorldTileisEmpty[x + m_RoomSize / 2, z + i] = false;

            AddNewTile(i * m_TileSize, m_RoomSize / 2 * m_TileSize, parent);
            m_WorldTileisEmpty[x + i, z + m_RoomSize / 2] = false;
        }

        for (int i = 1; i < 6; i++)
        {
            AddNewTile(m_RoomSize / 2 * m_TileSize, (m_RoomSize - i) * m_TileSize, parent);
            m_WorldTileisEmpty[x + m_RoomSize / 2, z + m_RoomSize - i] = false;
            AddNewTile((m_RoomSize - i) * m_TileSize, m_RoomSize / 2 * m_TileSize, parent);
            m_WorldTileisEmpty[x + m_RoomSize - i, z + m_RoomSize / 2] = false;
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

    public void AddNewTile(int x, int z, GameObject parent)
    {
        GameObject newOBJ = Instantiate(m_TileOBJ, parent.transform);
        newOBJ.transform.localPosition = new Vector3(x, 0, z);
        newOBJ = Instantiate(m_TileOBJ, parent.transform);
        newOBJ.transform.localPosition = new Vector3(x, 3, z);

    }
}