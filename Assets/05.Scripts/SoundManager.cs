using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip start;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip newhighscore;
    [SerializeField] private AudioClip gameover;
    [SerializeField] private AudioClip earnScore;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStart()
    {
        audioSource.PlayOneShot(start);
    }

    public void PlayAttack()
    {
        audioSource.PlayOneShot(attack);
    }

    public void PlayNewHighScore()
    {
        audioSource.PlayOneShot(newhighscore);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameover);
    }

    public void PlayEarnScore()
    {
        audioSource.PlayOneShot(earnScore);
    }

    public void BGMStop()
    {
        audioSource.Stop();
    }
}
