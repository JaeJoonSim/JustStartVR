using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tongueTrap : MonoBehaviour
{
    float yPos = 0;
    float yPosMovement = 0;

    bool isGrab = false;


    public Transform grabPos;
    float attack = 0;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public CharacterController characterController;

    GameObject player;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        characterController = target.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        attack += Time.deltaTime;
        if (!isGrab)
        {
            if (yPos < 0.3f)
            {
                yPos += Time.deltaTime / 5;
            }
            yPosMovement = Mathf.PingPong(Time.time / 10, 0.3f);
        }
        else
        {
            if (attack > 1f)
            {
                attack = 0;
                player.transform.GetComponent<Player_HP>().change_HP(-2f);
            }
            if (yPos > 0.1f)
            {
                yPos -= Time.deltaTime /10;
            }
            if (yPosMovement > 0f)
            {
                yPosMovement -= Time.deltaTime / 10;
            }
            target.transform.position = new Vector3(
                               target.transform.position.x,
                               grabPos.position.y-1.3f,
                               target.transform.position.z
                               );

        }

        transform.localScale = new Vector3(1 + Mathf.PingPong(Time.time / 2, 0.3f), yPos + yPosMovement, 1 + Mathf.PingPong(Time.time / 2, 0.3f));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isGrab)
            {
                player = other.gameObject;
                SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.tongueGrap);
                isGrab = true;
                characterController.enabled = false;
            }
        }

        else if (other.gameObject.tag == "bullet" || other.gameObject.tag == "Melee")
        {
            //Debug.Log("Çú¹Ù´Ú  ÃÑ¾Ë Ãæµ¹");
            isGrab = false;
            characterController.enabled = true;
            gameObject.SetActive(false);
        }
    }
}