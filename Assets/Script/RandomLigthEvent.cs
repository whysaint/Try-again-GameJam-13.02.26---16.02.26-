using System.Collections;
using UnityEngine;

public class RandomLightEvent : MonoBehaviour
{
    [SerializeField] private float minInterval = 8f;
    [SerializeField] private float maxInterval = 20f;

    [SerializeField] private float minOffTime = 3f;
    [SerializeField] private float maxOffTime = 6f;

    [SerializeField] private float timeDelay = 0.5f;

    [SerializeField] private AudioSource ligthOffSound;

    private void Start()
    {
        StartCoroutine(LightLoop());
    }

    private IEnumerator LightLoop()
    {
        yield return new WaitUntil(() => 
            GameManager.Instance != null && 
            GameManager.Instance.coffeeCount >= 1);

        while (true)
        {
            float wait = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(wait);
            
            GameManager.Instance.StartDarkness();

            if (ligthOffSound != null)
                ligthOffSound.Play();
            
            yield return new WaitForSeconds(timeDelay);
            
            GameManager.Instance.EnableDeadlyDarkness();

            float offTime = Random.Range(minOffTime, maxOffTime);
            yield return new WaitForSeconds(offTime - timeDelay);

            GameManager.Instance.EndDarkness();
        }
    }

}