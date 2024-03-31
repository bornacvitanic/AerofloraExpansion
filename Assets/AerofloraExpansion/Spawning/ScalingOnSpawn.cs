using System.Collections;
using UnityEngine;

namespace AerofloraExpansion.Spawning
{
    public class ScalingOnSpawn : MonoBehaviour
    {
        [SerializeField] private AnimationCurve scaleCurve;

        private void Start()
        {
            StartCoroutine(ScaleOverTime());
        }

        private IEnumerator ScaleOverTime()
        {
            float currentTime = 0.0f;
            while (currentTime <= scaleCurve.keys[^1].time)
            {
                float curveValue = scaleCurve.Evaluate(currentTime / scaleCurve.keys[^1].time);
                transform.localScale = new Vector3(curveValue, curveValue, curveValue);
                currentTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
