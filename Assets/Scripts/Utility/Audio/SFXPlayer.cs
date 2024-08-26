using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : AudioPlayer
{
    [SerializeField]
    protected AudioClip audioClip;
    public void SetAudioClip(AudioClip audioClip)
    {
        this.audioClip = audioClip;
    }

    public override void Play()
    {
        base.Play();
        audioClipKey = audioClip.name;
        SoundManager.Instance.PlaySFX(this, audioClip, isLoop);
    }
    
    public void PlayWithRandomPitch(Vector2 randomPitch, bool dontPlayWhenPlaying = false)
    {
        if (dontPlayWhenPlaying && isPlay)
            return;
        base.Play();
        audioClipKey = audioClip.name;
        SoundManager.Instance.PlaySFX(this, audioClip, isLoop, randomPitch);
    }
    
    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    public override void ChangeVolume(float changeVolume)
    {
        audioSource.volume = changeVolume * SoundManager.Instance.SFXMasterVolume;
    }
}
