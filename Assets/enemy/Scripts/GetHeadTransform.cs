using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHeadTransform : MonoBehaviour
{
    public Transform head;

    void Start()
    {
        transform.rotation = head.rotation;
    }

}
