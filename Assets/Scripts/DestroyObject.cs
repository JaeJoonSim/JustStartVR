using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float liveTime = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        // 오브젝트 삭제
        Destroy(this.gameObject, liveTime);
    }
}
