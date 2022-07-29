using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Make_Cracks : MonoBehaviour
{
    [SerializeField]
    int PunchCount = 3;
    int Punch = 0;
    [SerializeField]
    GameObject Crack, Crack_parent;
    
    GameObject Player;
    [SerializeField]
    sellCylinder sellCylinder;

    private void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject G in gameObjects)
        {
            CharacterController P = G.GetComponent<CharacterController>();

            if (P != null)
            {
                Player = G;
                Player.GetComponent<CharacterController>().enabled = false;
            }
        }
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
    }


    private void OnTriggerEnter(Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Melee")
        {
            Debug.Log(other);
            Vector3 dir = transform.position - other.transform.position;
            Quaternion rot = Quaternion.LookRotation(dir.normalized);

            rot = Quaternion.Euler(-90, rot.eulerAngles.y, 0);

            Instantiate(Crack, other.transform.position, rot, Crack_parent.transform);
            Punch++;
            SoundManager.m_instance.PlaySound(other.transform.position, SoundManager.SoundType.CrackGlass);
            if (Punch >= PunchCount)
            {
                sellCylinder.Sell();
                Destroy(Crack_parent);
                Player.GetComponent<CharacterController>().enabled = true;
                gameObject.SetActive(false);
            }
        }
    }
}
