using UnityEngine;

public class PressAnyKeyStart : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StageManager.Instance.StageStart();
            Destroy(gameObject);
        }
    }
}