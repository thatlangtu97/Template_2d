﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager instance 
    {
        get
        {
            if (_instance == null)
            {

//                GameObject soundManager = Instantiate( Resources.Load<GameObject>("SoundManager"));
//                _instance = soundManager.GetComponent<SoundManager>();
                GameObject soundManager = new GameObject("SoundManager");
                _instance = soundManager.AddComponent<SoundManager>();
                DontDestroyOnLoad(soundManager);
            }
            return _instance;
        }
    }

    public int sizeAudio = 20;
    public AudioMixer mixer;
    public AudioMixerGroup globalSound, localSound;
    public float Scale_DB_Mixer = 20;
    public float minMixer = -20f;
    public float defaultMixer = 0f;
    public float duration = 2f;
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
    public AudioSource playSound(AudioClip clip, bool checkInterrupt, float volume, bool loop, float speed, SoundGroup soundGroup )
    {
        if (!isMute && clip)
        {
            AudioSource source = getSourceFromPool((checkInterrupt) ? clip : null);
            switch (soundGroup)
            {
                case SoundGroup.Global:
                    source.outputAudioMixerGroup = globalSound;
                    break;
                case SoundGroup.Local:
                    source.outputAudioMixerGroup = localSound;
                    mixer.SetFloat(globalSound.name, minMixer);
                    mixer.DOSetFloat(globalSound.name, defaultMixer, duration);
                    break;
            }
            
            
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

    public void stopSound(AudioClip audioClip)
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
    public static AudioSource PlaySound(AudioClip clip, bool checkInterrupt, float volume, bool loop, float speed)
    {
        return instance.playSound(clip, checkInterrupt, volume, loop, speed);
    }
    public static AudioSource PlaySound(AudioClip clip, bool checkInterrupt, float volume, bool loop, float speed, SoundGroup soundGroup)
    {
        return instance.playSound(clip, checkInterrupt, volume, loop, speed, soundGroup);
    }
    public static void StopSound(AudioClip audioClip)
    {
        instance.stopSound(audioClip);
    }
}

public enum SoundGroup
{
    Global,
    Local,
}