using UnityEngine;
using PDollarGestureRecognizer;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
public class GestureTestTutorial : MonoBehaviour
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
    public GameManagerTutorial gameManager;

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

        StartCoroutine(LoadGestures());
    }

    private IEnumerator LoadGestures()
    {
        string gesturePath = Application.streamingAssetsPath;
        string[] gestureFiles;

        #if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL – manually list files (they must be inside Assets/StreamingAssets)
        gestureFiles = new string[] { "AccentObert.xml", "AccentTancat.xml", "Circle.xml", "Punt.xml" };

        trainingSet = new Gesture[gestureFiles.Length];

        for (int i = 0; i < gestureFiles.Length; i++)
        {
            string path = Path.Combine(gesturePath, gestureFiles[i]);
            UnityWebRequest www = UnityWebRequest.Get(path);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                string xml = www.downloadHandler.text;
                trainingSet[i] = GestureIO.ReadGestureFromXML(xml); // Read from XML text
            }
            else
            {
            }
        }
        #else
                // Editor / standalone
                gestureFiles = Directory.GetFiles(gesturePath, "*.xml", SearchOption.AllDirectories);
                trainingSet = new Gesture[gestureFiles.Length];
                for (int i = 0; i < gestureFiles.Length; i++)
                {
                    trainingSet[i] = GestureIO.ReadGestureFromFile(gestureFiles[i]);
                }
#endif

        Debug.Log($"Gestos carregats: {trainingSet.Length}");
        yield break;
    }

    void Update()
    {
        if (streak)
        {
            lineRenderer.material = streakMaterial;
        }
        else
        {
            lineRenderer.material = originalMaterial;
        }
        // Comença un nou dibuix
        if (Input.GetMouseButtonDown(0)  || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            isDrawing = true;
            points.Clear();
            lineRenderer.positionCount = 0;
            if (!streak)
            {
                lineRenderer.startColor = originalColor;
                lineRenderer.endColor = originalColor;
            }
        }

        // Dibuixar mentre es manté el clic
        if (isDrawing && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)))
        {
            Vector2 mousePos = Input.mousePosition;
            points.Add(new Point(mousePos.x, -mousePos.y, 0));

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);
        }

        // Quan deixem anar el clic, reconeixem el gest
        if (isDrawing && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
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
                        case "accent_obert":
                            gameManager.OptionPress(1);
                            break;

                        case "cercle":
                            gameManager.OptionPress(2);
                            break;

                        case "accent_tancat":
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
