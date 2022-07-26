using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawn_Item : MonoBehaviour
{
    public int SpawnCount = 1;

    [SerializeField]
    GameObject[] Item;
    [SerializeField]
    List<int> RadomProbability = new List<int>();
    [SerializeField]
    Transform[] Spawn_Point;

    GameObject Player;
    
    const float Set_Distance = 7;

    public virtual int Setitem()
    {
        int Count = Item.Length - RadomProbability.Count;
        if (Count == 0)
        {
            return 0;
        }
        else if (Count > 0)
        {
            for (int i = 0; i < Count; i++)
            {
                RadomProbability.Add(2);
            }
        }
        else
        {
            Count = Mathf.Abs(Count);
            for (int i = 0; i < Count; i++)
            {
                RadomProbability.RemoveAt(RadomProbability.Count - 1);
            }
        }
        return 0;
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Setitem();
    }
    private void Update()
    {
        float Distance = Vector3.Distance(transform.position, Player.transform.position);

        if(Distance< Set_Distance)
        {
            item_spawn();
        }
    }
    void item_spawn()
    {
        if (SpawnCount > 0)
        {
            foreach (Transform Point in Spawn_Point)
            {
                int ItemCount = Item.Length - 1;
                int item = Random.Range(0, ItemCount);
                Debug.Log(item);
                int _Random = Random.Range(0, RadomProbability[item]);

                if (_Random == 0)
                {
                    Instantiate(Item[item], Point.position,
                        Quaternion.identity, this.transform.GetChild(0).transform);
                }
            }
            SpawnCount--;
        }
    }
}



