using UnityEngine;
using UnityEngine.UI;

public class PlayerTablet : MonoBehaviour
{
    public FloorMgr fMgr;

    [SerializeField]
    Text showText;

    string passwordText;
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


    public void inputPassWord(int password, int _index, int floor)
    {
        fMgr.isGetPassword(floor, _index);

        int curFloor = floor;

        if(prevFloor < curFloor)
        {
            initPassWord();
            prevFloor = curFloor;
        }

        passwordText = passwordText.Remove(_index, 1).Insert(_index, password.ToString());
        showText.text = passwordText;
    }
}
