using TMPro;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject frog;
    [SerializeField] StageSO stageSO;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] AdManager adManager;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] float time_limit = 30f;
    [SerializeField] float clear_delay = 2.5f;

    private int stage = 0;
    private float time = 0;
    private bool isGamePlaying = false;

    void Awake()
    {
        if (PlayerPrefs.HasKey("Stage"))
            stage = PlayerPrefs.GetInt("Stage");

        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (isGamePlaying)
        {
            time -= Time.deltaTime;
            timeText.text = time.ToString("F1");
            if (time < 0) { FailStage(); }
        }
    }

    // 다음 스테이지로
    public IEnumerator NextStage()
    {
        stage++;
        UIManager.Instance.SetShowStageUI(stage);
        SoundManager.Instance.PlayShowStage();
        yield return new WaitForSeconds(clear_delay / 2);
        StageStart();
    }

    // 스테이지 시작
    public void StageStart()
    {
        ResetGame();
        SpawnEnemies();
        UIManager.Instance.SetGameUI();
        isGamePlaying = true;
    }
    // 게임 시작 전 준비
    private void ResetGame()
    {
        time = time_limit;
        SoundManager.Instance.BGMStart();
        frog.GetComponent<FrogAttack>().EnableAttack();
        enemyManager.ResetAllEnemy();
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
        if (stage % 2 == 1) adManager.ShowInterstitialAd();
        else StartCoroutine(ClearDelay());
    }

    private IEnumerator ClearDelay()
    {
        SoundManager.Instance.PlayClearSFX();
        UIManager.Instance.SetClearUI();
        yield return new WaitForSeconds(clear_delay); // 2초 후에 게임 재시작
        StartCoroutine(NextStage());
    }

    // 스테이지 못 깼을 때 호출
    private void FailStage()
    {
        frog.GetComponent<FrogAttack>().DisableAttack();
        SoundManager.Instance.PlayFailSFX();
        UIManager.Instance.SetFailUI();
    }

    // 광고 볼 때 게임 멈춰두기
    public void StopGame()
    {
        SoundManager.Instance.StopAllSound(); // 소리 끄고
        frog.GetComponent<FrogAttack>()?.DisableAttack(); // 공격 멈추고
        // 적은 안 꺼도 된다. 스테이지 클리어 했을때만 광고 시청하므로.
    }
}