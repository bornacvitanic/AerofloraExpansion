using UnityEngine;

namespace AerofloraExpansion.Wind.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class WindSoundModulator : WindAffected
    {
        [SerializeField]
        private AnimationCurve volumeCurve = new(new Keyframe(0, 0.1f), new Keyframe(1, 1.0f));
        // This curve defines the relationship between wind strength (0-1 normalized) and volume.
        // Adjust this curve in the inspector to achieve desired volume modulation.

        [SerializeField]
        private float maxWindStrength = 20f; // Define what maximum wind strength (for volume modulation) is expected
    
        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }
    
        public override void ApplyWindForce(Vector3 windDirection, float windStrength)
        {
            // Normalize the wind strength to a 0-1 range based on the defined max wind strength
            float normalizedStrength = Mathf.Clamp01(Mathf.Abs(windStrength) / maxWindStrength);

            // Sample the volume from the AnimationCurve based on the normalized wind strength
            audioSource.volume = volumeCurve.Evaluate(normalizedStrength);
        }
    }
}