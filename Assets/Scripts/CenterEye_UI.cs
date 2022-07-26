using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CenterEye_UI : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    Image[] images;
    int sprites_number, images_number, images_Index = 0;

    void Start()
    {
        sprites_number = sprites.Length;
        images_number = images.Length;
    }

    public void Blood_Effect()
    {
        Image_set();
        int Ran = Random.Range(0, sprites_number);
        int x = Random.Range(0, 10);
        int y = Random.Range(0, 5);
        images[images_Index].sprite = sprites[Ran];
        images[images_Index].rectTransform.transform.localPosition = new Vector3(x, y, 0);
        images[images_Index].gameObject.SetActive(true);
        images_Index++;
        if (images_Index >= images_number)
        {
            images_Index = 0;
        }
    }

    void Image_set()
    {
        for(int i = 0; i < images_number; i++)
        {
            images[i].gameObject.SetActive(false);
        }
    }
}
