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

    public int[,] password = new int[4, 4];

    bool[] Locked;

    public float Speed;
    public bool Open = false, OnZombi = false;

    //Vector2 velocity = Vector2.zero;
    int Floor;

    bool its = false;


  

    public string[] code;
    private void Start()
    {   
        code = new string[Floor_Point.Length];
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
        animator = GetComponent<Animator>();
        SetPassword();

        Locked[0] = true;
        initKeyCode(0);
    }

    private void initKeyCode(int number)
    {
        for (int j = 1; j < Floor_Point.Length; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                code[j] += password[j - 1, i].ToString();
            }
        }
    }
    void SetPassword()
    {
        Locked = new bool[Floor_Point.Length];
        Locked = new bool[Floor_Point.Length];
        for (int i = 0; i< Floor_Point.Length; i++)
        {
            Locked[i] = false;
        }

        for(int floor = 0; floor < 4; floor ++)
        {
            for (int i = 0; i < 4; i++)
            {
                password[floor, i] = passwordmgr.number[floor, i];
            }
        }
    }
    public void MoveElevator(int floor)
    {
        if (its) return;
        if (transform.position == Floor_Point[floor].position)
        {
            SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.CardKeyFailed);
            return;
        }

        Floor = floor;
        Floor_text.text = (floor +1).ToString();

        keypad.code = code[floor];


        if (Locked[Floor])
        {
            Moving();  
        }
        else
        {
            Floor_text.color = Color.red;
            text.color = Color.red;
            text.text = "비밀번호를\n입력하세요";
            text.gameObject.SetActive(true);
            SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.CardKeyFailed);
            GameObject.FindGameObjectWithTag("subtitle").GetComponent<subtitle>().ShowText(6);
        }
    }

    public void lift()
    {
        if (Locked[Floor]) return;
        Locked[Floor] = true;
        Floor_text.color = Color.green;
        text.color = Color.green;
        text.text = "해제되었습니다.";
        MoveElevator(Floor);
    }

    void Moving()
    {
        its = true;
        Player_YPoint.position = Player.transform.position;
        Player.transform.GetComponent<PlayerGravity>().GravityEnabled = false;
        animator.SetBool("Close", true);
        text.gameObject.SetActive(false);
        Floor_text.color = Color.green;
        SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.CardKeySucess);
    }

    public void Start_Move()
    {
        SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.ElevatorMove, this.transform);
        StartCoroutine(Move(Floor));
    }

    public void setLocked(int floorIdx)
    {
        if (Locked[floorIdx]) return;
        Locked[floorIdx] = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Debug.Log("OnZombi" + OnZombi);
            OnZombi = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Debug.Log("OnZombi" + OnZombi);
            OnZombi = false;
        }
    }
    bool floor3 = false;
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

        if (floor3)yield return null;
        if(Floor== 3)
        {
            if(floor3 == false)
            {
                GameObject Obj = GameObject.Find("Light Control Panel(Clone)");
                ClickSwitch clickSwitch = Obj.GetComponentInChildren<ClickSwitch>();
                clickSwitch.OffLight();

                GameObject.FindGameObjectWithTag("subtitle").GetComponent<subtitle>().ShowText(7);
                floor3 = true;
            }
        }
    }
}