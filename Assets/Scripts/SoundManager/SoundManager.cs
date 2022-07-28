using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider MasterSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;
    //public Toggle MuteToggle;

    public static SoundManager m_instance;

    [SerializeField] GameObject SoundPlayerObj;
    [SerializeField] AudioClip[] AudioArray;

    public List<AudioSource> audioList = new List<AudioSource>();

    private float Volume = 1.0f;

    public enum SoundType
    { 
        BossSkill, CardKeySucess, CrashGlass, CrashGlass2,
        ElevatorArrive, Heal, fire, motolov,
        CardKeyFailed, HealTrigger, ElevatorMove, Engine,
        Drop, itemDrop, Burning, PistolShoot, Casing, Click, Click2,
        DoorColse, DoorOpen, DropReverse, Drop2, ImpactSmall2, Slide,
        SwitchOff, SwitchOn, Tang
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

    public void PlaySound(Vector3 Position, SoundType type, Transform Parent)
    {
        int index = (int)type;

        GameObject newObj = Instantiate(SoundPlayerObj, Position, Quaternion.identity, Parent);
        AudioSource newAudio = newObj.GetComponent<AudioSource>();
        newAudio.clip = AudioArray[index];
        newAudio.volume = Volume;
        newAudio.Play();
        audioList.Add(newAudio);
    }

    public void PlaySound(Vector3 Position, SoundType type, Transform parent, bool loop, float volume)
    {
        int index = (int)type;

        GameObject newObj = Instantiate(SoundPlayerObj, Position, Quaternion.identity, parent);
        AudioSource newAudio = newObj.GetComponent<AudioSource>();
        newAudio.clip = AudioArray[index];
        newAudio.volume = volume / 100.0f;
        newAudio.loop = loop;
        newAudio.Play();
        audioList.Add(newAudio);
    }

    public void StopSound(AudioSource source)
    {
        source.Stop();
        audioList.Remove(source);
        Destroy(source.gameObject);
    }

    public void AudioControl()
    {
        float MasterVolume = MasterSlider.value;
        float BGMVolume = BGMSlider.value;
        float SFXVolume = SFXSlider.value;

        if (MasterVolume <= -40f)
            masterMixer.SetFloat("Master", -80);
        else
            masterMixer.SetFloat("Master", MasterVolume);

        if (BGMVolume <= -40f)
            masterMixer.SetFloat("BGM", -80);
        else
            masterMixer.SetFloat("BGM", BGMVolume);

        if (SFXVolume <= -40f)
            masterMixer.SetFloat("SFX", -80);
        else
            masterMixer.SetFloat("SFX", SFXVolume);
        
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
