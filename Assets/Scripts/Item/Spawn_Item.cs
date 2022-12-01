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


    bool spawn;

    GameObject Player;
    
    const float Set_Distance = 1000;


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
        //Player = GameObject.FindGameObjectWithTag("Player");


        //spawn = gameObject.name == "table(withitem)" ? true : false;
        //if (gameObject.name == "table(withitem)")
        //{
        //    spawn = true;
        //
        //}
        //else
        //    spawn = false;

        Setitem();
    }
    private void Update()
    {
        //float Distance = Vector3.Distance(transform.position, Player.transform.position);

        if(true)
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
                int ItemCount = Item.Length;
                int item = Random.Range(0, ItemCount);
                //Debug.Log(item);
                int _Random = Random.Range(0, RadomProbability[item]);

                if (_Random == 0 || spawn == true)
                {
                    if (item == 3 && Point.position.y < 39) continue;
                    Instantiate(Item[item], Point.position,
                        Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)),
                        this.transform.parent);
                }
            }
            SpawnCount--;
        }
    }
}



