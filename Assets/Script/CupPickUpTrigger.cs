using System.Collections;
using UnityEngine;

public class CupPickUpTrigger : MonoBehaviour
{
    [SerializeField] private Light mainLight;
    [SerializeField] private GameObject monster;
    [SerializeField] private float lightDelay = 4.5f;
    [SerializeField] private float destroyDelay = 5f;
    [SerializeField] public AudioClip ligthOff;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioSource LigthMain;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Cup cup = other.GetComponent<Cup>();
        if (cup == null) return;
        if (!cup.IsReady) return;

        triggered = true;
        StartCoroutine(HandleCupEvent(cup.gameObject));
    }

    private IEnumerator HandleCupEvent(GameObject cup)
    {
        yield return new WaitForSeconds(lightDelay);

        if (mainLight != null)
            mainLight.enabled = false;
        
        if (ligthOff != null && audioSource != null && LigthMain != null)
        {
            audioSource.PlayOneShot(ligthOff);
            LigthMain.gameObject.SetActive(false);
        }

            
        
        yield return new WaitForSeconds(destroyDelay - lightDelay);

        Destroy(cup);
        
        if (monster != null)
            monster.SetActive(false);
        
        yield return new WaitForSeconds(2f);
        
        if (mainLight != null)
            mainLight.enabled = true;
        
        LigthMain.gameObject.SetActive(true);
    }
}