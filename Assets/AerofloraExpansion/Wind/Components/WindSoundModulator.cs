using UnityEngine;

namespace AerofloraExpansion.Wind.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class WindSoundModulator : WindAffected
    {
        [SerializeField]
        private float minVolume = 0.1f; // Minimum volume, at no wind

        [SerializeField]
        private float maxVolume = 1.0f; // Maximum volume, at max wind strength

        [SerializeField]
        private float maxWindStrength = 20f; // Define what maximum wind strength (for volume) is expected
        
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        public override void ApplyWindForce(Vector3 windDirection, float windStrength)
        {
            // Normalize the wind strength to a 0-1 range based on the defined max wind strength
            float normalizedStrength = Mathf.Clamp01(windStrength / maxWindStrength);

            // Lerp the volume based on the normalized wind strength
            audioSource.volume = Mathf.Lerp(minVolume, maxVolume, normalizedStrength);
        }
    }
}