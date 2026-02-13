using System.Collections;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] private Transform cupPoint;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bipSound;
    [SerializeField] private AudioClip brewSound;
    [SerializeField] private float brewTime;

    public bool IsBrewing { get; private set; }
    public bool IsReady { get; private set; }

    private GameObject currentCup;

    public bool TryPlaceCup(GameObject cup)
    {
        if (IsBrewing || IsReady)
            return false;

        currentCup = cup;
        
        Collider col = cup.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Rigidbody rb = cup.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        cup.transform.SetParent(cupPoint);
        cup.transform.localPosition = Vector3.zero;
        cup.transform.localRotation = Quaternion.identity;

        StartCoroutine(Brew());
        return true;
    }

    IEnumerator Brew()
    {
        IsBrewing = true;

        if (audioSource && brewSound)
            audioSource.PlayOneShot(brewSound);

        yield return new WaitForSeconds(brewTime);

        IsBrewing = false;
        IsReady = true;

        if (currentCup != null)
        {
            Cup cupComp = currentCup.GetComponent<Cup>();
            if (cupComp != null)
            {
                cupComp.MarkReady();
            }
            
            Collider col = currentCup.GetComponent<Collider>();
            if (col != null) col.enabled = true;
            
            currentCup.transform.SetParent(cupPoint);
            currentCup.transform.localPosition = Vector3.zero;
            currentCup.transform.localRotation = Quaternion.identity;
        }
    }

    public GameObject TryTakeCoffee()
    {
        if (!IsReady)
            return null;

        IsReady = false;

        GameObject cup = currentCup;
        currentCup = null;
        
        Collider col = cup.GetComponent<Collider>();
        if (col != null) col.enabled = true;

        return cup;
    }
}