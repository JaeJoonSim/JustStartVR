using UnityEngine;

public class Spawn_Item : MonoBehaviour
{
    public int SpawnCount = 1;

    [SerializeField]
    int RadomProbability = 2;
    [SerializeField]
    GameObject[] Item;
    [SerializeField]
    Transform[] Spawn_Point;

    // Start is called before the first frame update
    private void Start()
    {
        if (SpawnCount > 0)
        {
            foreach (Transform Point in Spawn_Point)
            {
                int _Random = Random.Range(0, 2);
                if (_Random == 0)
                {
                    int Count = Item.Length;
                    _Random = Random.Range(0, Count);
                    Instantiate(Item[_Random], Point.position, Quaternion.identity, this.transform.parent);
                }
            }
            SpawnCount--;
        }
    }
}
