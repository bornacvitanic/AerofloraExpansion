using UnityEngine;

namespace AerofloraExpansion.Wind
{
    public class WindAffectedRendererMaterial : MonoBehaviour
    {
        public WindController windController;
        public Renderer treeRenderer; // Assuming this is the renderer of your voxel tree
        
        private static readonly int WindDirection = Shader.PropertyToID("_WindDirection");
        private static readonly int WindStrength = Shader.PropertyToID("_WindStrength");

        void Update()
        {
            if (windController == null || treeRenderer == null) return;
            
            Vector3 windDirection = windController.WindDirection;
            float windStrength = windController.WindStrength;

            treeRenderer.material.SetVector(WindDirection, windDirection);
            treeRenderer.material.SetFloat(WindStrength, windStrength);
        }
    }
}