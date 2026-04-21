using UnityEngine;

public class Respawn : MonoBehaviour
{

    public float killHeight = -10f;

    public Transform currentRespawnPoint;
    public Transform startSpawn;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (currentRespawnPoint == null && startSpawn != null)
            currentRespawnPoint = startSpawn;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void FixedUpdate()
    {
        if (transform.position.y < killHeight)
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        if (currentRespawnPoint == null)
        {
            Debug.LogWarning("No respawn point set!");
            return;
        }
        transform.position = currentRespawnPoint.position;
        transform.rotation = currentRespawnPoint.rotation;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }



    }

    public void SetRespawnPoint(Transform newPoint)
    {
        currentRespawnPoint = newPoint;
        Debug.Log("Checkpoint: " + newPoint.name);
    }

}
