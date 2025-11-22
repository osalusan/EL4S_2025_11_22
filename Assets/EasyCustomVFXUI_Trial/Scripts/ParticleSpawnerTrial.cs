using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawnerTrial : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab;

    public void SpawnParticle()
    {
        if (particlePrefab != null)
        {
            Instantiate(particlePrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Particle prefab is not assigned.");
        }
    }
}
