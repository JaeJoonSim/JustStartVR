using UnityEngine;

public class PassWordManager : MonoBehaviour
{
    [HideInInspector]
    public int[,] number = new int[4, 4];
    [HideInInspector]
    public int count = 0;

    void Awake()
    {        
        for(int floor = 0; floor < 4; floor ++)
        {
            for(int i = 0; i < 4; i++)
            {
                number[floor, i] = Random.Range(0, 10);
            }
            Debug.Log(number[floor, 0].ToString() +
                number[floor, 1].ToString() +
                number[floor, 2].ToString() +
                number[floor, 3].ToString());
        }
    }
}