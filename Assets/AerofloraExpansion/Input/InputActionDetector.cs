using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace AerofloraExpansion.Input
{
    public class InputActionDetector : MonoBehaviour
    {
        [SerializeField] private InputActionReference inputActionReference;

        public UnityEvent OnPerformed;

        private void OnEnable()
        {
            inputActionReference.action.Enable();
            inputActionReference.action.performed += Performed;
        }

        private void OnDisable()
        {
            inputActionReference.action.performed -= Performed;
        }

        private void Performed(InputAction.CallbackContext obj)
        {
            OnPerformed?.Invoke();
        }
    }
}