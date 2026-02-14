using System.Collections;
using UnityEngine;

public class CupPickUpTrigger : MonoBehaviour
{
    [SerializeField] private Light mainLight;
    [SerializeField] private GameObject monster;
    [SerializeField] private float lightDelay = 4.5f;
    [SerializeField] private float destroyDelay = 5f;
    [SerializeField] public AudioClip ligthOffClipSound;
    [SerializeField] public AudioSource audioSourceLigth;
    [SerializeField] public AudioSource LigthMainSoundOff;

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
        
        if (GameManager.Instance != null)
            GameManager.Instance.TurnLightOff();
        
        if (ligthOffClipSound != null && audioSourceLigth != null)
            audioSourceLigth.PlayOneShot(ligthOffClipSound);

        if (LigthMainSoundOff != null)
            LigthMainSoundOff.volume = 0f;
        
        if (monster != null)
            monster.SetActive(true);

        yield return new WaitForSeconds(destroyDelay - lightDelay);

        Destroy(cup);

        if (GameManager.Instance != null)
            GameManager.Instance.AddCoffee();

        yield return new WaitForSeconds(2f);
        
        if (GameManager.Instance != null)
            GameManager.Instance.TurnLightOn();

        if (LigthMainSoundOff != null)
            LigthMainSoundOff.volume = 0.5f;

        triggered = false;
    }

}
