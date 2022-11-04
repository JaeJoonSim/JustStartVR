using UnityEngine;
using UnityEngine.UI;


public class TextPad : MonoBehaviour
{
    [SerializeField]
    TextAsset TextAsset;
    [SerializeField]
    Text text;

    private void Start()
    {
        string[] tex = TextAsset.text.Split('$');

        int index = Random.Range(1, tex.Length);

        text.text = tex[index];
    }
}
