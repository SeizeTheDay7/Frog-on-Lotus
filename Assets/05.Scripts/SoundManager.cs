using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource bgmSource;
    [SerializeField] private int audioSourceSize = 5;
    private List<AudioSource> audioSourceList;
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip start;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip clear;
    [SerializeField] private AudioClip fail;
    [SerializeField] private AudioClip earnScore;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        audioSourceList = new List<AudioSource>();

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.Play();

        for (int i = 0; i < audioSourceSize; i++)
        {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            audioSourceList.Add(src);
        }
    }

    private AudioSource GetFreeAS()
    {
        foreach (AudioSource src in audioSourceList)
        {
            if (!src.isPlaying) return src;
        }

        AudioSource newsrc = gameObject.AddComponent<AudioSource>();
        audioSourceList.Add(newsrc);
        return newsrc;
    }

    public void PlayStartSFX()
    {
        AudioSource src = GetFreeAS();
        src.clip = start;
        src.Play();
    }

    public void PlayAttackSFX()
    {
        AudioSource src = GetFreeAS();
        src.clip = attack;
        src.Play();
    }

    public void PlayClearSFX()
    {
        AudioSource src = GetFreeAS();
        src.clip = clear;
        src.Play();
    }

    public void PlayFailSFX()
    {
        AudioSource src = GetFreeAS();
        src.clip = fail;
        src.Play();
    }

    public void PlayEarnScore()
    {
        AudioSource src = GetFreeAS();
        src.clip = earnScore;
        src.Play();
    }

    public void BGMStart()
    {
        bgmSource.Play();
    }

    public void BGMStop()
    {
        bgmSource.Stop();
    }

    public void StopAllSound()
    {
        BGMStop();
        foreach (AudioSource src in audioSourceList) src.Stop();
    }
}
