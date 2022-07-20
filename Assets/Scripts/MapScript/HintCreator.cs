using UnityEngine;

public class HintCreator : MonoBehaviour
{

    [SerializeField]private GameObject m_hintObj;

    void Start()
    {
        Instantiate(m_hintObj, transform.position, Quaternion.Euler(-90, Random.Range(0, 360), 0), this.transform.parent.parent);
    }
}
