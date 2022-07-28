using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttackAwake : MonoBehaviour
{
    public GameObject SpownEffect;

    public GameObject Tongue;

    void Start()
    {
        transform.Rotate(new Vector3(0, 1, 0), Random.Range(0, 360));
        GameObject impact = Instantiate(SpownEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), gameObject.transform.rotation) as GameObject;
        Destroy(impact, 5f);
        //Invoke("InstantiateTongue", 1f);
    }

    void InstantiateTongue()
    {
        Tongue.SetActive(true);
    }


    public void DestroyTongue()
    {
        //GameObject impact = Instantiate(SpownEffect, new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), gameObject.transform.rotation) as GameObject;
        //Destroy(impact, 5f);
        Destroy(gameObject, 1f);
    }

}
