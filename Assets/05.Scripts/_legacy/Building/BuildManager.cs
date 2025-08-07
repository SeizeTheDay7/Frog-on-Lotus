using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] GameObject buildCamera;

    public void ChangeToBuildMode()
    {
        UIManager.Instance.SetBuildUI();
        buildCamera.SetActive(true);
    }
}