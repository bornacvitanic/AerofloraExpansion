using UnityEngine;

namespace AerofloraExpansion.Wind.Components
{
    public class FaceWindDirection : WindAffected
    {
        public override void ApplyWindForce(Vector3 windDirection, float windStrength)
        {
            // Normalize the wind direction to get a direction vector
            Vector3 normalizedWindDirection = windDirection.normalized;

            // Determine the target rotation for the GameObject to face the wind direction.
            // Quaternion.LookRotation generates a quaternion looking in the direction of the provided vector.
            Quaternion targetRotation = Quaternion.LookRotation(normalizedWindDirection, Vector3.up);

            // Apply the rotation to the GameObject.
            // Here, you can directly set the rotation or use a smoother transition like Lerp or Slerp for visual effect, especially if wind changes are frequent.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * windStrength);
        }
    }
}