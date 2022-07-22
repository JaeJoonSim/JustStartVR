using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JustStartVR;

public class Elevator : MonoBehaviour
{
    public GameObject Player;
    [SerializeField]
    Transform Player_YPoint;
    [SerializeField]
    Transform[] Floor_Point;

    Animator animator;

    public float Speed;
    public bool Open = false;

    //Vector2 velocity = Vector2.zero;
    int Floor;

    bool its = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }
    public void MoveElevator(int floor)
    {
        if (its) return;
        Floor = floor;
        its = true;
        Player_YPoint.position = Player.transform.position;
        Player.transform.GetComponent<PlayerGravity>().GravityEnabled = false;
        animator.SetBool("Close", true);
    }

    public void Start_Move()
    {
        StartCoroutine(Move(Floor));
    }

    IEnumerator Move(int floor)
    {
        bool y_Ppint = true;
        while (y_Ppint)
        {
            //float posY = Mathf.SmoothDamp(transform.position.y, Floor_Point[floor].transform.position.y, ref velocity.y, smoothTimeY);
            transform.position =  Vector3.MoveTowards(transform.position, Floor_Point[floor].transform.position, Speed * Time.deltaTime);

            Player.transform.position = new Vector3(Player.transform.position.x, Player_YPoint.transform.position.y, Player.transform.position.z);

            float dist = Vector3.Distance(transform.position, Floor_Point[floor].transform.position);
            y_Ppint = dist > 0.01f;
            //y_Ppint = !(transform.position.y < Floor_Point[floor].transform.position.y + 0.5f && transform.position.y > Floor_Point[floor].transform.position.y - 0.5f);
            Debug.Log(y_Ppint);
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Close", false);
        Player.transform.GetComponent<PlayerGravity>().GravityEnabled = true;
        its = false;
    }

}