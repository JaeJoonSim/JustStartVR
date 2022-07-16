using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Holde : MonoBehaviour
{
    public float Distance = 10;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        if(distance> Distance)
        {
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = transform.position;
            Player.GetComponent<CharacterController>().enabled = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, Distance);
    }
}
