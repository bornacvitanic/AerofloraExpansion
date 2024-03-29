using UnityEngine;

namespace AerofloraExpansion.Wind.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class WindAffectedRigidbody : WindAffected
    {
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public override void ApplyWindForce(Vector3 windDirection, float windStrength)
        {
            rb.AddForce(windDirection * windStrength, ForceMode.Force);
        }
    }
}