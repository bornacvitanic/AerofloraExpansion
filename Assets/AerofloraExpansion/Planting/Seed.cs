using System.Linq;
using UnityEngine;

namespace AerofloraExpansion.Planting
{
    [RequireComponent(typeof(Rigidbody))]
    public class Seed : MonoBehaviour
    {
        [SerializeField] private GameObject plantPrefabToSpawn;
        [SerializeField] private LayerMask layerMaskForPlanting;

        private void OnCollisionEnter(Collision collision)
        {
            if (plantPrefabToSpawn == null) return;
            if ((layerMaskForPlanting.value & (1 << collision.gameObject.layer)) == 0) return;

            Instantiate(plantPrefabToSpawn, collision.GetContact(0).point, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}