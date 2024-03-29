using UnityEngine;

namespace AerofloraExpansion.Wind
{
    [RequireComponent(typeof(Rigidbody))]
    public class WindAffectedRigidbody : MonoBehaviour
    {
        [SerializeField] private WindController windController;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (windController == null)
            {
                Debug.LogError("WindController reference not set.");
                return;
            }

            ApplyWindForce();
        }

        private void ApplyWindForce()
        {
            // Apply a force based on the wind direction and strength
            rb.AddForce(windController.GetCurrentWindVector(), ForceMode.Force);
        }
    }
}