using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTongueCollision : MonoBehaviour
{
    float yPos = 0;
    bool tongueDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("destroyTrigger", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (tongueDestroy)
        {
            if (yPos > 0f)
            {
                yPos -= Time.deltaTime ;
            }
            else
            {
                GetComponentInParent<AreaAttackAwake>().DestroyTongue();
            }
        }
        else
        {
            if (yPos < 0.8f)
            {
                yPos += Time.deltaTime*10;
            }
            else
            {
                tongueDestroy = true; 
            }
   
        }
       
        transform.localScale = new Vector3(1 + Mathf.PingPong(Time.time / 2, 0.3f), yPos , 1 + Mathf.PingPong(Time.time / 2, 0.3f));
        
    }
    void destroyTrigger()
    {
        tongueDestroy = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            other.transform.GetComponent<Player_HP>().change_HP(-10);
            GetComponentInParent<AreaAttackAwake>().DestroyTongue();
        }
    }


}
