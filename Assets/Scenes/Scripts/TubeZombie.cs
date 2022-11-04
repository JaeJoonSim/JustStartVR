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
       
        anim = GetComponentInParent<Animator>();
        //Invoke("zombieAwake", 3f);

    }

    public void zombieAwake()
    {
        GetComponentInParent<NavMeshObstacle>().enabled = false;
        anim.SetTrigger("Awake");
        Invoke("Spown", 0.5f);
    }

    void Spown()
    {
       
        Destroy(this.gameObject);
        Instantiate(enemy, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }
}
