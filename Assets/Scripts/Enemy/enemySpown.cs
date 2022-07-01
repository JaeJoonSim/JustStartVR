using System.Collections;
using System.Collections.Generic;
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
            Instantiate(enemy[random], gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
