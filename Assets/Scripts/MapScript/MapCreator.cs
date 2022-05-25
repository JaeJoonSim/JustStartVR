using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    private int m_RoomNumber;
    private GameObject[,] m_GroupOBJ;
    private GameObject m_newOBJ;

    private List<Tile> m_TileList = new List<Tile>();
    private Stack<Tile> m_tileStack = new Stack<Tile>();

    [SerializeField] private GameObject m_AllTilesParents;
    [SerializeField] private GameObject m_WallParents;
    [SerializeField] private GameObject m_PathParents;

    [SerializeField] private GameObject[] m_TileOBJ;
    [SerializeField] private GameObject[] m_CeilingOBJ;
    [SerializeField] private GameObject m_WallOBJ;
    [SerializeField] private GameObject m_DoorOBJ;

    [SerializeField] private int m_wallHeight;
    [SerializeField] private int m_TileSize;
    [SerializeField] private int m_RoomSize;
    [SerializeField] private int m_RoomCountX;
    [SerializeField] private int m_RoomCountZ;
    [SerializeField] private int m_TileCount;
    [SerializeField] private int m_MapInterval;

    public bool[,] m_TileisEmpty;

    public class Tile
    {
        public int x;
        public int z;
        public bool isRoom;
        public Tile(int _x, int _z, bool room)
        {
            x = _x;
            z = _z;
            isRoom = room;
        }
    }

    private void Start()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        int MaxTileIndex = m_RoomCountX * m_RoomCountZ * (m_RoomSize);
        m_TileisEmpty = new bool[MaxTileIndex, MaxTileIndex];


        m_GroupOBJ = new GameObject[m_RoomCountX, m_RoomCountZ];

        for (int i = 0; i < m_RoomCountX; i++)
        {
            for (int j = 0; j < m_RoomCountZ; j++)
            {
                m_GroupOBJ[i, j] = null;
            }
        }

        for (int i = 0; i < MaxTileIndex; i++)
        {
            for (int j = 0; j < MaxTileIndex; j++)
            {
                m_TileisEmpty[i, j] = true;
            }
        }

        int random = 0;

        for (int i = 0; i < m_RoomCountX; i++)
        {
            for(int j = 0; j < m_RoomCountZ; j++)
            {
                random = Random.Range(0, 6);
                m_GroupOBJ[i, j] = new GameObject("Room (" + i + ", " + j + ")");
                m_GroupOBJ[i, j].transform.parent = m_AllTilesParents.transform;
                m_GroupOBJ[i, j].AddComponent<Room>();
                m_newOBJ = m_GroupOBJ[i, j];
                if (random >= 2 || (i == 0 && j == 0))
                {
                    RoomCreator(i, j);
                }
            }
        }

        PathCreator();
        WallCreator();
    }

    public void WallCreator()
    {
        int count;
        int dir;

        int x, z;

        GameObject newOBJ;

        for(int i = 0; i < m_TileList.Count; i++)
        {
            dir = 0;
            count = 0;
            Tile getTile = m_TileList[i];


            while (count < 4)
            {
                x = getTile.x;
                z = getTile.z;
                dir = count++;

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

                int MaxTileIndex = m_RoomCountX * m_RoomCountZ * (m_RoomSize);
                if (x > -1 && x < MaxTileIndex && z > -1 && z < MaxTileIndex)
                {
                    if (m_TileisEmpty[x, z])
                    {
                        newOBJ = Instantiate(m_WallOBJ, new Vector3(x * m_TileSize, 2, z * m_TileSize),
                        Quaternion.Euler(0, 0, 0), m_WallParents.transform);
                        newOBJ.transform.localScale = new Vector3(m_TileSize, m_wallHeight, m_TileSize);
                    }
                }
                else
                {
                    newOBJ = Instantiate(m_WallOBJ, new Vector3(x * m_TileSize, 2, z * m_TileSize),
                     Quaternion.Euler(0, 0, 0), m_WallParents.transform);
                    newOBJ.transform.localScale = new Vector3(m_TileSize, m_wallHeight, m_TileSize);
                }
            }

        }
    }

    public void PathCreator()
    {
        int index = 0;

        int x = 0;
        int z = 0;

        x = 0;
        for(; x < m_RoomCountX; x++)
        {
            z = 0;
            for (; z < m_RoomCountZ - 1; z++)
            {
                index = z;
                if (m_GroupOBJ[x, z].transform.childCount > 1 && index < m_RoomCountZ - 1)
                {
                    index++;
                    if (m_GroupOBJ[x, index].transform.childCount == 0 && index < m_RoomCountZ - 1)
                    {
                        index++;
                    }

                    if (m_GroupOBJ[x, index].transform.childCount > 1)
                    {
                        CreatePathAxisZ(x, z, x, index);
                    }
                }
            }
        }

        z = 0;
        for (; z < m_RoomCountZ; z++)
        {
            x = 0;
            for (; x < m_RoomCountX - 1; x++)
            {
                index = x;
                if (m_GroupOBJ[x, z].transform.childCount > 1 && index < m_RoomCountX - 1)
                {
                    index++;
                    if (m_GroupOBJ[index, z].transform.childCount == 0 && index < m_RoomCountX - 1)
                    {
                        index++;
                    }

                    if (m_GroupOBJ[index, z].transform.childCount > 1)
                    {
                        CreatePathAxisX(x, z, index, z);
                    }
                }
            }  
        }
    }

    public void CreatePathAxisZ(int StartX, int StartZ, int EndX, int EndZ)
    {
        int dist = 0;
        dist = EndZ - StartZ;

        StartX = (StartX * (m_RoomSize + m_MapInterval)) + m_RoomSize / 2;
        StartZ = (StartZ * (m_RoomSize + m_MapInterval)) + m_RoomSize / 2;

        int Max =  0;

        Max = dist * m_RoomSize;

        int door = 0;

        for (int z = StartZ; z < StartZ + Max + m_MapInterval; z++)
        {
            if(m_TileisEmpty[StartX, z])
            {
                AddNewTile(StartX, z, false);
                Instantiate(m_TileOBJ[0], new Vector3(StartX * m_TileSize, 0, z * m_TileSize),
                    Quaternion.Euler(0,0,0), m_PathParents.transform);
                Instantiate(m_TileOBJ[0], new Vector3(StartX * m_TileSize, m_wallHeight, z * m_TileSize),
                    Quaternion.Euler(0, 0, 0), m_PathParents.transform);                
            }
            else if (z > (StartZ + m_RoomSize / 2))
            {
                int random = Random.Range(0, 3);
                if (random != 0 && door == 0)
                {
                    //Instantiate(m_DoorOBJ, new Vector3(StartX * m_TileSize, 1, z * m_TileSize),
                    //    Quaternion.Euler(0, 0, 0), m_PathParents.transform);
                    Instantiate(m_DoorOBJ, new Vector3(StartX * m_TileSize, 0, (StartZ + m_RoomSize / 2) * m_TileSize),
                        Quaternion.Euler(0, 0, 0), m_PathParents.transform);
                    door++;
                }                
            }
        }
    }

    public void CreatePathAxisX(int StartX, int StartZ, int EndX, int EndZ)
    {
        int dist = 0;
        dist = EndX - StartX;

        StartX = (StartX * (m_RoomSize + m_MapInterval)) + 5;
        StartZ = (StartZ * (m_RoomSize + m_MapInterval)) + 5;

        int Max = 0;

        int door = 0;

        Max = dist * m_RoomSize;

        for (int x = StartX; x < StartX + Max + m_MapInterval; x++)
        {
            if (m_TileisEmpty[x, StartZ])
            {
                AddNewTile(x, StartZ, false);
                Instantiate(m_TileOBJ[0], new Vector3(x * m_TileSize, 0, StartZ * m_TileSize),
                  Quaternion.Euler(0, 0, 0), m_PathParents.transform);
                Instantiate(m_TileOBJ[0], new Vector3(x * m_TileSize, m_wallHeight, StartZ * m_TileSize),
                    Quaternion.Euler(0, 0, 0), m_PathParents.transform);
            }
            else if(x > (StartX + m_RoomSize / 2))
            {
                int random = Random.Range(0, 3);
                if (random != 0 && door == 0)
                {
                    //Instantiate(m_DoorOBJ, new Vector3(x * m_TileSize, 1, StartZ * m_TileSize),
                    //    Quaternion.Euler(0, 0, 0), m_PathParents.transform);
                    Instantiate(m_DoorOBJ, new Vector3((StartX + m_RoomSize / 2) * m_TileSize, 0, StartZ * m_TileSize),
                        Quaternion.Euler(0, 0, 0), m_PathParents.transform);
                }
            }
        }
    }

    public void RoomCreator(int _X, int _Z)
    {
        int StartX = _X * (m_RoomSize + m_MapInterval);
        int StartZ = _Z * (m_RoomSize + m_MapInterval);

        int CreateX = StartX;
        int CreateZ = StartZ;

        int count = 0;

        while(count < m_TileCount)
        {
            if (FindEmpty(StartX, StartZ, CreateX, CreateZ) < 4)
            {
                CreateX = m_tileStack.Peek().x;
                CreateZ = m_tileStack.Peek().z;

                count++;

                continue;
            }
            else
            {
                do
                {
                    Tile lastTile = m_tileStack.Pop();

                    CreateX = lastTile.x;
                    CreateZ = lastTile.z;

                } while (FindEmpty(StartX, StartZ, CreateX, CreateZ) >= 4);
                count++;
            }
        }               
    }

    public void AddNewTile(int x, int z, bool room)
    {
        m_TileisEmpty[x, z] = false;
        Tile createdTile = new Tile(x, z, room);
        m_tileStack.Push(createdTile);
        m_TileList.Add(createdTile);        
    }

    public int FindEmpty(int StartX, int StartZ, int CreateX, int CreateZ)
    {
        int count = 0;

        int x = 0;
        int z = 0;

        int dir;

        while (count < 4)
        {
            x = CreateX;
            z = CreateZ;

            dir = Random.Range(0, 4);

            count++;

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

            int MaxX = StartX + m_RoomSize;
            int MaxZ = StartZ + m_RoomSize;
            if (x < StartX) x = StartX;
            else if (x >= MaxX) x = MaxX - 1;
            if (z < StartZ) z = StartZ;
            else if (z >= MaxZ) z = MaxZ - 1;

            if (m_TileisEmpty[x, z])
            {   
                Instantiate(m_TileOBJ[0], new Vector3(x * m_TileSize, 0, z * m_TileSize),
                    Quaternion.Euler(0,0,0), m_newOBJ.transform);
                Instantiate(m_TileOBJ[0], new Vector3(x * m_TileSize, m_wallHeight, z * m_TileSize),
                                    Quaternion.Euler(0, 0, 0), m_newOBJ.transform);

                AddNewTile(x, z, true);
                
                return count;
            }
        }
        return count;
    }
}