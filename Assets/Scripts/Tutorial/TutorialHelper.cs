using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialHelper : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public string TutorialText;
    public Color TextColor, BaseColor;

    public Renderer LineColor;

    Image BaseImage;
    Canvas canvas;
    Transform mainCam;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        BaseImage = GetComponent<Image>();
        AssignCamera();


        textMesh.text = TutorialText;
        textMesh.color = TextColor;

        LineColor.material.color = BaseColor;
        BaseImage.color = BaseColor;
    }

    // Update is called once per frame
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
}
