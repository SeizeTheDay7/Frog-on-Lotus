using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameLogic : MonoBehaviour
{
    [SerializeField] private GameObject RestartButtonObj;
    [SerializeField] private GameObject ExitButtonObj;
    [SerializeField] private TextMeshProUGUI YourScore;
    [SerializeField] private TextMeshProUGUI HighScore;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject NewHighScore;
    private Button RestartButton;
    private Button ExitButton;

    void Awake()
    {
        RestartButton = RestartButtonObj.GetComponent<Button>();
        ExitButton = ExitButtonObj.GetComponent<Button>();

        RestartButton.onClick.AddListener(Restart);
        ExitButton.onClick.AddListener(Exit);

        YourScore.text = "Your Score : " + GameManager.Instance.Score.ToString();
        HighScore.text = "High Score : " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        if (GameManager.Instance.Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", GameManager.Instance.Score);
            NewHighScore.SetActive(true);
            SoundManager.Instance.PlayNewHighScore();
        }
        else
        {
            GameOver.SetActive(true);
            SoundManager.Instance.PlayGameOver();
        }
    }

    private void Restart()
    {
        // 자식들을 모두 삭제
        GameManager.Instance.flyCount = 0;
        // 모든 파리 삭제
        foreach (GameObject fly in GameObject.FindGameObjectsWithTag("Fly"))
        {
            Destroy(fly);
        }
        GameManager.Instance.GameStart();
        Destroy(gameObject);
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
