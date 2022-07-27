using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asdasd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float time = 1.0f;
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time > 0.0f) return;
        time = 1.0f;
        SoundManager.m_instance.PlaySound(transform.position, SoundManager.SoundType.BossFoot);
    }
}
