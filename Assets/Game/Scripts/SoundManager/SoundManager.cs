using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager instance 
    {
        get
        {
            if (_instance == null)
            {

                GameObject soundManager = Instantiate( Resources.Load<GameObject>("SoundManager"));
                _instance = soundManager.GetComponent<SoundManager>();
                DontDestroyOnLoad(soundManager);
            }
            return _instance;
        }
    }

    public int sizeAudio = 10;
    
    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioSource> audioSourcePool = new List<AudioSource>();
    private bool isMute;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        initAudio();
    }

    void initAudio()
    {
        int i = 0;
        while (i < sizeAudio)
        {
            audioSourcePool.Add(gameObject.AddComponent<AudioSource>());
            ++i;
        }
    }
    private AudioSource getSourceFromPool(AudioClip clip = null)
    {
        if (clip != null)
        {
            for (int i = 0; i < audioSourcePool.Count; i++)
            {
                if (audioSourcePool[i] != null && audioSourcePool[i].clip != null &&
                    audioSourcePool[i].clip.name == clip.name)
                {
                    return audioSourcePool[i];
                }
            }
        }

        foreach (AudioSource adSource in audioSourcePool)
        {
            if (adSource != null && !adSource.isPlaying)
                return adSource;
        }
        
        return audioSourcePool[1];
    }
    public bool SfxIsPlaying(AudioClip soundClip)
    {
        foreach (AudioSource audioSource in audioSourcePool)
        {
            if (audioSource.clip == soundClip && audioSource.isPlaying)
                return true;
        }
        return false;
    }
//    private bool IsReachLimit(AudioClip clip)
//    {
//        if (clip != null )
//        {
////            int cap = sfxCap[clip.name];
//            int count = 0;
//            for (int i = audioSourcePool.Count - 1; i >= 0; i--)
//            {
//                if (audioSourcePool[i] != null && audioSourcePool[i].clip != null &&
//                    audioSourcePool[i].clip.name == clip.name && audioSourcePool[i].isPlaying)
//                {
//                    count++;
////                    if (count >= cap)
////                    {
//                        return true;
////                    }
//                }
//            }
//        }
//
//        return false;
//    }
    public AudioSource playSound(AudioClip clip, bool checkInterrupt, float volume, bool loop, float speed)
    {
        if (!isMute && clip)
        {
            AudioSource source = getSourceFromPool((checkInterrupt) ? clip : null);
            if (source && (!checkInterrupt || !SfxIsPlaying(clip)))
            {
//                if (!IsReachLimit(clip))
//                {
                    source.volume = volume;
                    source.clip = clip;
                    source.Play();
                    source.loop = loop;
                    source.pitch = speed;
//                }
                return source;
            }
        }

        return null;
    }
    public void StopSound(AudioClip audioClip)
    {
        for (int i = 0; i < audioSourcePool.Count; i++)
        {
            if (audioSourcePool[i] != null && audioSourcePool[i].clip != null && audioClip != null &&
                audioSourcePool[i].clip.name == audioClip.name)
            {
                audioSourcePool[i].Stop();
            }
        }
    }
}
