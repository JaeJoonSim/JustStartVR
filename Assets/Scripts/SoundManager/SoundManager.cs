using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager m_instance;

    [SerializeField] GameObject SoundPlayerObj;
    [SerializeField] AudioClip[] AudioArray;

    private List<AudioSource> audioList = new List<AudioSource>();

    public float Volume = 1.0f;

    public enum SoundType
    { 
        BossFoot,
        CardKeySucess,
        CrashGlass,
        CrashGlass2,
        ElevatorArrive,
        Heal,
        fire,
        motolov,
        CardKeyFailed,
        HealTrigger,
    }

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
    }

    public void PlaySound(Vector3 Position, SoundType type)
    {
        int index = (int)type;

        GameObject newObj = Instantiate(SoundPlayerObj, Position, Quaternion.identity);        
        AudioSource newAudio = newObj.GetComponent<AudioSource>();
        newAudio.clip = AudioArray[index];
        newAudio.volume = Volume;
        newAudio.Play();
        audioList.Add(newAudio);
    }
}
