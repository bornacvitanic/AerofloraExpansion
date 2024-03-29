using UnityEngine;

namespace AerofloraExpansion.Planting
{
    // Collider needed for colliding, rigidbody needed for physics
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Seed : MonoBehaviour
    {
        [SerializeField] private GameObject plantPrefabToSpawn;

        private void OnCollisionEnter(Collision collision)
        {
            Instantiate(plantPrefabToSpawn, collision.GetContact(0).point, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}