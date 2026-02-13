using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class BlickLigth : MonoBehaviour
{
    [SerializeField] private float minFlickerTime = 0.1f;
    [SerializeField] private float maxFlickerTime = 0.2f;
    [SerializeField] private float minPauseTime = 1f;
    [SerializeField] private float maxPauseTime = 3f;
    [SerializeField] private float minligthIntensity;

    private float normalLigthIntensity = 0.9f;

    private Light _light;

    private void Start()
    {
        _light = GetComponent<Light>();
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minPauseTime, maxPauseTime));

            int flickerCount = Random.Range(1, 2);

            for (int i = 0; i < flickerCount; i++)
            {
                _light.intensity = minligthIntensity;
                yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));

                _light.intensity = normalLigthIntensity;
                yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            }
        }
    }
}