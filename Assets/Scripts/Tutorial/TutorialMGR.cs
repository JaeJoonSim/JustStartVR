using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMGR : MonoBehaviour
{
    [SerializeField]
    TutorialHelper TutorialHelper;

    public GameObject password, pistal, Rifle, flash, Syringe;
    public int[] TutorialNumber = new int[5];

    public void de()
    {
        Destroy(TutorialHelper.gameObject);
    }
    public void Grip(Transform pos)
    {
        if (TutorialHelper == null) return;
        Transform T = pos;
        TutorialHelper.gameObject.SetActive(true);
        TutorialHelper.Grip(T);
    }
    public void rel()
    {
        if (TutorialHelper == null) return;
        TutorialHelper.gameObject.SetActive(false);
    }
    public void next(int item)
    {
        if (TutorialHelper == null) return;
        TutorialHelper.Next(item);
    }
}
