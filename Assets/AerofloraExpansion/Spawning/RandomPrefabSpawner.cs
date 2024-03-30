using System.Collections.Generic;
using UnityEngine;

namespace AerofloraExpansion.Spawning
{
    public class RandomPrefabSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> prefabsToSpawn = new();

        [SerializeField] private Transform spawnOrigin;
        
        [ContextMenu("Spawn")]
        public void Spawn()
        {
            if (prefabsToSpawn.Count == 0)
            {
                Debug.LogWarning("No prefabs assigned to spawn.");
                return;
            }

            if (spawnOrigin == null)
            {
                Debug.LogWarning("Spawn origin not set. Using spawner's position.");
                spawnOrigin = this.transform;
            }
            
            int randomIndex = Random.Range(0, prefabsToSpawn.Count);
            GameObject selectedPrefab = prefabsToSpawn[randomIndex];
            
            Instantiate(selectedPrefab, spawnOrigin.position, spawnOrigin.rotation);
        }
    }
}