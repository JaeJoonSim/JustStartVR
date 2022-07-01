using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustStartVR
{
    public class UI_Rotation : MonoBehaviour
    {

        Transform mainCam;


        Canvas canvas;
        void Start()
        {
            AssignCamera();
            canvas = GetComponent<Canvas>();
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


        // Update is called once per frame
        void Update()
        {
            if (mainCam == null) return;
            
            //canvas.transform.LookAt(mainCam);
        }
    }
}
