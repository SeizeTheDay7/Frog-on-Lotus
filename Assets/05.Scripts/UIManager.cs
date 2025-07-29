using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetGameUI()
    {
        // 
    }

    public void SetFailUI()
    {

    }

    public void SetClearUI()
    {

    }

    public void CloseAllUI()
    {

    }
}