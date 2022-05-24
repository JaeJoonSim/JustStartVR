using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public GameObject CreateOBJ(GameObject obj, int x, int y, int z)
    {
        GameObject tileobj = Instantiate(obj, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
        return tileobj;       
    }
}