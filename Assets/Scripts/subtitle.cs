using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class subtitle : MonoBehaviour
{
    public float exposure_time;
    public Text Subtitle;
    public TextAsset TextAsset;
    string[] _text;


    void Start()
    {
        _text = TextAsset.text.Split('$');
    }

    public void ShowText(int i)
    {
        Subtitle.text = _text[i];
        Subtitle.gameObject.SetActive(true);
        Invoke("OffText", exposure_time);
    }
    void OffText()
    {
        Subtitle.gameObject.SetActive(false);
    }
}
