using System.Linq;
using UnityEngine;

namespace AerofloraExpansion.Planting
{
    [RequireComponent(typeof(Rigidbody))]
    public class Seed : MonoBehaviour
    {
        [SerializeField] private GameObject plantPrefabToSpawn;
        [SerializeField] private LayerMask layerMaskForPlanting = -1;

        private void OnCollisionEnter(Collision collision)
        {
            if (plantPrefabToSpawn == null) return;
            if ((layerMaskForPlanting.value & (1 << collision.gameObject.layer)) == 0) return;

            Instantiate(plantPrefabToSpawn, collision.GetContact(0).point, RandomRotationY());
            Destroy(gameObject);
        }

        private Quaternion RandomRotationY()
        {
            float randomY = 90 * Random.Range(0, 3);
            Vector3 rotation = new Vector3(0, randomY, 0);
            return Quaternion.Euler(rotation);
        }
    }
}