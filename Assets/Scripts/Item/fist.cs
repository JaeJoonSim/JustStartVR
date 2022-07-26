using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class fist : MonoBehaviour
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
    [SerializeField]
    GameObject Fist;
    public XRButtonType XR_Button_Type;
    public XRButtonType XR_Button_Type2;
    public float speed = 0.1f;

    InputFeatureUsage<bool> inputFeatureUsage;
    InputFeatureUsage<bool> inputFeatureUsage2;

    bool button1, button2;

    private Vector3 oldPosition;
    private Vector3 currentPosition;
    private double velocity;

    private void Start()
    {
        Fist.SetActive(false);
        oldPosition = transform.position;
        //rb.velocity; // Velocity of gameObject (Vector3)
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
        switch (XR_Button_Type2)
        {
            case XRButtonType.Trigger:
                inputFeatureUsage2 = CommonUsages.triggerButton;
                break;
            case XRButtonType.Grip:
                inputFeatureUsage2 = CommonUsages.gripButton;
                break;
            case XRButtonType.primaryButton:
                inputFeatureUsage2 = CommonUsages.primaryButton;
                break;
            case XRButtonType.primaryTouch:
                inputFeatureUsage2 = CommonUsages.primaryTouch;
                break;
            case XRButtonType.secondaryButton:
                inputFeatureUsage2 = CommonUsages.secondaryButton;
                break;
            case XRButtonType.secondaryTouch:
                inputFeatureUsage2 = CommonUsages.secondaryTouch;
                break;
            case XRButtonType.primary2DAxisClick:
                inputFeatureUsage2 = CommonUsages.primary2DAxisClick;
                break;
            case XRButtonType.primary2DAxisTouch:
                inputFeatureUsage2 = CommonUsages.primary2DAxisTouch;
                break;
        }
    }

    private void FixedUpdate()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            bool Value;
            button1 = (device.TryGetFeatureValue(inputFeatureUsage, out Value) && Value);
            button2 = (device.TryGetFeatureValue(inputFeatureUsage2, out Value) && Value);
        }

        Fist.SetActive(false);
        if (!(button1 && button2)) return;

        currentPosition = transform.position;
        var dis = (currentPosition - oldPosition);
        var distance = Math.Sqrt(Math.Pow(dis.x, 2) + Math.Pow(dis.y, 2) + Math.Pow(dis.z, 2));
        velocity = distance / Time.deltaTime;
        //Debug.Log(velocity);
        oldPosition = currentPosition;
        if (velocity > speed)
        {
            Fist.SetActive(true);
        }
    }
}
