using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SFXScriptableObject sfxConfig;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    public void PlaySFX(string audioID)
    {
        AudioSource audioSourceToUse = sfxAudioSource;
        audioSourceToUse.PlayOneShot(GetAudioUsingID(audioID));
    }
    public void PlayMusic(string audioID)
    {
        AudioSource audioSourceToUse = musicAudioSource ;
        audioSourceToUse.clip = GetAudioUsingID(audioID);
        audioSourceToUse.Play();
    }
    private AudioClip GetAudioUsingID(string audioID)
    {
        SFXConfig config = sfxConfig.SFXConfigs.Find(x => x.id == audioID);
        if (config == null)
        {
            Debug.LogError("Audio clip " + audioID + " not found");
        }
        return config.audioClip;
    }
}
