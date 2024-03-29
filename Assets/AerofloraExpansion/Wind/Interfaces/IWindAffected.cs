using UnityEngine;

namespace AerofloraExpansion.Wind.Interfaces
{
    public interface IWindAffected
    {
        public void ApplyWindForce(Vector3 windDirection, float windStrength);
    }
}