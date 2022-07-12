using UnityEngine;

public class DoorCreatorAxisX : MonoBehaviour
{
    private GameObject[] m_DoorObj = new GameObject[2];
    private RoomCreator m_roomCreator;

    void Start()
    {
        m_DoorObj[0] = Resources.Load<GameObject>("Room/HingeDoor");
        m_DoorObj[1] = Resources.Load<GameObject>("Room/Exit");

        GameObject newObj = Instantiate(m_DoorObj[0], this.transform);

        GameObject tile = this.transform.GetChild(4).gameObject;

        newObj.transform.position = tile.transform.position + new Vector3(0, 0.5f, 0);
        newObj.transform.Rotate(0, 90, 0);

    }

    void Update()
    {
        
    }
}
