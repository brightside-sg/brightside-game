using UnityEngine;
using System.Collections.Generic;

public class NPCInteraction : MonoBehaviour
{
    public DialogueManager dialogueManager; // Reference to the dialogue system
    public string npcName; // Set in Unity for each NPC

    private bool dialogueTriggered = false;

    void Start()
    {
        if (dialogueManager == null)
        {
            dialogueManager = FindFirstObjectByType<DialogueManager>();
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
            DialogueData npcDialogue = DialogueDatabase.GetDialogue(npcName);
            dialogueManager.StartDialogue(npcDialogue.npcName, npcDialogue.dialogueSteps);
            dialogueTriggered = true;
        }
    }
}