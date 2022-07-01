using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    [SerializeField]private RoomCreator roomCreator;
    [SerializeField] private GameObject m_WallOBJ;
    [SerializeField] private GameObject m_ExitOBJ;
    [SerializeField] private bool[,] m_WallisEmpty;

    int m_count;
    bool created;
    private void Start()
    {
        created = false;
        m_count = 100;

        m_WallisEmpty = new bool[roomCreator.m_MaxCount, roomCreator.m_MaxCount];

        for (int _x = 0; _x < roomCreator.m_MaxCount; _x++)
        {
            for (int _z = 0; _z < roomCreator.m_MaxCount; _z++)
            {
                m_WallisEmpty[_x, _z] = true;
            }
        }

        for (int _x = 0; _x < roomCreator.m_MaxCount; _x++)
        {
            for (int _z = 0; _z < roomCreator.m_MaxCount; _z++)
            {
                if (!roomCreator.m_WorldTileisEmpty[_x, _z] && m_WallisEmpty[_x, _z])
                {
                    CreateWall(_x, _z);                  
                }
            }
        }
    }

    private bool CreateExitPoint(int _x, int _z)
    {
        if(created == true || (_x < roomCreator.m_mapinterval && _z < roomCreator.m_mapinterval) || !m_WallisEmpty[_x, _z])
        {
            return false;
        }

        int random = Random.Range(0, m_count);
        if (random != 0)
        {
            m_count -= 1;
            return false;
        }

        Instantiate(m_ExitOBJ, new Vector3(_x * roomCreator.m_TileSize, 0.5f, _z * roomCreator.m_TileSize), Quaternion.identity);
        created = true;
        roomCreator.m_WorldTileisEmpty[_x, _z] = false;
        m_WallisEmpty[_x, _z] = false;

        return true;
    }

    private void CreateWall(int _x, int _z)
    {
        int x;
        int z;
        
        for (int dir = 0; dir < 4; dir++)
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

            if (x > -1 && x < roomCreator.m_MaxCount && z > -1 && z < roomCreator.m_MaxCount)
            {
                if (roomCreator.m_WorldTileisEmpty[x, z] && m_WallisEmpty[x, z])
                {
                    if(!CreateExitPoint(x, z))
                    {
                        Instantiate(m_WallOBJ,
                            new Vector3(x * roomCreator.m_TileSize, 0.5f, z * roomCreator.m_TileSize)
                            , Quaternion.identity);
                        m_WallisEmpty[x, z] = false;
                    }
                }
            }
            else
            {
                    Instantiate(m_WallOBJ,
                        new Vector3(x * roomCreator.m_TileSize, 0.5f, z * roomCreator.m_TileSize)
                        , Quaternion.identity);
            }
        }
    }
}
