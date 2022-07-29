using UnityEngine;

public class DoorCreatorAxisZ : MonoBehaviour
{
    public Vector2 m_keyCardRoom;
    public Vector2 m_curRoomPos;
    public RoomCreator roomCreator;
    int type;
    void Start()
    {
        type = 0;

        int index = 4;

        if (m_curRoomPos.x == m_keyCardRoom.x && m_curRoomPos.y == m_keyCardRoom.y)
        {
            type = 1;
        }
        else if (m_curRoomPos.x  == m_keyCardRoom.x && m_curRoomPos.y + 1 == m_keyCardRoom.y)
        {
            type = 1;
        }
        else if (m_curRoomPos.x  == m_keyCardRoom.x && m_curRoomPos.y + 2 == m_keyCardRoom.y)
        {
            if(roomCreator.m_GroupOBJ[(int)m_curRoomPos.x, (int)m_curRoomPos.y + 1].transform.childCount == 0)
            {
                type = 1;
                index = 38;

            }
        }

        GameObject tile = this.transform.GetChild(index).gameObject;

        ObjectCreator objcreator = roomCreator.m_objectCreator.GetComponent<ObjectCreator>();

        GameObject newObj = Instantiate(objcreator.m_Obj[18 + type], tile.transform.position + new Vector3(0, 0.5f, -1), Quaternion.identity, this.transform.parent);
    }
}