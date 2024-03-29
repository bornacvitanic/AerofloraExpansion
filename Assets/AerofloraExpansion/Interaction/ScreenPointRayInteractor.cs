using AerofloraExpansion.Interaction.Interfaces;
using UnityEngine;

namespace AerofloraExpansion.Interaction
{
    public class ScreenPointRayInteractor : MonoBehaviour
    {
        [SerializeField] private float maxDistance = 5f;
        [SerializeField] private LayerMask layerMask = -1;

        private Ray ray;
        public void TriggerInteraction()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance,layerMask)) return;
            foreach (var interactable in hitInfo.transform.gameObject.GetComponents<IInteractable>())
            {
                interactable.Interact();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(ray.origin, ray.direction*maxDistance);
        }
    }
}