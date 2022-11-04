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

    public static SoundManager m_instance;

    [SerializeField] GameObject SoundPlayerObj;
    [SerializeField] AudioClip[] AudioArray;

    public List<AudioSource> audioList = new List<AudioSource>();

    private const float defaultVolume = 1.0f;

    public enum SoundType
    {
        BossSkill, CardKeySucess, CrashGlass, CrashGlass2,
        ElevatorArrive, Heal, fire, motolov,
        CardKeyFailed, HealTrigger, ElevatorMove, Engine,
        Drop, itemDrop, Burning, PistolShoot, Casing, Click, Click2,
        DoorColse, DoorOpen, DropReverse, Drop2, ImpactSmall2, Slide,
        SwitchOff, SwitchOn, Tang,


        zombieAttack1, zombieAttack2, zombieAttack3, zombieAttack4,
        zombieAttack5, zombieAttack6, zombieAttack7, zombieAttack8, zombieAttack9,

        zombieSearching1, zombieSearching2,

        zombieScreaming1, zombieScreaming2, zombieScreaming3,

        zombieIdle,

        tongue, tongueGrap, playerHit, playerHit2, BossFoot, gameOver, CrackGlass,
        swing, neon, shutter

    }

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
    }


    public AudioSource PlaySound(Vector3 Position, SoundType type)
    {
        int index = (int)type;

        GameObject newObj = Instantiate(SoundPlayerObj, Position, Quaternion.identity);
        AudioSource newAudio = newObj.GetComponent<AudioSource>();
        newAudio.clip = AudioArray[index];
        newAudio.volume = defaultVolume;
        newAudio.Play();
        audioList.Add(newAudio);
        return newAudio;
    }

    public AudioSource PlaySound(Vector3 Position, SoundType type, Transform Parent)
    {
        AudioSource newAudio = PlaySound(Position, type);
        newAudio.transform.parent = Parent;
        return newAudio;
    }

    public AudioSource PlaySound(Vector3 Position, SoundType type, Transform parent, bool loop, float volume)
    {
        AudioSource newAudio = PlaySound(Position, type);
        newAudio.volume = volume / 100.0f;
        newAudio.loop = loop;
        return newAudio;
    }

    public AudioSource ChangeSound(Vector3 Position, SoundType type,
        Transform parent, bool loop, float volume, AudioSource source)
    {
        StopSound(source);

        AudioSource newAudio = PlaySound(Position, type);
        newAudio.transform.parent = parent;
        newAudio.volume = volume / 100.0f;
        newAudio.loop = loop;
        return newAudio;
    }

    public void StopSound(AudioSource source)
    {
        if (source == null)
        {
            return;
        }

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