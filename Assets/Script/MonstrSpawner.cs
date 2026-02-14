using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterObject;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float soundDelay = 1f;
    [SerializeField] private float monsterDelay = 2f;

    private bool isRunning;

    public void ActivateMonster()
    {
        if (isRunning) return;

        StartCoroutine(ActivationRoutine());
    }

    IEnumerator ActivationRoutine()
    {
        isRunning = true;
        
        yield return new WaitForSeconds(soundDelay);
        
        if (audioSource != null)
            audioSource.Play();
        
        yield return new WaitForSeconds(monsterDelay);
        
        monsterObject.SetActive(true);

        isRunning = false;
    }
}