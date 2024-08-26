using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    protected float volume = 0.5f;

    public bool autoPlay = true;

    private void Start()
    {
        if (autoPlay)
            SoundManager.Instance.PlayBGM(audioClip, volume);
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        this.audioClip = audioClip;
    }

    public void Play()
    {
        SoundManager.Instance.PlayBGM(audioClip, volume);
    }

    public void Pause()
    {
        SoundManager.Instance.PauseBGM();
    }

    public void UnPause()
    {
        SoundManager.Instance.UnPauseBGM();
    }

    public void Stop()
    {
        SoundManager.Instance.StopBGM();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    public void ChangeVolume(float changeVolume)
    {
        volume = changeVolume;
        SoundManager.Instance.ChangeBGMVolume(changeVolume);
    }

    public bool IsBGMPlaying()
    {
        return SoundManager.Instance.IsBGMPlaying();
    }

    public bool IsBgmPlaying(AudioClip clip)
    {
        return SoundManager.Instance.IsBGMPlaying(clip);
    }

    public void GetOutputData(ref float[] samples, int channel)
    {
        SoundManager.Instance.GetOutputData(ref samples, channel);
    }
}
