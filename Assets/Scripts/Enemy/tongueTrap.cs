using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tongueTrap : MonoBehaviour
{
    float yPos = 0;
    float yPosMovement = 0;

    bool isGrab = false;

    public Transform grabPos;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public CharacterController characterController;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        characterController = target.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
            isGrab = true;
            characterController.enabled = false;
        }

        else if (other.gameObject.tag == "bullet" || other.gameObject.tag == "Melee")
        {
            //Debug.Log("���ٴ�  �Ѿ� �浹");
            isGrab = false;
            characterController.enabled = true;
            gameObject.SetActive(false);
        }
    }
}