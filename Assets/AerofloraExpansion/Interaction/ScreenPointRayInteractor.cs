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
            ray = GetRay();
            if (!Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance,layerMask)) return;
            foreach (var interactable in hitInfo.transform.gameObject.GetComponents<IInteractable>())
            {
                interactable.Interact();
            }
        }

        private Ray GetRay()
        {
            var mainCam = Camera.main;
            if (mainCam == null || !Application.isFocused) return new Ray();
            if (!mainCam.orthographic) return mainCam.ScreenPointToRay(Input.mousePosition);
            Vector3 rayOrigin = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane));
            return new Ray(rayOrigin, mainCam.transform.forward);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            var ray = GetRay();
            Gizmos.DrawLine(ray.origin, ray.direction*maxDistance);
        }
    }
}