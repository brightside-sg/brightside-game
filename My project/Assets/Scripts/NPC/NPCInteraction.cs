using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public DialogueManager dialogueManager; // Reference to the dialogue system
    public string npcName = "Shopkeeper"; // Future-proofing for multiple NPCs
    public string[] npcDialogue; // NPC-specific dialogue
    public string[] npcResponses; // Player response options

    private bool dialogueTriggered = false; // Ensures dialogue only plays once

    void Start()
    {
        // Get reference to DialogueManager if not set manually
        if (dialogueManager == null)
        {
            dialogueManager = FindFirstObjectByType<DialogueManager>(); // Updated method
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueTriggered)
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        if (dialogueManager != null)
        {
            dialogueManager.ShowDialogue(npcName, npcDialogue, npcResponses ?? new string[0]);
            dialogueTriggered = true; // Prevents re-triggering
        }
    }
}
