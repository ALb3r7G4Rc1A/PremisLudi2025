using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCycle : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float speed = 1f;
    [Range(0f, 1f)]
    public float saturation = 1f;
    [Range(0f, 1f)]
    public float value = 1f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Gira el tono de color continuamente
        float hue = Mathf.Repeat(Time.time * speed, 1f);
        Color color = Color.HSVToRGB(hue, saturation, value);
        spriteRenderer.color = color;
    }
}
