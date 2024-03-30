using UnityEngine;

namespace AerofloraExpansion.Wind.Components
{
    [RequireComponent(typeof(Renderer))]
    public class WindAffectedRendererMaterial : WindAffected
    {
        public Renderer treeRenderer;
        
        private static readonly int WindOffset = Shader.PropertyToID("_WindOffset");
        
        private Vector3 accumulatedWindOffset = Vector3.zero;

        public override void ApplyWindForce(Vector3 windDirection, float windStrength)
        {
            if (treeRenderer == null) return;

            // Accumulate offset based on the current wind direction and strength
            // Adjust the accumulation rate to fit your game's scale and speed
            accumulatedWindOffset += windDirection * (windStrength * Time.deltaTime);

            treeRenderer.material.SetVector(WindOffset, accumulatedWindOffset);
        }
    }
}