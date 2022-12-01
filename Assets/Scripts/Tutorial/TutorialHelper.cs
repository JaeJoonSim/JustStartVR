
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialHelper : MonoBehaviour
{
    public Text textMesh;
    public Color TextColor, BaseColor;
    public string[] TutorialText;
    bool[] showCount;
    public Transform[] Taget;
    public int Text_Number;
    public float UPPos;
    public Renderer LineColor;
     public DrowLine DrowLine;


    Image BaseImage;
    Canvas canvas;
    Transform mainCam;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        BaseImage = GetComponent<Image>();
        AssignCamera();
        showCount = new bool[TutorialText.Length];

        Next(5);
        textMesh.color = TextColor;
        LineColor.material.color = BaseColor;
        BaseImage.color = BaseColor;
    }

    void Update()
    {
        canvas.transform.LookAt(mainCam);
    }

    public virtual void AssignCamera()
    {
        if (mainCam == null)
        {
            // Find By Tag instead of Camera.main as the camera could be disabled
            if (GameObject.FindGameObjectWithTag("MainCamera") != null)
            {
                mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
            }
        }
    }

    public void Grip(Transform pos)
    {
        Vector3 rialpos = pos.transform.position;
        Debug.Log(rialpos);
        rialpos = new Vector3(rialpos.x, UPPos, rialpos.z);
        DrowLine.TargetPos = pos;
        transform.position = rialpos;
    }

    public void Next(int i)
    {
        //Text_Number++;
        if (showCount[i]) gameObject.SetActive(false);

        showCount[i] = true;
        textMesh.text = TutorialText[i];
    }
}
