using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomBlister : MonoBehaviour
{
    public GameObject[] Blister = new GameObject[8];
    int[] index = new int[6] { 0, 1, 2, 3, 4, 5};
    void Start()
    {
        Blister = ShuffleArray(Blister);
        for (int i = 0; i < 3; i++)
        {
            Blister[i].SetActive(true);
        }
    }

    private T[] ShuffleArray<T>(T[] array)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < array.Length; ++i)
        {
            random1 = Random.Range(0, array.Length);
            random2 = Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }
}
