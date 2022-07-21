using UnityEngine;

public class HintCreator : MonoBehaviour
{

    [SerializeField]private GameObject m_hintObj;
    [SerializeField]private Transform m_Position;

    void Start()
    {
        Instantiate(m_hintObj, m_Position.position, Quaternion.Euler(-90, Random.Range(0, 360), 0),
            this.transform.root.GetChild(0).transform);
    }
}
