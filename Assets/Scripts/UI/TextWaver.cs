using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextWaveEffectUI : MonoBehaviour
{
    [Header("Wave Settings")]
    public float amplitude = 5f;      // Altura de la ola
    public float frequency = 2f;      // Separación entre letras
    public float speed = 2f;          // Velocidad del movimiento

    private TextMeshProUGUI tmpText;
    private TMP_TextInfo textInfo;
    private Vector3[][] originalVertices;

    void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        RefreshMeshData();
    }

    void OnValidate()
    {
        if (tmpText != null)
            RefreshMeshData();
    }

    void RefreshMeshData()
    {
        tmpText.ForceMeshUpdate();
        textInfo = tmpText.textInfo;
        originalVertices = new Vector3[textInfo.meshInfo.Length][];

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var vertices = textInfo.meshInfo[i].vertices;
            originalVertices[i] = new Vector3[vertices.Length];
            System.Array.Copy(vertices, originalVertices[i], vertices.Length);
        }
    }

    void Update()
    {
        if (tmpText == null) return;

        tmpText.ForceMeshUpdate();
        textInfo = tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            int meshIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;
            Vector3[] vertices = textInfo.meshInfo[meshIndex].vertices;

            float offsetY = Mathf.Sin(Time.time * speed + i * frequency) * amplitude;

            for (int j = 0; j < 4; j++)
            {
                vertices[vertexIndex + j] = originalVertices[meshIndex][vertexIndex + j] + new Vector3(0, offsetY, 0);
            }
        }

        // Actualizar el mesh
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            tmpText.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
