using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private GameObject fly;
    [SerializeField] private GameObject bee;
    [SerializeField] private GameObject butterfly;

    List<GameObject> enemyList = new List<GameObject>();

    [SerializeField] private GameObject boundArea;
    private Bounds bound;

    void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        bound = boundArea.GetComponent<SpriteRenderer>().bounds;
    }

    // 적들 일시정지
    public void StopAllEnemy()
    {
        foreach (GameObject go in enemyList) go.SetActive(false);
    }

    public void ResumeAllEnemy()
    {
        foreach (GameObject go in enemyList) go.SetActive(true);
    }

    // 요청한 종류의 적 스폰
    public GameObject SpawnEnemy(Enemy enemy, float difficulty)
    {
        GameObject target;
        if (enemy == Enemy.Fly) target = fly;
        else if (enemy == Enemy.Bee) target = bee;
        else if (enemy == Enemy.Butterfly) target = butterfly;
        else return null; // Invalid enemy type
        GameObject spawned = Instantiate(target, GetRandomSpawnPosition(), Quaternion.identity);
        spawned.GetComponent<EnemyMove>().SetSpeed(difficulty);
        enemyList.Add(spawned);
        return spawned;
    }

    // 적을 없앨 때 호출
    public void DestroyEnemy(GameObject enemy)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
            Destroy(enemy);
        }
        else Debug.LogError("[EnemyManager][DestroyEnemy] Enemy not found in list");
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float minX = bound.min.x;
        float maxX = bound.max.x;
        float minY = bound.min.y;
        float maxY = bound.max.y;

        return Random.Range(0, 2) == 0
            ? new Vector2(Random.Range(minX, maxX), Random.value < 0.5f ? minY : maxY) // 윗변 아랫변
            : new Vector2(Random.value < 0.5f ? minX : maxX, Random.Range(minY, maxY)); // 좌변 우변
    }
}