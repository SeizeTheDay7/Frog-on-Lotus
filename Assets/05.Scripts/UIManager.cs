using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Transform UICanvas;
    [SerializeField] GameObject GameUI_Hunt;
    [SerializeField] GameObject GameUI_Build;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    public void CloseAllUI()
    {
        for (int i = 0; i < UICanvas.childCount; i++)
            UICanvas.GetChild(i).gameObject.SetActive(false);
    }

    public void SetHuntUI()
    {
        CloseAllUI();
        GameUI_Hunt.SetActive(true);
    }

    public void SetBuildUI()
    {
        CloseAllUI();
        GameUI_Build.SetActive(true);
    }

    public void SetFailUI()
    {

    }

    public void SetClearUI()
    {

    }
}