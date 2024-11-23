using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameLogic : MonoBehaviour
{
    [SerializeField] private GameObject RestartButtonObj;
    [SerializeField] private GameObject ExitButtonObj;
    private Button RestartButton;
    private Button ExitButton;

    void Awake()
    {
        RestartButton = RestartButtonObj.GetComponent<Button>();
        ExitButton = ExitButtonObj.GetComponent<Button>();

        RestartButton.onClick.AddListener(Restart);
        ExitButton.onClick.AddListener(Exit);
    }

    private void Restart()
    {
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
