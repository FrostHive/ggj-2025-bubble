using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{

    public static AudioManager ACIVEMIXER = null;

    public AudioClip[] bgSounds;
    public AudioClip[] sounds;
    public AudioSource bgSource;
    public AudioSource audioSource;
    public AudioMixer audioMixer;


    void Start()
    {
        if (ACIVEMIXER == null)
        {
            ACIVEMIXER = this;
            if (audioSource == null) audioSource = FindFirstObjectByType<AudioSource>();
            if (bgSource == null) bgSource = audioSource;
        }
        if(bgSounds.Length > 0) bgSource.clip = bgSounds[0];
    }

    //default playsound, assues position is at (0,0,0)

    static public void PlaySound(string name)
    {
        if (!ACIVEMIXER.audioSource.isPlaying)
        {
            AudioClip fClip = ACIVEMIXER.GetSound(name);
            ACIVEMIXER.audioSource.clip = fClip;
            ACIVEMIXER.audioSource.Play();
        }
    }
    static public void PlaySound(int index)
    {
        if (!ACIVEMIXER.audioSource.isPlaying)
        {
            AudioClip fClip = ACIVEMIXER.sounds[index];
            ACIVEMIXER.audioSource.clip = fClip;
            ACIVEMIXER.audioSource.Play();
        }
    }

    static public void PlayBgMusic()
    {
        ACIVEMIXER.bgSource.Play();
    }
    static public void StopBgMusic()
    {
        ACIVEMIXER.bgSource.Stop();
    }

    static public void PlayOneShot(string name, float volume)
    {
        AudioClip fClip = ACIVEMIXER.GetSound(name);
        if (!ACIVEMIXER.audioSource.isPlaying)
        {
            ACIVEMIXER.audioSource.clip = fClip;
            ACIVEMIXER.audioSource.PlayOneShot(fClip, volume);
        }
    }
    static public void PlayOneShot(int pIndex, float pVolume)
    {
        AudioClip fClip = ACIVEMIXER.sounds[pIndex];
        if (!ACIVEMIXER.audioSource.isPlaying)
        {
            ACIVEMIXER.audioSource.clip = fClip;
            ACIVEMIXER.audioSource.PlayOneShot(fClip, pVolume);
        }
    }

    AudioClip GetSound(string name)
    {
        for (int i = 0; i < ACIVEMIXER.sounds.Length; i++)    
            if (ACIVEMIXER.sounds[i].name == name)
                return ACIVEMIXER.sounds[i];
       
        
        Debug.Log("Error: Sound: " + name + " ; not found");
        return null;

    }
    AudioClip GetSound(int index)
    {
        if(ACIVEMIXER.sounds[index])
            return ACIVEMIXER.sounds[index];


        Debug.Log("Error: Sound at index: " + index + " ; not found");
        return null;

    }

    static public void ChangeMixerVolume(int pMixer, float pNewVolume)
    {
        if (pMixer == 1)
        {
            ACIVEMIXER.audioMixer.SetFloat("SfxVol", pNewVolume);
        }
        if (pMixer == 2)
        {
            ACIVEMIXER.audioMixer.SetFloat("MusicVol", pNewVolume);
        }
        if (pMixer == 0)
        {
            ACIVEMIXER.audioMixer.SetFloat("MasterVol", pNewVolume);
        }
    }
}
