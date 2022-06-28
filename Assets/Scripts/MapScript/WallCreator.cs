using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    [SerializeField]private RoomCreator roomCreator;
    [SerializeField]private ObjectCreator objCreator;
    [SerializeField] private GameObject m_WallOBJ;

    private void Start()
    {
        int count = roomCreator.m_MaxCount;

        int dir = 0;

        int x = 0;
        int z = 0;

        for (int xx = 0; xx < count;  xx++)
        {
            for(int zz = 0; zz < count; zz++)
            {
                if (!roomCreator.m_WorldTileisEmpty[xx, zz])
                {
                    for (dir = 0; dir < 4; dir++)
                    {
                        x = xx;
                        z = zz;

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

                        if (x > -1 && x < count && z > -1 && z < count)
                        {
                            if (roomCreator.m_WorldTileisEmpty[x, z])
                            {
                                Instantiate(m_WallOBJ, new Vector3(x * roomCreator.m_TileSize, 0, z * roomCreator.m_TileSize)
                                    , Quaternion.identity);
                                objCreator.CreaetObj(x * roomCreator.m_TileSize, 0.5f, z * roomCreator.m_TileSize, dir);
                            }
                        }
                        else
                        {
                            Instantiate(m_WallOBJ, new Vector3(x * roomCreator.m_TileSize, 0, z * roomCreator.m_TileSize)
                                , Quaternion.identity);
                                objCreator.CreaetObj(x * roomCreator.m_TileSize, 0.5f, z * roomCreator.m_TileSize, dir);
                        }
                    }
                }  
            }           
        }
    }
}
