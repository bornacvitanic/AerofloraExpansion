using AerofloraExpansion.Interaction.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace AerofloraExpansion.Interaction
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        public UnityEvent OnInteracted;
        public void Interact()
        {
            OnInteracted?.Invoke();
        }
    }
}