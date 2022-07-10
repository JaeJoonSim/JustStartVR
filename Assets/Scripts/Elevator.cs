using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject Player;
    [SerializeField]
    Transform Player_YPoint;
    [SerializeField]
    Transform[] Floor_Point;

    public float smoothTimeY;

    Vector2 velocity = Vector2.zero;

    bool its = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void MoveElevator(int floor)
    {
        if (its) return;
        Player_YPoint.position = Player.transform.position;
        its = true;
        StartCoroutine(Move(floor));
    }
    IEnumerator Move(int floor) 
    {
        bool y_Ppint = true;
        while (y_Ppint)
        {
            float posY = Mathf.SmoothDamp(transform.position.y, Floor_Point[floor].transform.position.y, ref velocity.y, smoothTimeY);
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);

            Player.transform.position = new Vector3(Player.transform.position.x, Player_YPoint.transform.position.y, Player.transform.position.z);
            float dist = Vector3.Distance(transform.position, Floor_Point[floor].transform.position);
            y_Ppint = dist > 0.01f;
            //y_Ppint = !(transform.position.y< Floor_Point[floor].transform.position.y+0.5f && transform.position.y > Floor_Point[floor].transform.position.y - 0.5f);
            Debug.Log(y_Ppint);
            yield return null;
        }
        its = false;
    }

}
