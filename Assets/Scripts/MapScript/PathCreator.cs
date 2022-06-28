using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] private RoomCreator roomCreator;
    [SerializeField] private GameObject[,] m_Parents;
    [SerializeField] private GameObject m_DoorOBJ;

    void Start()
    {
        int x = 0;
        int z = 0;
        
        m_Parents = new GameObject[roomCreator.m_RoomCountX, roomCreator.m_RoomCountZ];
        
        bool Xislinked = false;
        bool Zislinked = false;
        
        int index = 0;
        
        for (x = 0; x < roomCreator.m_RoomCountX; x++)
        {
            for (z = 0; z < roomCreator.m_RoomCountZ; z++)
            {
                if(roomCreator.m_GroupOBJ[x, z].transform.childCount > 0)
                {
                    Xislinked = false;
                    Zislinked = false;
        
                    for (int i = 1; i <= 2; i++)
                    {
                        index = z + i; 
                        if (index < roomCreator.m_RoomCountZ)
                        {
                            if (roomCreator.m_GroupOBJ[x, index].transform.childCount > 0 && !Xislinked)
                            {
                                m_Parents[x, z] = new GameObject("Path");
        
                                CreatePathAxisZ(x, index, z, m_Parents[x, z]);
        
        
                                Xislinked = true;
                            }
                        }
        
        
                        index = x + i;
                        if (index < roomCreator.m_RoomCountX)
                        {
                            if (roomCreator.m_GroupOBJ[x + i, z].transform.childCount > 0 && !Zislinked)
                            {
                                m_Parents[x, z] = new GameObject("Path");
        
                                CreatePathAxisX(x, index, z, m_Parents[x, z]);
        
        
                                Zislinked = true;
                            }
                        }                            
                    }
                }
            }
        }
    }

    private void CreatePathAxisZ(int x, int z, int _z, GameObject parent)
    {
        int distance = 0;
        int count = 0;


        distance = z - _z;

        if (distance < 0) distance = -distance;

        if(distance == 1)
        {
            count = 6;
        }
        else if(distance == 2)
        {
            count = 25;
        }

        parent.transform.position =
            new Vector3((x * (roomCreator.m_mapinterval - 1) * roomCreator.m_TileSize) + (roomCreator.m_RoomSize - 1),
            0, (_z * (roomCreator.m_mapinterval - 1) + roomCreator.m_RoomSize) * roomCreator.m_TileSize);

        int xx = x * roomCreator.m_mapinterval + roomCreator.m_RoomSize - 7;
        int zz = _z * (roomCreator.m_mapinterval - 1) + roomCreator.m_RoomSize;

        for (int i = 0; i < count; i++)
        {
            roomCreator.m_WorldTileisEmpty[xx, zz + _z + i] = false;
            roomCreator.AddNewTile((int)(x * roomCreator.m_TileSize),
               (int)(_z + i) * roomCreator.m_TileSize, parent);
        }

        GameObject newDoor = Instantiate(m_DoorOBJ, parent.transform);
        newDoor.transform.localPosition = new Vector3(x * roomCreator.m_TileSize, 0.5f, _z + count / 2 * roomCreator.m_TileSize);
    }

    private void CreatePathAxisX(int _x, int x, int z, GameObject parent)
    {
        int distance = 0;
        int count = 0;


        distance = x - _x;

        if (distance < 0) distance = -distance;

        if (distance == 1)
        {
            count = 6;
        }
        else if (distance == 2)
        {
            count = 25;
        }

        parent.transform.position =
            new Vector3((_x * (roomCreator.m_mapinterval - 1) + roomCreator.m_RoomSize) * roomCreator.m_TileSize,
            0, (z * (roomCreator.m_mapinterval - 1) * roomCreator.m_TileSize) + (roomCreator.m_RoomSize - 1));

        int xx = _x * (roomCreator.m_mapinterval - 1) + roomCreator.m_RoomSize;
        int zz = z * roomCreator.m_mapinterval + roomCreator.m_RoomSize - 7;

        for (int i = 0; i < count; i++)
        {
            roomCreator.m_WorldTileisEmpty[xx + _x + i, zz] = false;
            roomCreator.AddNewTile((int)((_x + i) * roomCreator.m_TileSize),
               (int)(z * roomCreator.m_TileSize), parent);
        }

        GameObject newDoor = Instantiate(m_DoorOBJ, parent.transform);
        newDoor.transform.Rotate(new Vector3(0, 90, 0));
        newDoor.transform.localPosition =
            new Vector3(_x + count / 2  * roomCreator.m_TileSize, 0.5f, z * roomCreator.m_TileSize);
    }
}
