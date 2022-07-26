using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sellCylinder : MonoBehaviour
{
    [SerializeField]
    GameObject SellOBJ, OriginalOBJ;
    GameObject copy;
    bool Destruction = false;

    //���� �׽�Ʈ ��
    //void Start()
    //{
    //    Invoke("Sell", 3f);
    //}

    public void Sell()
    {
        GetComponentInChildren<TubeZombie>().zombieAwake();

        if (Destruction) return;
        Destruction = true;
        Destroy(OriginalOBJ);
        copy = Instantiate(SellOBJ, transform.position, Quaternion.identity, this.transform.parent);
    }

    public void elimination()
    {
        if (copy == null) return;
        Destroy(copy);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet" || other.gameObject.tag == "Melee")
        {
            if (Destruction) return;
            Sell();
        }
        else if (other.gameObject.tag == "EnemyAttack")
        {
            if (other.gameObject.name == "DeshCollider")
            {
                if (Destruction) return;
                Sell();
            }
        }
    }

}
