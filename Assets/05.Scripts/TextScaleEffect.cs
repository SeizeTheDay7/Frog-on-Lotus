
using UnityEngine;
using TMPro;

public class TextScaleEffect : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public float scaleSpeed = 3f;
    public float scaleAmount = 1.05f;

    private Vector3 originalScale;

    private void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        originalScale = tmpText.transform.localScale;
    }

    private void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * scaleSpeed) * (scaleAmount - 1);
        tmpText.transform.localScale = originalScale * scale;
    }
}