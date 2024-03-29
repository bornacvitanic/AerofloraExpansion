using UnityEngine;

namespace AerofloraExpansion.Wind
{
    [RequireComponent(typeof(Rigidbody))]
    public class Uplift : MonoBehaviour
    {
        [SerializeField]
        private float liftForce = 9.81f; // Default to counteract standard gravity

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Inside FixedUpdate or Awake, to make the balloon hover without explicitly setting liftForce
            rb.AddForce(Vector3.up * (rb.mass * liftForce), ForceMode.Force);
        }
    }
}