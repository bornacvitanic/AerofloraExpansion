using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AerofloraExpansion.Planting
{
    public class SeedDispenser : MonoBehaviour
    {
        [SerializeField] private List<GameObject> seedPrefabs; // Better to be a SO
        [SerializeField] private float seedSpawnInterval;
        [SerializeField] private float seedInitialVelocity;
        [SerializeField] private Transform origin;

        private Coroutine _seedSpawningCoroutine;

        private void OnValidate()
        {
            for (int i = seedPrefabs.Count - 1; i >= 0; i--)
            {
                Seed _seedPrefab;
                if (!seedPrefabs[i].TryGetComponent(out _seedPrefab))
                    seedPrefabs.RemoveAt(i);
            }
        }

        [ContextMenu("StartDispensing")]
        public void StartDispensing()
        {
            if (seedPrefabs == null)
            {
                Debug.LogWarning("Seed prefab is not assigned in Seed Dispenser.");
                return;
            }
            
            if (_seedSpawningCoroutine != null) return;

            // Start spawning seeds
            _seedSpawningCoroutine = StartCoroutine(SpawnSeedRoutine());
        }

        [ContextMenu("StopDispensing")]
        public void StopDispensing()
        {
            if (_seedSpawningCoroutine == null) return;
            StopCoroutine(_seedSpawningCoroutine);
        }

        private IEnumerator SpawnSeedRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(seedSpawnInterval);

                SpawnSeed();
            }
        }

        private void SpawnSeed()
        {
            if (seedPrefabs == null)
            {
                Debug.LogWarning("Seed prefab is not assigned in Seed Dispenser.");
                return;
            }

            // coneDirection is a unit Vector3 that has a -y from origin. This can be replaced with a vector3 perhaps to have flexibility
            Vector3 coneDirection = Random.onUnitSphere;
            coneDirection.y = -Mathf.Abs(coneDirection.y);

            Vector3 initialVelocity = coneDirection * seedInitialVelocity;

            // Instantiate a random seed from list
            GameObject newSeed = Instantiate(seedPrefabs[Random.Range(0, seedPrefabs.Count - 1)], origin.position, Quaternion.identity);


            // Apply initial velocity
            Rigidbody seedRigidbody;
            if (newSeed.TryGetComponent(out seedRigidbody))
            {
                seedRigidbody.velocity = initialVelocity;
            }
        }
    }
}