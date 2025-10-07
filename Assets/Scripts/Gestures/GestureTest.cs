using UnityEngine;
using System.Collections.Generic;
using PDollarGestureRecognizer;
using PDollarDemo; // Namespace del reconeixedor

public class GestureTest : MonoBehaviour
{
    public LineRenderer lineRenderer; // Assigna el LineRenderer al GameObject
    private List<Vector3> positions = new List<Vector3>();
    private List<Point> points = new List<Point>();
    private Gesture[] trainingSet;
    private int strokeId = -1;

    void Start()
    {
        // Carrega els gestos d'entrenament des de Resources/
        trainingSet = GestureIO.LoadGestures();
        Debug.Log("Gestos carregats: " + trainingSet.Length);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            strokeId++;
            positions.Clear();
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            positions.Add(mousePos);
            points.Add(new Point(mousePos.x, -mousePos.y, strokeId));
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }

        if (Input.GetMouseButtonUp(0))
        {
            Gesture candidate = new Gesture(points.ToArray());
            Result result = PointCloudRecognizer.Classify(candidate, trainingSet);

            Debug.Log($"Gest reconegut: {result.GestureClass} ({result.Score * 100f:F1}% confiança)");
        }
    }
}
