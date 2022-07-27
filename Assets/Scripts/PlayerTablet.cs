using UnityEngine;
using UnityEngine.UI;

public class PlayerTablet : MonoBehaviour
{
    [SerializeField]
    Text showText;

    string passwordText;


    void Start()
    {
        initPassWord();
    }

    public void initPassWord()
    {
        passwordText = "asdf";
        showText.text = passwordText;
    }


    public void inputPassWord(int password, int index)
    {
        int _index = index % 4;

        //passwordText = passwordText.Remove(index, 1);
        //passwordText.Insert(index, password.ToString());
        //showText.text = passwordText;
    }
}
