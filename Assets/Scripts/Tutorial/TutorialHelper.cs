using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHelper : MonoBehaviour
{

    Canvas canvas;
    Transform mainCam;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        AssignCamera();
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
