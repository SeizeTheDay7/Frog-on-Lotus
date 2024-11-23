using UnityEngine;

public class PressAnyKeyStart : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.GameStart();
            Destroy(gameObject);
        }
    }
}