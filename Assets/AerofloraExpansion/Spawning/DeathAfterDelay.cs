using UnityEngine;

namespace AerofloraExpansion.Spawning
{
    public class DeathAfterDelay : MonoBehaviour
    {
        [SerializeField] private float delay;

        private void Start()
        {
            Destroy(gameObject, delay);
        }
    }
}