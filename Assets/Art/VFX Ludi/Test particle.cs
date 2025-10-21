using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testparticle : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps == null)
        {
            Debug.LogError("No se encontró un ParticleSystem en este GameObject.");
        }
    }

    void Update()
    {
        // Detecta si se presiona la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ps != null)
            {
                ps.Play(); // Inicia el efecto
            }
        }
    }
}

