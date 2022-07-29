using UnityEngine;
using UnityEngine.UI;

public class PlayerTablet : MonoBehaviour
{
    [SerializeField]
    Text showText;

    string passwordText;
    int index;
    int prevFloor;

    void Start()
    {
        prevFloor = 0;
        initPassWord();
    }

    public void initPassWord()
    {
        passwordText = "****";
        showText.text = passwordText;
    }


    public void inputPassWord(int password, int _index)
    {
        int curFloor = _index / 4;
        
        index = _index % 4;

        if(prevFloor < curFloor)
        {
            initPassWord();
            prevFloor = curFloor;
        }

        passwordText = passwordText.Remove(index, 1).Insert(index, password.ToString());
        showText.text = passwordText;
    }
}
