using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject FailUI;
    [SerializeField] GameObject ClearUI;
    [SerializeField] GameObject ShowStageUI;
    [SerializeField] TextMeshProUGUI stageText;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetGameUI()
    {
        CloseAllUI();
        GameUI.SetActive(true);
    }

    public void SetFailUI()
    {
        CloseAllUI();
        FailUI.SetActive(true);
    }

    public void SetClearUI()
    {
        CloseAllUI();
        ClearUI.SetActive(true);
    }

    public void SetShowStageUI(int stage)
    {
        CloseAllUI();
        stageText.text = "Stage " + stage;
        ShowStageUI.SetActive(true);
    }

    public void CloseAllUI()
    {
        GameUI.SetActive(false);
        FailUI.SetActive(false);
        ClearUI.SetActive(false);
        ShowStageUI.SetActive(false);
    }
}