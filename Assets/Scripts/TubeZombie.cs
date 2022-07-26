using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TubeZombie : MonoBehaviour
{
    public GameObject enemy;

    protected Animator anim;

    void Start()
    {
        GetComponentInParent<NavMeshObstacle>().enabled = false;
        anim = GetComponentInParent<Animator>();
        Invoke("zombieAwake", 3f);

    }

    void zombieAwake()
    {
        anim.SetTrigger("Awake");
        Invoke("Spown", 0.5f);
    }

    void Spown()
    {
       
        Destroy(this.gameObject);
        Instantiate(enemy, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }
}
