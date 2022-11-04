using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet_Looking : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Door")
        {
            other.GetComponent<HingeJoint>().useSpring = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            other.GetComponent<HingeJoint>().useSpring = false;
        }
    }
}
