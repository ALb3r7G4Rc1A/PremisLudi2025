using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    [Header("Configuración del pulso")]
    public bool isActive = false;     // Activar o desactivar el efecto
    public float pulseSpeed = 5f;     // Velocidad del pulso
    public float pulseAmount = 0.1f;  // Qué tanto crece (por ejemplo, 0.1 = +10%)

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (!isActive)
        {
            // Vuelve suavemente a su tamaño normal cuando se desactiva
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 5f);
            return;
        }

        // Mathf.Sin() va de -1 a 1 → usamos Mathf.Abs() para que solo crezca (0 a 1)
        float pulse = Mathf.Abs(Mathf.Sin(Time.time * pulseSpeed));

        // Calculamos la escala (mínimo = 1, máximo = 1 + pulseAmount)
        float scaleMultiplier = 1f + pulse * pulseAmount;

        // Aplicamos la escala
        transform.localScale = originalScale * scaleMultiplier;
    }

    public void SetActive(bool value)
    {
        isActive = value;
    }
}