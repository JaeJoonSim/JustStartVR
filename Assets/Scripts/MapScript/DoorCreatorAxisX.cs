using UnityEngine;

public class DoorCreatorAxisX : MonoBehaviour
{
    private GameObject[] m_DoorObj = new GameObject[2];
    public RoomCreator roomCreator;
    public Vector2 m_keyCardRoom;
    public Vector2 m_curRoomPos;
    int type;

    void Start()
    {
        type = 0;

        m_DoorObj[0] = Resources.Load<GameObject>("Room/HingeDoor");
        m_DoorObj[1] = Resources.Load<GameObject>("Room/CardeKey_HingeDoor");


        int index = 4;

        if(m_curRoomPos.x == m_keyCardRoom.x && m_curRoomPos.y == m_keyCardRoom.y)
        {
            type = 1;
        }
        else if (m_curRoomPos.x + 1 == m_keyCardRoom.x && m_curRoomPos.y == m_keyCardRoom.y)
        {
            type = 1;
        }
        else if (m_curRoomPos.x + 2 == m_keyCardRoom.x && m_curRoomPos.y == m_keyCardRoom.y)
        {
            if(roomCreator.m_GroupOBJ[(int)m_curRoomPos.x + 1, (int)m_curRoomPos.y].transform.childCount == 0)
            {
                type = 1;
                index = 38;
            }
        }
        GameObject tile = this.transform.GetChild(index).gameObject;
        GameObject newObj = Instantiate(m_DoorObj[type], tile.transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(new Vector3(0, 90, 0)), this.transform);
    }
}
