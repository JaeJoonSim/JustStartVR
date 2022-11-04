using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialHelper : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public Color TextColor, BaseColor;
    public string[] TutorialText;
    public int Text_Number;


    public Renderer LineColor;

    Image BaseImage;
    Canvas canvas;
    Transform mainCam;




    void Start()
    {
        canvas = GetComponent<Canvas>();
        BaseImage = GetComponent<Image>();
        AssignCamera();

        Text_Number = 0;
        textMesh.text = TutorialText[0];
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

    public void Next()
    {
        Text_Number++;
        textMesh.text = TutorialText[Text_Number];
    }
}
