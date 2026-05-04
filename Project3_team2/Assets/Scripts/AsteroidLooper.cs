using UnityEngine;

public class AsteroidLooper : MonoBehaviour
{
    public Transform player;

    public float speed = 50f;
    public float spawnZOffset = 200f;   
    public float despawnZOffset = -100f; 

    public float rotationSpeed = 200f;

    void Update()
    {
      
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // resets after going past player
        if (transform.position.z < player.position.z + despawnZOffset)
        {
            Reposition();
        }
    }

    void Reposition()
    {
        Vector3 pos = transform.position;

        
        pos.z = player.position.z + spawnZOffset;

        transform.position = pos;
    }
}