using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 전역 접근용 정적 변수
    public int Score;
    [SerializeField] private GameObject InGameManager;
    private InGameLogic inGameLogic;
    [SerializeField] private GameObject EndGameManager;
    private EndGameLogic endGameLogic;

    private void Awake()
    {
        Instance = this; // 현재 인스턴스를 정적 변수에 할당
        Score = 0;
        GameStart();
    }

    public void GameStart()
    {
        Debug.Log("GameManager :: Game Start!");
        Score = 0;
        Instantiate(InGameManager);
    }

    public void AddScore()
    {
        Score += 1;
    }

    public void GameEnd()
    {
        Debug.Log("GameManager :: Game End!");
        Instantiate(EndGameManager);
    }
}
