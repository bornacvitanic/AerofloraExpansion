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

        private Vector3 windDirection;
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
        
        private Vector3 targetWindDirection;
        private float directionChangeFrequency = 0.1f; // How quickly the wind direction can change
        private float lastDirectionUpdateTime = 0;
        private Vector2 perlinOffset = new(100, 200); // Arbitrary offsets for Perlin noise
        private void SimulateWind()
        {
            float timeSinceLastUpdate = Time.time - lastDirectionUpdateTime;

            if (timeSinceLastUpdate > directionChangeSpeed)
            {
                Vector2 newDirection = Random.insideUnitCircle;

                targetWindDirection = new Vector3(newDirection.x, 0, newDirection.y).normalized; 

                lastDirectionUpdateTime = Time.time;
            }

            windDirection = Vector3.Lerp(WindDirection, targetWindDirection, timeSinceLastUpdate / directionChangeSpeed).normalized;

            windStrength = baseWindStrength + (Mathf.PerlinNoise(Time.time * directionChangeFrequency + perlinOffset.x, perlinOffset.y) * 2 - 1) * turbulenceStrength;
        }

        private void ApplyWindToZone()
        {
            WindDirection = Vector3.Lerp(windZone.transform.forward, windDirection,
                Time.deltaTime * directionChangeSpeed);
            windZone.transform.rotation = Quaternion.LookRotation(WindDirection);
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