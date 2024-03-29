using AerofloraExpansion.Wind.Interfaces;
using UnityEngine;

namespace AerofloraExpansion.Wind
{
    public abstract class WindAffected : MonoBehaviour, IWindAffected
    {
        private void Awake()
        {
            WindTransmitter.Instance.Add(this);
        }

        public abstract void ApplyWindForce(Vector3 windDirection, float windStrength);

        private void OnDestroy()
        {
            WindTransmitter.Instance.Remove(this);
        }
    }
}