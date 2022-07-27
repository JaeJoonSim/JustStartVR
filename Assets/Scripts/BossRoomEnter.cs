using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnter : MonoBehaviour
{
    public string bgmName = "";

    public GameObject camObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            camObject.GetComponent<PlayMusicOperator>().PlayBGM(bgmName);
    }
}
