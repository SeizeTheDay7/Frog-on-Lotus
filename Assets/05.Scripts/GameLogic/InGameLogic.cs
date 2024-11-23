using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class InGameLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private float time;

    [SerializeField] private GameObject fly;
    [SerializeField] private GameObject holeArea;
    [SerializeField] private GameObject outerArea;
    private Bounds holeBounds;
    private Bounds outerBounds;


    void Start()
    {
        Debug.Log("InGameLogic :: Game Start!");
        TimeText.text = "Time\n" + time.ToString("F1");
        ScoreText.text = "Score\n" + GameManager.Instance.Score.ToString();

        holeBounds = holeArea.GetComponent<SpriteRenderer>().bounds;
        outerBounds = outerArea.GetComponent<SpriteRenderer>().bounds;
    }

    void Update()
    {
        // 시간이 0이 되면 게임 종료
        time -= Time.deltaTime;
        if (time < 0)
        {
            GameManager.Instance.GameEnd();
            Destroy(gameObject);
        }

        TimeText.text = "Time\n" + time.ToString("F1");
        ScoreText.text = "Score\n" + GameManager.Instance.Score.ToString();
    }

    public void NewFly()
    {
        Instantiate(fly, GetRandomSpawnPosition(), Quaternion.identity);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // 뚫린 영역을 제외한 네 구역 정의
        Vector2[] corners = new Vector2[4]
        {
        new Vector2(Random.Range(outerBounds.min.x, holeBounds.min.x), Random.Range(outerBounds.min.y, outerBounds.max.y)), // 좌측
        new Vector2(Random.Range(holeBounds.max.x, outerBounds.max.x), Random.Range(outerBounds.min.y, outerBounds.max.y)), // 우측
        new Vector2(Random.Range(holeBounds.min.x, holeBounds.max.x), Random.Range(outerBounds.min.y, holeBounds.min.y)), // 아래
        new Vector2(Random.Range(holeBounds.min.x, holeBounds.max.x), Random.Range(holeBounds.max.y, outerBounds.max.y)) // 위
        };

        Debug.Log("InGameLogic :: corners : " + corners);
        foreach (Vector2 Corner in corners)
        {
            Debug.Log("InGameLogic :: corner : " + Corner);
        }

        // 랜덤하게 하나의 구역에서 위치 선택
        return corners[Random.Range(0, corners.Length)];
    }
}
