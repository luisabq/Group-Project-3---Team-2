using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    [Header("UI")]
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;

    [Header("Default Dialogue (Before Engines)")]
    [TextArea]
    public string[] lines;

    [Header("After Engines Dialogue")]
    [TextArea]
    public string[] afterEngineLines;

    [Header("1 Key Dialogue")]
    [TextArea]
    public string[] oneKeyLines;

    [Header("2 Keys Dialogue")]
    [TextArea]
    public string[] twoKeyLines;

    [Header("3 Keys Dialogue")]
    [TextArea]
    public string[] threeKeyLines;

    private int currentLine = 0;
    private bool isTalking = false;

    private string[] activeLines;

    private GameProgress progress;
    private PlayerMovement playerMovement;

    void Start()
    {
        progress = GameProgress.Instance;
        playerMovement = Object.FindFirstObjectByType<PlayerMovement>();
    }

    public void Interact()
    {

        activeLines = lines;

        if (progress != null)
        {
            if (progress.keysCollected >= 3 && threeKeyLines.Length > 0)
            {
                activeLines = threeKeyLines;
            }
            else if (progress.keysCollected == 2 && twoKeyLines.Length > 0)
            {
                activeLines = twoKeyLines;
            }
            else if (progress.keysCollected == 1 && oneKeyLines.Length > 0)
            {
                activeLines = oneKeyLines;
            }
        }

        else if (playerMovement != null && playerMovement.activeSteamReceptors >= 2 && afterEngineLines.Length > 0)
        {
            activeLines = afterEngineLines;
        }

        if (!isTalking)
        {
            StartDialogue();
        }
        else
        {
            NextLine();
        }
    }

    void StartDialogue()
    {
        isTalking = true;
        dialogueUI.SetActive(true);
        currentLine = 0;

        dialogueText.text = activeLines[currentLine];
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine >= activeLines.Length)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = activeLines[currentLine];
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }
}