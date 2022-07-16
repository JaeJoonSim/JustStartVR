using UnityEngine;

public class HintCreator : MonoBehaviour
{
    private int m_hintCount;

    [SerializeField] private GameObject m_hintOBJ;
    [SerializeField] private RoomCreator m_roomCreator;

    void Start()
    {
        m_hintCount = 0;

        int x;
        int z;

        for(; m_hintCount < 4; m_hintCount++)
        {
            while(true)
            {
                x = Random.Range(m_roomCreator.m_mapinterval, m_roomCreator.m_MaxCount);
                z = Random.Range(m_roomCreator.m_mapinterval, m_roomCreator.m_MaxCount);

                if(!m_roomCreator.m_WorldTileisEmpty[x, z])
                {
                    Instantiate(m_hintOBJ,
                        new Vector3(x * m_roomCreator.m_TileSize, m_roomCreator.m_Y + 0.5f, z * m_roomCreator.m_TileSize),
                        Quaternion.Euler(new Vector3(-90, 0, 0)));
                    break;
                }
            }
        }
    }
}
