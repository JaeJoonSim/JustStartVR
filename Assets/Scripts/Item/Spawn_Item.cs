using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Item : MonoBehaviour
{
    [SerializeField]
    int RadomProbability = 2;
    [SerializeField]
    GameObject[] Item;
    [SerializeField]
    Transform[] Spawn_Point;
    // Start is called before the first frame update
    private void OnEnable()
    {
        foreach(Transform Point in Spawn_Point)
        {
            int _Random = Random.Range(0, 2);
            if(_Random == 0)
            {
                int Count = Item.Length;
                _Random = Random.Range(0, Count);
                GameObject newOBJ = Instantiate(Item[_Random]);
                newOBJ.transform.position = Point.position;
            }
        }
    }
}
