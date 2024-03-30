using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RandomEnviromentSpawner : MonoBehaviour
{
    [SerializeField] private Transform enviromentParent;
    [SerializeField] private int spawnCountAttempts = 10;
    [SerializeField] private List<GameObject> prefabsToSpawn;
    [SerializeField] private LayerMask layerMaskForSpawning = -1;
    [SerializeField] private Vector2 spawningBox;

    private void Start()
    {
        StartCoroutine(SpawnPrefabs());
    }

    private IEnumerator SpawnPrefabs()
    {
        for(int i = 0; i < spawnCountAttempts; i++)
        {
            SpawnPrefab();
        }
        yield return null;
    }

    private void SpawnPrefab()
    {
        Vector3 sPos = transform.position;
        float newX = sPos.x + Random.Range(-spawningBox.x / 2, spawningBox.x / 2);
        float newZ = sPos.z + Random.Range(-spawningBox.y / 2, spawningBox.y / 2);
        Vector3 pos = new Vector3(newX, sPos.y, newZ);

        RaycastHit hit;
        if(Physics.Raycast(pos, Vector3.down, out hit, 100, layerMaskForSpawning))
        {
            Instantiate(prefabsToSpawn[0], hit.point, Quaternion.identity, enviromentParent);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawningBox.x, 0, spawningBox.y));
    }
}
