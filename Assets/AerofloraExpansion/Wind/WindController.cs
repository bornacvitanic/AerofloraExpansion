using UnityEngine;

namespace AerofloraExpansion.Wind
{
    public class WindController : MonoBehaviour
    {
        [SerializeField] private WindZone windZone;
        [SerializeField] private float baseWindStrength = 5.0f;
        [SerializeField] private float turbulenceStrength = 1.0f;
        [SerializeField] private float directionChangeSpeed = 1.0f;

        public Vector3 WindDirection { get; private set; }
        public float WindStrength => windZone.windMain;
        
        private float windStrength;
        private float lastWindStrength;

        private void Start()
        {
            if (windZone == null)
            {
                Debug.LogError("WindZone reference not set.");
                return;
            }

            // Initialize with the WindZone's current direction and strength
            WindDirection = windZone.transform.forward;
            windStrength = baseWindStrength;
        }

        private void Update()
        {
            SimulateWind();
            ApplyWindToZone();
        }

        private void SimulateWind()
        {
            // Create a smooth transition for wind direction and strength changes
            float time = Time.time;
            WindDirection = new Vector3(
                Mathf.PerlinNoise(time, 0) * 2 - 1,
                0, // Assuming wind only changes direction on the horizontal plane
                Mathf.PerlinNoise(time, 1) * 2 - 1
            ).normalized;

            windStrength = baseWindStrength + (Mathf.PerlinNoise(time, 2) * 2 - 1) * turbulenceStrength;
        }

        private void ApplyWindToZone()
        {
            windZone.transform.rotation = Quaternion.Lerp(windZone.transform.rotation, Quaternion.LookRotation(WindDirection), Time.deltaTime * directionChangeSpeed);
            windZone.windMain = Mathf.Lerp(lastWindStrength, windStrength, Time.deltaTime * directionChangeSpeed);

            // Update last values for smooth interpolation
            lastWindStrength = windStrength;
        }
        
        public Vector3 GetCurrentWindVector()
        {
            return WindDirection * WindStrength;
        }
    }
}