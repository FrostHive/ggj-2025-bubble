using System.Xml.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip levelMusic;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource sfxObject;
    [SerializeField] private AudioSource UIObject;
    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform)
    {
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.Play();
        float clipLength = audioClip.length;
        Destroy(audioSource, clipLength);
    }

    public void PlayUISound(AudioClip audioClip)
    {
        AudioSource audioSource = Instantiate(UIObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.Play();
        float clipLength = audioClip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
