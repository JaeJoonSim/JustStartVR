using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] m_EnemyObj;

    public void init()
    {
        int count = MapInstance.Instance.m_TileList.Count;

        List<MapInstance.Tile> TileList = new List<MapInstance.Tile>();
        TileList = MapInstance.Instance.m_TileList;

        int random = 0;
        int type = 0;

        for (int i = 0; i < count; i++)
        {
            random = Random.Range(0, 6);
            type = Random.Range(0, 2);
            if(random == 0)
            CreateEnemy(TileList[i].x * 2, TileList[i].z * 2, type);
        }
    }

    public void CreateEnemy(int x, int z, int type)
    {
        float angle = Random.Range(0, 360);
        Instantiate(m_EnemyObj[type], new Vector3(x, 1, z), Quaternion.Euler(0, angle, 0));
    }
}
