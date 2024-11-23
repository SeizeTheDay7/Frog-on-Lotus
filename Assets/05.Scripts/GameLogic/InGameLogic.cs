using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private float time;

    void Start()
    {
        Debug.Log("InGameLogic :: Game Start!");
        TimeText.text = "Time\n" + time.ToString("F1");
        ScoreText.text = "Score\n" + GameManager.Instance.Score.ToString();
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
}
