using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager m_instance;

    [SerializeField] GameObject SoundPlayerObj;
    [SerializeField] AudioClip[] AudioArray;

    public List<AudioSource> audioList = new List<AudioSource>();

    public float maxVolume = 1.0f;

    public enum SoundType
    { 
        BossSkill,
        CardKeySucess,
        CrashGlass,
        CrashGlass2,
        ElevatorArrive,
        Heal,
        fire,
        motolov,
        CardKeyFailed,
        HealTrigger,
        ElevatorMove,
        Engine,
        Drop,
        itemDrop,
        BossFoot,
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
        newAudio.volume = maxVolume;
        newAudio.Play();
        audioList.Add(newAudio);
    }

    public void PlaySound(Vector3 Position, SoundType type, Transform Parent)
    {
        int index = (int)type;

        GameObject newObj = Instantiate(SoundPlayerObj, Position, Quaternion.identity, Parent);
        AudioSource newAudio = newObj.GetComponent<AudioSource>();
        newAudio.clip = AudioArray[index];
        newAudio.volume = maxVolume;
        newAudio.Play();
        audioList.Add(newAudio);
    }

    public void PlaySound(Vector3 Position, SoundType type, bool loop, float volume)
    {
        int index = (int)type;

        GameObject newObj = Instantiate(SoundPlayerObj, Position, Quaternion.identity);
        AudioSource newAudio = newObj.GetComponent<AudioSource>();
        newAudio.clip = AudioArray[index];
        newAudio.volume = volume / 100.0f / maxVolume;
        newAudio.loop = true;
        newAudio.Play();
        audioList.Add(newAudio);
    }

    public void StopSound(AudioSource source)
    {
        source.Stop();
        audioList.Remove(source);
        Destroy(source.gameObject);
    }
}
