using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] private RoomCreator roomCreator;
    private GameObject[,] m_Parents;
    [SerializeField] private Transform m_Parent;

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
                                m_Parents[x, z].transform.parent = roomCreator.m_GroupOBJ[x, z].transform;
                                CreatePathAxisZ(x, index, z, m_Parents[x, z]);

                                m_Parents[x, z].AddComponent<DoorCreatorAxisZ>();
                                DoorCreatorAxisZ door = m_Parents[x, z].GetComponent<DoorCreatorAxisZ>();
                                door.m_keyCardRoom = roomCreator.m_keyCardRoom;
                                door.m_curRoomPos = new Vector2(x, z);
                                door.roomCreator = roomCreator;
                                Xislinked = true;
                            }
                        }
        
        
                        index = x + i;
                        if (index < roomCreator.m_RoomCountX)
                        {
                            if (roomCreator.m_GroupOBJ[index, z].transform.childCount > 0 && !Zislinked)
                            {
                                m_Parents[x, z] = new GameObject("Path");
                                m_Parents[x, z].transform.parent = roomCreator.m_GroupOBJ[x, z].transform;

                                CreatePathAxisX(x, index, z, m_Parents[x, z]);
                                m_Parents[x, z].AddComponent<DoorCreatorAxisX>();
                                DoorCreatorAxisX door = m_Parents[x, z].GetComponent<DoorCreatorAxisX>();
                                door.m_keyCardRoom = roomCreator.m_keyCardRoom;
                                door.m_curRoomPos = new Vector2(x, z);
                                door.roomCreator = roomCreator;
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

        int i;
        for (i = 0; i < count; i++)
        {
            roomCreator.m_WorldTileisEmpty[xx, zz + _z + i] = false;
            roomCreator.AddNewTile((int)(x * roomCreator.m_TileSize),
               (int)(_z + i) * roomCreator.m_TileSize, parent);
        }
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

        int i = 0;
        for (i = 0; i < count; i++)
        {
            roomCreator.m_WorldTileisEmpty[xx + _x + i, zz] = false;
            AddNewTile((int)((_x + i) * roomCreator.m_TileSize),
               (int)(z * roomCreator.m_TileSize), parent);
        }
    }

    public void AddNewTile(int x, int z, GameObject parent)
    {
        GameObject newOBJ;
        newOBJ = Instantiate(roomCreator.m_TileOBJ, parent.transform);
        newOBJ.transform.localPosition = new Vector3(x, roomCreator.m_Y, z);
        newOBJ = Instantiate(roomCreator.m_CellingOBJ, parent.transform);
        newOBJ.transform.localPosition = new Vector3(x, roomCreator.m_Y + 4, z);
    }
}
