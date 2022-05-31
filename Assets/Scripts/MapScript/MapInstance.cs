using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstance : MonoBehaviour
{
    public static MapInstance Instance;

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

    public List<Tile> m_TileList = new List<Tile>();
    public const int m_TileSize = 2;

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }
}
