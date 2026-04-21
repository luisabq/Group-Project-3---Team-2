using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Respawn respawn = other.GetComponentInParent<Respawn>();

        if (respawn != null)
        {
            respawn.RespawnPlayer();
        }
    }
}