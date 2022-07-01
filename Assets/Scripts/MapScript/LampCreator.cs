using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampCreator : MonoBehaviour
{
    [SerializeField] private GameObject m_LampObj;

    public void init()
    {
        int count = MapInstance.Instance.m_TileList.Count;
        int random = 0;

        float x = 0;
        float z = 0;

        for (int i = 0; i < count; i++)
        {
            random = Random.Range(0, 40);
            if(random == 0)
            {
                x = MapInstance.Instance.m_TileList[i].x;
                z = MapInstance.Instance.m_TileList[i].z;
                Instantiate(m_LampObj, new Vector3(x * 2, 4.5f, z * 2), Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
