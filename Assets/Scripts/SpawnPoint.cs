using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float cooldownTime = 20f;
    public bool isAvailable = true;

    public void StartCooldown()
    {
        isAvailable = false;
        Invoke(nameof(EnableSpawn), cooldownTime);
    }

    void EnableSpawn()
    {
        isAvailable = true;
    }
}