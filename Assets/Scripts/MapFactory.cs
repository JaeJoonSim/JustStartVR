using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFactory : MonoBehaviour
{
    [SerializeField] private float m_TileSizeX;
    [SerializeField] private float m_TileSizeZ;
    [SerializeField] private float m_wallHeight;

    [SerializeField] private GameObject[] m_TileOBJ;
    [SerializeField] private GameObject[] m_CeilingOBJ;
    [SerializeField] private GameObject m_WallOBJ;
    [SerializeField] private GameObject m_ParentsOBJ;

    private GameObject[] m_ExitTileOBJ = new GameObject[2];

    private Stack<Tile> m_tileStack = new Stack<Tile>();
    private Queue<Tile> m_tileQueue = new Queue<Tile>();

    private int m_TileCount;

    private const int m_MaxTileCount = 100;
    private const int m_MaxLine = 20;

    private bool[,] m_TileisEmpty = new bool[m_MaxLine, m_MaxLine];

    private float m_distance = 0;

    private Tile m_lastPoint;

    class Tile
    {
        public int x;
        public int z;
        public Tile(int _x, int _z)
        {
            x = _x;
            z = _z;
        }
    }

    void CreateTile(int x, int z, int objindex)
    {
        int type = Random.Range(0, 4);
        Tile newTile = new Tile(x, z);

        Vector3 pos = new Vector3(x * m_TileSizeX, 0, z * m_TileSizeZ);

        GameObject[] tile = new GameObject[2];
        tile[0] = Instantiate(m_TileOBJ[objindex], pos, Quaternion.Euler(0, 0, 0));
        tile[1] = Instantiate(m_TileOBJ[objindex], new Vector3(x * m_TileSizeX, m_wallHeight, z * m_TileSizeZ), Quaternion.Euler(0, 0, 0));

        tile[0].transform.parent = m_ParentsOBJ.transform;
        tile[1].transform.parent = m_ParentsOBJ.transform;

        float dist = Vector3.Distance(Vector3.zero, pos);

        if (m_distance < dist)
        {
            m_distance = dist;
            m_lastPoint = new Tile(x, z);
            m_ExitTileOBJ = tile;
        }

        m_tileStack.Push(newTile);
        m_tileQueue.Enqueue(newTile);

        m_TileisEmpty[x, z] = false;
        m_TileCount++;
    }

    void CreateWall(int _x, int _z, int dir)
    {
        int type = Random.Range(0, 4);
        Tile newTile = new Tile(_x, _z);

        float x = _x * m_TileSizeX;
        float z = _z * m_TileSizeZ;

        float angle = 0;

        switch (dir)
        {
            case 0:
                z += m_TileSizeZ;
                angle = 90;
                break;
            case 1:
                z -= m_TileSizeZ;
                angle = 90;
                break;
            case 2:
                x -= m_TileSizeX;
                break;
            case 3:
                x += m_TileSizeX;
                break;
        }


        GameObject wallOBJ = Instantiate(m_WallOBJ, new Vector3(x, m_wallHeight / 2, z), Quaternion.Euler(0, angle, 0));
        wallOBJ.transform.localScale = new Vector3(m_TileSizeX, m_wallHeight, m_TileSizeZ);
        wallOBJ.transform.parent = m_ParentsOBJ.transform;
    }

    int FindEmpty(int _x, int _z, int createType)
    {
        int dir = 0;

        int[] usedDir = { -1, -1, -1, -1};

        int count = 0;

        int x;
        int z;

        while (count < 4)
        {
            x = _x;
            z = _z;

            if(createType == 0)
            {
                int i = 0;
                while (i < 4)
                {
                    dir = Random.Range(0, 4);
                    if (dir == usedDir[i++]) continue;
                    usedDir[count++] = dir;
                    break;
                }
            }
            else
            {
                dir = count++;                
            }

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

            if (createType == 0)
            {
                if (x < 0) x = 0;
                if (x >= m_MaxLine) x = m_MaxLine - 1;
                if (z < 0) z = 0;
                if (z >= m_MaxLine) z = m_MaxLine - 1;

                if (m_TileisEmpty[x, z])
                {
                    CreateTile(x, z, 0);
                    return count;
                }
            }
            else
            {
                if(x > -1 && x < m_MaxLine && z > -1 && z < m_MaxLine)
                {
                    if(m_TileisEmpty[x, z])
                    CreateWall(_x, _z, dir);
                }
                else
                {
                    CreateWall(_x, _z, dir);
                }
            }
        }

        return count;
    }

    void TileCreateLoop()
    {
        int x = 0;
        int z = 0;

        while (m_TileCount < m_MaxTileCount)
        {
            if(FindEmpty(x, z, 0) < 4)
            {
                x = m_tileStack.Peek().x;
                z = m_tileStack.Peek().z;
                continue;
            }
            else
            {
                while(true)
                {
                    Tile lastTile = m_tileStack.Pop();

                    x = lastTile.x;
                    z = lastTile.z;

                    if (FindEmpty(x, z, 0) < 4)
                    {
                        break;
                    }
                }
            }
        }
    }

    void WallCreateLoop()
    {
        Tile curTile;

        int x;
        int z;

        while(m_tileQueue.Count > 0)
        {
            curTile = m_tileQueue.Dequeue();
            x = curTile.x;
            z = curTile.z;
            FindEmpty(x, z, 1);
        }
    }


    void Start()
    {
        m_TileCount = 0;

        for(int i = 0; i < m_MaxLine; i++)
        {
            for (int j = 0; j < m_MaxLine; j++)
            {
                m_TileisEmpty[i, j] = true;
            }
        }
                
        CreateTile(0, 0, 1);
        TileCreateLoop();
        WallCreateLoop();
        Destroy(m_ExitTileOBJ[0]);
        Destroy(m_ExitTileOBJ[1]);
        CreateTile(m_lastPoint.x, m_lastPoint.z, 1);
    }
}