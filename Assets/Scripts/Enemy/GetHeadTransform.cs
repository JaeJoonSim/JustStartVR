using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHeadTransform : MonoBehaviour
{
    public Transform head;
    private void Update()
    {
        transform.eulerAngles = new Vector3(0f, head.eulerAngles.y+90f, 0f);
        //head.rotation; //Quaternion.Euler(0, 90, 0) *
        //transform.eulerAngles = new Vector3(0f, head.localRotation.y, 0f);
        //Debug.Log(head.localRotation.y);
        //Debug.Log(head.transform.localEulerAngles);
    }

}
