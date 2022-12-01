using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnter : MonoBehaviour
{
    public string bgmName = "";
    public Animator animator;
    public GameObject camObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            camObject.GetComponent<PlayMusicOperator>().PlayBGM(bgmName);
            if(animator != null)
            {
                animator.enabled = true;
                SoundManager.m_instance.PlaySound(this.transform.position, SoundManager.SoundType.shutter);
            }
        }
    }
}
