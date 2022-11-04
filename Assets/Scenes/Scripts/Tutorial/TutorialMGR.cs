using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMGR : MonoBehaviour
{
    [SerializeField]
    TutorialHelper TutorialHelper;

    public GameObject password, pistal, Rifle, flash, Syringe;
    public int[] TutorialNumber = new int[5];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Grip(Transform pos)
    {
        Transform T = pos;
        TutorialHelper.gameObject.SetActive(true);
        TutorialHelper.Grip(T);
    }
    public void rel()
    {
        TutorialHelper.gameObject.SetActive(false);
    }
    public void next(int item)
    {
        TutorialHelper.Next(item);
    }
}
