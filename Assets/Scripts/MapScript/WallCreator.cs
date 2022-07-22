using UnityEngine;

public class WallCreator : MonoBehaviour
{
    [SerializeField]private RoomCreator roomCreator;
    [SerializeField] private GameObject m_WallOBJ;
    [SerializeField] private GameObject m_ExitOBJ;
    [SerializeField] private GameObject m_roomCreatorObj;
    [SerializeField] private Transform m_Parent;
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

        Destroy(m_roomCreatorObj);
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
                if (roomCreator.m_WorldTileisEmpty[x, z])
                {
                    if (m_WallisEmpty[x, z])
                    {
                        GameObject newObj =
                        Instantiate(m_WallOBJ,
                            new Vector3(x * roomCreator.m_TileSize, roomCreator.m_Y + 0.5f, z * roomCreator.m_TileSize)
                            , Quaternion.identity);
                        newObj.transform.parent = m_Parent;
                        m_WallisEmpty[x, z] = false;
                    }
                }
                else if((x == 24 || x == 26 )&& z == 25)
                {
                    GameObject newObj =
                        Instantiate(m_WallOBJ,
                            new Vector3(x * roomCreator.m_TileSize, roomCreator.m_Y + 0.5f, z * roomCreator.m_TileSize)
                            , Quaternion.identity);
                    newObj.transform.parent = m_Parent;
                    m_WallisEmpty[x, z] = false;
                }
            }
            else
            {
                 GameObject newObj = 
                 Instantiate(m_WallOBJ,
                     new Vector3(x * roomCreator.m_TileSize, roomCreator.m_Y + 0.5f, z * roomCreator.m_TileSize)
                     , Quaternion.identity);
                     newObj.transform.parent = m_Parent;
            }
        }
    }
}
