using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SteeringWheel : MonoBehaviour, IInteractable
{
    public PlayerMovement playerMovement;

    public float delay = 2f;

    public void Interact()
    {
        if (playerMovement.activeSteamReceptors < 2)
        {
            Debug.Log("Engines aren't powered yet!");
            return;
        }

        if (!MapSystem.HasSelection())
        {
            Debug.Log("No level selected.");
            return;
        }

        StartCoroutine(Travel());
    }

    IEnumerator Travel()
    {
        Debug.Log("Traveling to: " + MapSystem.selectedLevel);

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(MapSystem.selectedLevel);
    }
}