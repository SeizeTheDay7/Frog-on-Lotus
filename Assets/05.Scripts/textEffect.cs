
using UnityEngine;
using TMPro;

public class WaveEffect : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public float waveSpeed = 2f;
    public float amplitude = 5f;

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        tmpText.ForceMeshUpdate();
        var textInfo = tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            var verts = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            for (int j = 0; j < 4; j++)
            {
                Vector3 offset = new Vector3(0, Mathf.Sin(Time.time * waveSpeed + i) * amplitude, 0);
                verts[vertexIndex + j] += offset;
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }
    }
}