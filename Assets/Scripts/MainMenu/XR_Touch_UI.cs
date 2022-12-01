using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XR_Touch_UI : MonoBehaviour
{
    public string TagName;
    public UnityEvent OnEnter;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(TagName == other.tag)
        {
            OnEnter.Invoke();
        }
    }
}
