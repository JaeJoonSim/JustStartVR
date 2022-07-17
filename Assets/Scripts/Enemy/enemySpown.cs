using UnityEngine;

public class enemySpown : MonoBehaviour
{
    public GameObject[] enemy ;
    void Start()
    {
        int SpowRrandom = Random.Range(0, 20);
        if (SpowRrandom == 0)
        {
            int random = Random.Range(0, enemy.Length);
            Instantiate(enemy[random], gameObject.transform.position,
                Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            Destroy(this.gameObject);
        }
        
    }
}
