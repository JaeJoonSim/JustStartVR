using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audio;

    void Update()
    {
        if(!audio.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
