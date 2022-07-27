using UnityEngine;
using UnityEngine.UI;

public class PlayerTablet : MonoBehaviour
{
    [SerializeField]
    Text showText;

    string passwordText;
    int index;

    void Start()
    {
        initPassWord();
    }

    public void initPassWord()
    {
        passwordText = "****";
        showText.text = passwordText;
    }


    public void inputPassWord(int password, int _index)
    {
        index = _index % 4;

        if(index / 4 < _index / 4)
        {
            initPassWord();
        }

        passwordText = passwordText.Remove(index, 1).Insert(index, password.ToString());
        showText.text = passwordText;
    }
}
