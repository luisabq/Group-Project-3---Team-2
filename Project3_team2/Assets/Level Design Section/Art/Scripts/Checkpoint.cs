using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered checkpoint");
        Respawn respawn = other.GetComponentInParent<Respawn>();

        if (respawn != null)
        {
            Debug.Log("Respawn script found!");
            respawn.SetRespawnPoint(transform);
        }
    }
}