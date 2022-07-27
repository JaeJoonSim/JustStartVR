using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownAreaAttack : MonoBehaviour
{
    public GameObject AreaAttack;
    float t = 0;
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        Invoke("Spown", 1f);
    }
     void Update()
    {
 
        t += Time.deltaTime;
        
        transform.localScale = new Vector3(t, t * 2, t);
    }
    void Spown()
    {
        Instantiate(AreaAttack, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }


}
