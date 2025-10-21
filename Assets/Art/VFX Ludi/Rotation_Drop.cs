using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Drop : MonoBehaviour
{
    [Tooltip("Punto hacia el que se orientan las partículas (por ejemplo, un objeto en el centro).")]
    public Transform centerPoint;

    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void LateUpdate()
    {
        if (centerPoint == null) return;

        // Asegura que el array tenga el tamaño adecuado
        if (particles == null || particles.Length < ps.main.maxParticles)
            particles = new ParticleSystem.Particle[ps.main.maxParticles];

        int count = ps.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            Vector3 dir = centerPoint.position - particles[i].position;

            // Calcula el ángulo Z en grados (en el plano XY)
            float angleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Aplica solo la rotación en Z (manteniendo X e Y sin tocar)
            Vector3 rot = particles[i].rotation3D;
            rot.z = angleZ; // rota solo en Z
            particles[i].rotation3D = rot;
        }

        ps.SetParticles(particles, count);
    }
}
