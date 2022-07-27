using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JustStartVR;

public class Elevator : MonoBehaviour
{
    public GameObject Player;
    [SerializeField]
    Transform Player_YPoint;
    [SerializeField]
    Transform[] Floor_Point;
    [SerializeField]
    Text text, Floor_text;
   
    Animator animator;
    [SerializeField]
    Keypad keypad;

    [SerializeField]
    PassWordManager passwordmgr;

    public int[] password = new int[16];

    bool[] Locked;

    public float Speed;
    public bool Open = false;

    //Vector2 velocity = Vector2.zero;
    int Floor;

    bool its = false;


    private void initKeyCode(int number)
    {
        int min = number * 4;
        int max = min + 4;
        keypad.code = null;
        for (int i = min; i < max; i++)
        {
            keypad.code += password[i].ToString();
        }
    }

    private void Start()
    {   
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        SetPassword();
        initKeyCode(0);
        Locked[0] = true;
    }
    void SetPassword()
    {
        Locked = new bool[Floor_Point.Length];
        for (int i = 0; i< Floor_Point.Length; i++)
        {
            Locked[i] = false;
        }

        for(int i = 0; i < 16; i++)
        {
            password[i] = passwordmgr.number[i];
        }
    }
    public void MoveElevator(int floor)
    {
        if (its) return;
        Floor = floor;
        Floor_text.text = (floor +1).ToString();
        if (Locked[Floor])
        {
            Moving();
            initKeyCode(Floor);
            text.gameObject.SetActive(false);
            Floor_text.color = Color.green;
        }
        else
        {
            Floor_text.color = Color.red;
            text.color = Color.red;
            text.text = "비밀번호를\n입력하세요";
            text.gameObject.SetActive(true);
        }
    }

    public void lift()
    {
        Locked[Floor] = true;
        Floor_text.color = Color.green;
        text.color = Color.green;
        text.text = "해제되었습니다.";
    }

    void Moving()
    {
        its = true;
        Player_YPoint.position = Player.transform.position;
        Player.transform.GetComponent<PlayerGravity>().GravityEnabled = false;
        animator.SetBool("Close", true);
    }

    public void Start_Move()
    {
        SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.ElevatorMove, this.transform);
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
            //Debug.Log(y_Ppint);
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Close", false);
        Player.transform.GetComponent<PlayerGravity>().GravityEnabled = true;
        SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.ElevatorArrive);
        its = false;
    }

}