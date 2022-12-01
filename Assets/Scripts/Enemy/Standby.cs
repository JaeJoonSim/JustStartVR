using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standby : MonoBehaviour
{
    [HideInInspector]
    public GameObject zombie = null;

    private Transform target;

    private void Awake()
    {
        //zombie = Resources.Load<GameObject>("Enemy/zombie_S");
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject G in gameObjects)
        {
            CharacterController P = G.GetComponent<CharacterController>();
            if (P != null)
            {
                target = G.transform;
            }
            
        }
    }

    void Update()
    {
        if (zombie != null)
        {
            if (Vector3.Distance(transform.position, target.position) < 30f)
            {
                Instantiate(zombie, transform.position, transform.rotation, gameObject.transform.parent);
                Destroy(gameObject);
            }
        }
       
    }
}
