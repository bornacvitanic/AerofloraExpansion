using UnityEngine;

namespace AerofloraExpansion.Wind.Components
{
    public class WindStrengthBasedRotation : WindAffected
    {
        // Rotation speed factor to control how fast the object rotates
        // This could be adjusted to make the object's rotation more or less sensitive to the wind strength
        [SerializeField] private float rotationSpeedFactor = 1.0f;

        public override void ApplyWindForce(Vector3 windDirection, float windStrength)
        {
            // Calculate the rotation amount based on wind strength
            float rotationAmount = windStrength * rotationSpeedFactor * Time.deltaTime;
            
            float dotProduct = Vector3.Dot(transform.forward, windDirection.normalized);

            // Apply the rotation around the Z-axis
            transform.Rotate(0, 0, rotationAmount * dotProduct);
        }
    }
}