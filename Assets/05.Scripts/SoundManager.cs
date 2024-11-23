using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip start;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip endgame;
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

    public void PlayEndGame()
    {
        audioSource.PlayOneShot(endgame);
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
