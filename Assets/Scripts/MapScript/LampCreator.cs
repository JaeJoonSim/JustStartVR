using UnityEngine;

public class LampCreator : MonoBehaviour
{
    [SerializeField] private RoomCreator m_roomCreator;
    [SerializeField] private GameObject m_LampOBJ;
    [SerializeField] private Transform m_Parent;
    [SerializeField] private ClickSwitch m_clickSwitch;

    void Start()
    {
        GameObject newObj = null;

        if(m_roomCreator.m_Y == 40)
        {
            m_clickSwitch = GameObject.FindGameObjectWithTag("LightPanel").GetComponent<ClickSwitch>();
        }

        for(int i = 0; i < m_roomCreator.m_MaxCount; i++)
        {
            for (int j = 0; j < m_roomCreator.m_MaxCount; j++)
            {
                if(!m_roomCreator.m_WorldTileisEmpty[i, j])
                {
                    if (i % 5 == 0 && j % 6 == 0)
                    {
                        newObj =
                        Instantiate(m_LampOBJ,
                            new Vector3(i * m_roomCreator.m_TileSize, m_roomCreator.m_Y + 3.51f, j * m_roomCreator.m_TileSize),
                            Quaternion.identity);
                        newObj.transform.parent = m_Parent;

                        if(m_roomCreator.m_Y == 40)
                        {
                            m_clickSwitch.AddNewLamp(newObj.GetComponent<TurnOnLamp>());
                        }
                    }
                }
            }
        }
    }
}
