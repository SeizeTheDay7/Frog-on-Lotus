using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    [SerializeField] private int audioSourceSize = 5;
    private List<AudioSource> audioSourceList;
    [SerializeField] private AudioClip start;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip newhighscore;
    [SerializeField] private AudioClip gameover;
    [SerializeField] private AudioClip earnScore;

    void Awake()
    {
        Instance = this;
        audioSourceList = new List<AudioSource>();
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

    public void PlayStart()
    {
        AudioSource src = GetFreeAS();
        src.clip = start;
        src.Play();
    }

    public void PlayAttack()
    {
        AudioSource src = GetFreeAS();
        src.clip = attack;
        src.Play();
    }

    public void PlayNewHighScore()
    {
        AudioSource src = GetFreeAS();
        src.clip = newhighscore;
        src.Play();
    }

    public void PlayGameOver()
    {
        AudioSource src = GetFreeAS();
        src.clip = gameover;
        src.Play();
    }

    public void PlayEarnScore()
    {
        AudioSource src = GetFreeAS();
        src.clip = earnScore;
        src.Play();
    }

    public void BGMStop()
    {
        audioSource.Stop();
    }
}
