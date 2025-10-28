using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections.Generic;
using System.IO;

[RequireComponent(typeof(LineRenderer))]
public class GestureTest : MonoBehaviour
{
    private List<Point> points = new List<Point>();
    private Gesture[] trainingSet;
    private bool isDrawing = false;

    private LineRenderer lineRenderer;
    private Color originalColor;
    [Header("Streak")]
    public bool streak;
    public Material streakMaterial;
    private Material originalMaterial;

    [Header("Ajustos del traç")]
    public Color lineColor;
    public float lineWidth = 0.15f;
    public float recognitionThreshold = 0.5f; //Marge de detecció de la figura

    [Header("Referència al GameManager")]
    public GameManager gameManager;

    private string recognized;

    void Start()
    {
        // Inicialitzar LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        originalMaterial = lineRenderer.material;
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.numCapVertices = 10;

        originalColor = lineColor;
        lineRenderer.startColor = originalColor;
        lineRenderer.endColor = originalColor;     

        // Carregar tots els gestos .xml del projecte
        string[] gestureFiles = Directory.GetFiles(Application.dataPath, "*.xml", SearchOption.AllDirectories);
        trainingSet = new Gesture[gestureFiles.Length];
        for (int i = 0; i < gestureFiles.Length; i++)
            trainingSet[i] = GestureIO.ReadGestureFromFile(gestureFiles[i]);

        //Debug.Log($"Gestos carregats: {trainingSet.Length}");
    }

    void Update()
    {
        if (streak)
        {
            lineRenderer.material = streakMaterial; // NOT WORKING
        }
        else
        {
            lineRenderer.material = originalMaterial;
        }
        // Comença un nou dibuix
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            points.Clear();
            lineRenderer.positionCount = 0;
            if (!streak)
            {
                lineRenderer.startColor = originalColor;
                lineRenderer.endColor = originalColor;
                lineRenderer.materials[0] = originalMaterial;
            }
        }

        // Dibuixar mentre es manté el clic
        if (isDrawing && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            points.Add(new Point(mousePos.x, -mousePos.y, 0));

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);
        }

        // Quan deixem anar el clic, reconeixem el gest
        if (isDrawing && Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            Gesture candidate = new Gesture(points.ToArray());
            Result result = PointCloudRecognizer.Classify(candidate, trainingSet);

            recognized = result.GestureClass;

            if (result.Score >= recognitionThreshold && result.Score != 1)
            {
                //Debug.Log($"Gest reconegut: {recognized} (Score: {result.Score:F2})");

                // Cridar funció del GameManager segons el gest reconegut
                if (gameManager != null)
                {
                    switch (recognized)
                    {
                        case "accent obert":
                            gameManager.OptionPress(1);
                            break;

                        case "cercle":
                            gameManager.OptionPress(2);
                            break;

                        case "accent tancat":
                            gameManager.OptionPress(3);
                            break;

                        default:
                            //Debug.Log("No hi ha acció assignada a aquest gest.");
                            break;
                    }
                }
                else
                {
                    //Debug.LogWarning("No s'ha assignat cap GameManager al GestureTest.");
                }
            }
            else
            {
                //Debug.Log($"No s'ha reconegut cap figura. (Score: {result.Score:F2})");
                recognized = "";
            }

            // Tota la vaina del fade del traç
            StartCoroutine(FadeLine());
        }
    }

    // Fa que el traç es dissolgui suaument després del dibuix
    private System.Collections.IEnumerator FadeLine()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Color startFadeColor = lineRenderer.startColor;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = 1f - (elapsed / duration);
            Color faded = startFadeColor;
            faded.a = t;
            lineRenderer.startColor = faded;
            lineRenderer.endColor = faded;
            yield return null;
        }

        // Reiniciar el LineRenderer per al següent dibuix
        lineRenderer.positionCount = 0;
        if (!streak)
        {
            lineRenderer.startColor = originalColor;
            lineRenderer.endColor = originalColor;
        }        
    }
}
