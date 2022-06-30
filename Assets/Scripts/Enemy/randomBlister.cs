using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomBlister : MonoBehaviour
{
    public GameObject[] Blister = new GameObject[8];
    int[] index = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
    void Start()
    {
        index = ShuffleArray(index);
        for (int i = 0; i < 8; i++)
        {
            Debug.Log(index[i]);
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
