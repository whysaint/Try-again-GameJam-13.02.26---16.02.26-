using UnityEngine;

public class GameEvent : MonoBehaviour
{
    [SerializeField] private MonsterSpawner enemy;
    [SerializeField] private float activateAfterSeconds = 10f;

    private bool activated;

    private void Update()
    {
        if (activated) return;

        if (Time.time >= activateAfterSeconds)
        {
            enemy.ActivateMonster();
            activated = true;
        }
    }
}