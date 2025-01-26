using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{

    public static AudioManager ACTIVEMIXER = null;

    public AudioClip[] bgSounds;
    public AudioClip[] sounds;
    public AudioSource bgSource;
    public AudioSource audioSource;
    public AudioMixer audioMixer;


    void Start()
    {
        if (ACTIVEMIXER == null)
        {
            ACTIVEMIXER = this;
            if (audioSource == null) audioSource = FindFirstObjectByType<AudioSource>();
            if (bgSource == null) bgSource = audioSource;
        }

        //if(bgSounds.Length > 0) bgSource.clip = bgSounds[0];
        //PlayBgMusic();
    }

    private void OnLevelWasLoaded(int level)
    {
        
    }
    //default playsound, assues position is at (0,0,0)

    static public void PlaySound(string name)
    {
        if (!ACTIVEMIXER.audioSource.isPlaying)
        {
            AudioClip fClip = ACTIVEMIXER.GetSound(name);
            ACTIVEMIXER.audioSource.clip = fClip;
            ACTIVEMIXER.audioSource.Play();
        }
    }
    static public void PlaySound(int index)
    {
        if (!ACTIVEMIXER.audioSource.isPlaying)
        {
            AudioClip fClip = ACTIVEMIXER.sounds[index];
            ACTIVEMIXER.audioSource.clip = fClip;
            ACTIVEMIXER.audioSource.Play();
        }
    }

    static public void PlayBgMusic()
    {
        if (ACTIVEMIXER.bgSource.isPlaying)
            ACTIVEMIXER.bgSource.Stop();
        ACTIVEMIXER.bgSource.Play();
    }
    static public void PlayBgMusic(int index)
    {
        if (ACTIVEMIXER.bgSource.isPlaying)
            ACTIVEMIXER.bgSource.Stop();
        ACTIVEMIXER.bgSource.clip = ACTIVEMIXER.bgSounds[index];
        ACTIVEMIXER.bgSource.Play();
    }
    static public void StopBgMusic()
    {
        ACTIVEMIXER.bgSource.Stop();
    }

    static public void PlayOneShot(string name, float volume)
    {
        AudioClip fClip = ACTIVEMIXER.GetSound(name);
        if (!ACTIVEMIXER.audioSource.isPlaying)
        {
            ACTIVEMIXER.audioSource.clip = fClip;
            ACTIVEMIXER.audioSource.PlayOneShot(fClip, volume);
        }
    }
    static public void PlayOneShot(int pIndex, float pVolume)
    {
        AudioClip fClip = ACTIVEMIXER.sounds[pIndex];
        if (!ACTIVEMIXER.audioSource.isPlaying)
        {
            ACTIVEMIXER.audioSource.clip = fClip;
            ACTIVEMIXER.audioSource.PlayOneShot(fClip, pVolume);
        }
    }

    AudioClip GetSound(string name)
    {
        for (int i = 0; i < ACTIVEMIXER.sounds.Length; i++)    
            if (ACTIVEMIXER.sounds[i].name == name)
                return ACTIVEMIXER.sounds[i];
       
        
        Debug.Log("Error: Sound: " + name + " ; not found");
        return null;

    }
    AudioClip GetSound(int index)
    {
        if(ACTIVEMIXER.sounds[index])
            return ACTIVEMIXER.sounds[index];


        Debug.Log("Error: Sound at index: " + index + " ; not found");
        return null;

    }

    static public void ChangeMixerVolume(int pMixer, float pNewVolume)
    {
        if (pMixer == 1)
        {
            ACTIVEMIXER.audioMixer.SetFloat("SfxVol", pNewVolume);
        }
        if (pMixer == 2)
        {
            ACTIVEMIXER.audioMixer.SetFloat("MusicVol", pNewVolume);
        }
        if (pMixer == 0)
        {
            ACTIVEMIXER.audioMixer.SetFloat("MasterVol", pNewVolume);
        }
    }
}
