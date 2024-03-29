using UnityEngine;

namespace AerofloraExpansion.Wind.Components
{
    [RequireComponent(typeof(Renderer))]
    public class WindAffectedRendererMaterial : WindAffected
    {
        public Renderer treeRenderer;
        
        private static readonly int WindDirection = Shader.PropertyToID("_WindDirection");
        private static readonly int WindStrength = Shader.PropertyToID("_WindStrength");

        public override void ApplyWindForce(Vector3 windDirection, float windStrength)
        {
            if (treeRenderer == null) return;

            treeRenderer.material.SetVector(WindDirection, windDirection);
            treeRenderer.material.SetFloat(WindStrength, windStrength);
        }
    }
}