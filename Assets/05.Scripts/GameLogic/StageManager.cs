using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    [SerializeField] GameObject frog;
    [SerializeField] StageSO stageSO;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] AdManager adManager;

    private int stage = 0;
    private float time;
    private bool isPlaying;

    void Awake()
    {
        if (PlayerPrefs.HasKey("Stage"))
            stage = PlayerPrefs.GetInt("Stage");

        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 스테이지 진행 중일 때 실패 여부 판정
    void Update()
    {
        if (isPlaying)
        {
            time -= Time.deltaTime;
            if (time <= 0) FailStage();
        }
    }

    // 스테이지 시작
    public void StageStart()
    {
        ResetGame();
        SpawnEnemies();
        UIManager.Instance.SetGameUI();
    }

    // 게임 시작 전 준비
    private void ResetGame()
    {
        time = 30f;
        SoundManager.Instance.BGMStart();
        enemyManager.ResetAllEnemy();
        frog.GetComponent<FrogAttack>().EnableAttack();
    }

    private void SpawnEnemies()
    {
        EnemyInfo enemyInfo = stageSO.stageInfoList[stage].enemyinfo;
        int fly = enemyInfo.fly;
        int bee = enemyInfo.bee;
        int butterfly = enemyInfo.butterfly;
        float difficulty = stageSO.stageInfoList[stage].difficulty;
        for (int i = 0; i < fly; i++) enemyManager.SpawnEnemy(Enemy.Fly, difficulty);
        for (int i = 0; i < bee; i++) enemyManager.SpawnEnemy(Enemy.Bee, difficulty);
        for (int i = 0; i < butterfly; i++) enemyManager.SpawnEnemy(Enemy.Butterfly, difficulty);
    }

    // 벌레를 먹었을 때 호출
    public void CheckClear()
    {
        if (enemyManager.AreAllEnemyDead()) ClearStage();
    }

    // EnemyList가 전부 비었을 때 호출
    private void ClearStage()
    {
        // SoundManager.Instance.PlayClearSFX(); 
        UIManager.Instance.SetClearUI();
        if (stage % 2 == 1) adManager.ShowInterstitialAd();
        else ContinueGame(); // TODO :: 클리어 연출 나온 뒤에 ContinueGame() 호출하는 걸로 바꾸기
    }

    // 스테이지 못 깼을 때 호출
    private void FailStage()
    {
        isPlaying = false;
        time = 0;
        frog.GetComponent<FrogAttack>().DisableAttack();
        UIManager.Instance.SetFailUI();
        SoundManager.Instance.PlayFailSFX();
    }

    // 광고 볼 때 게임 멈춰두기
    public void StopGame()
    {
        SoundManager.Instance.StopAllSound(); // 소리 끄고
        frog.GetComponent<FrogAttack>()?.DisableAttack(); // 공격 멈추고
        // 적은 안 꺼도 된다. 스테이지 클리어 했을때만 광고 시청하므로.
    }

    // 광고 본 뒤에 게임 재시작
    public void ContinueGame()
    {
        stage++;
        StageStart();
    }
}