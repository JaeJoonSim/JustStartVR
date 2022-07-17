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

    [SerializeField]
    Transform m_Parent;

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
                    GameObject newOBJ = Instantiate(Item[_Random]);
                    newOBJ.transform.parent = m_Parent;
                    newOBJ.transform.position = Point.position;
                }
            }
            SpawnCount--;
        }
    }
}
