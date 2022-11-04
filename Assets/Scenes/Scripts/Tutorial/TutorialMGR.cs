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
        TutorialHelper.gameObject.SetActive(true);
        TutorialHelper.Grip(pos);
    }
    public void next(int item)
    {
        switch (item)
        {
            case 0:

                break;
        }
    }
}
