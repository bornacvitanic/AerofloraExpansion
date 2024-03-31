using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnviromentSpawner : MonoBehaviour
{
    [SerializeField] private Transform enviromentParent;
    [SerializeField] private int spawnCountAttempts = 10;
    [SerializeField] private List<GameObject> prefabsToSpawn;
    [SerializeField] private LayerMask layerMaskForSpawning = -1;
    [SerializeField] private Bounds spawningBounds;

    private void Start()
    {
        Spawn();
    }

    [ContextMenu("SpawnPrefabs")]
    public void Spawn()
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
        RaycastHit hit;
        if(Physics.Raycast(GetCalculatedRandomPosition(), Vector3.down, out hit, spawningBounds.extents.y, layerMaskForSpawning))
        {
            Instantiate(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)], hit.point, Quaternion.Euler(new Vector3(0,90*Random.Range(0,4), 0)), enviromentParent);
        }
    }

    private Vector3 GetCalculatedRandomPosition()
    {
        Vector3 sPos = transform.position;
        float newX = spawningBounds.center.x + sPos.x + Random.Range(-spawningBounds.extents.x, spawningBounds.extents.x);
        float newZ = spawningBounds.center.z + sPos.z + Random.Range(-spawningBounds.extents.z, spawningBounds.extents.z);
        return new Vector3(newX, sPos.y, newZ);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + spawningBounds.center - new Vector3(0, spawningBounds.size.y, 0), spawningBounds.size);
    }
}
    