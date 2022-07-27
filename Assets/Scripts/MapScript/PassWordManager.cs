using UnityEngine;

public class PassWordManager : MonoBehaviour
{

    [SerializeField]
    Elevator elevator;
    [HideInInspector]
    public int[] number = new int[16];
    [HideInInspector]
    public int count = 0;

    void Awake()
    {        
        for (int i = 0; i < 16; i++)
        {
            number[i] = Random.Range(0, 10);
        }
    }
}