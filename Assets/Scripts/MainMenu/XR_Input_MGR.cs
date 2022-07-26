using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class XR_Input_MGR : MonoBehaviour
{
    public enum XRButtonType
    {
        Trigger,
        Grip,
        primaryButton,
        primaryTouch,
        secondaryButton,
        secondaryTouch,
        primary2DAxisClick,
        primary2DAxisTouch
    }
    public XRButtonType XR_Button_Type;
    public UnityEvent Onprssd;

    InputFeatureUsage<bool> inputFeatureUsage;

    private void Start()
    {
        switch (XR_Button_Type)
        {
            case XRButtonType.Trigger:
                inputFeatureUsage = CommonUsages.triggerButton;
                break;
            case XRButtonType.Grip:
                inputFeatureUsage = CommonUsages.gripButton;
                break;
            case XRButtonType.primaryButton:
                inputFeatureUsage = CommonUsages.primaryButton;
                break;
            case XRButtonType.primaryTouch:
                inputFeatureUsage = CommonUsages.primaryTouch;
                break;
            case XRButtonType.secondaryButton:
                inputFeatureUsage = CommonUsages.secondaryButton;
                break;
            case XRButtonType.secondaryTouch:
                inputFeatureUsage = CommonUsages.secondaryTouch;
                break;
            case XRButtonType.primary2DAxisClick:
                inputFeatureUsage = CommonUsages.primary2DAxisClick;
                break;
            case XRButtonType.primary2DAxisTouch:
                inputFeatureUsage = CommonUsages.primary2DAxisTouch;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            bool Value;
            if (device.TryGetFeatureValue(inputFeatureUsage, out Value))
            {
                //Debug.Log("button is pressed" + XR_Button_Type);
                Onprssd.Invoke();
            }
        }
    }
}

