using UnityEngine;

public class CupPickUpTrigger : MonoBehaviour
{
    [SerializeField] public Cup _cup;
    private void OnTriggerEnter(Collider other)
    {
        _cup = other.GetComponent<Cup>();
        if (_cup.IsReady)
        {
            Destroy(_cup.gameObject, 5f);
        }
    }
}
