using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Displays NPC's dialogue
    public GameObject dialogueBox; // Holds the dialogue UI
    public GameObject responsePanel; // Holds response buttons
    public Button responseButtonPrefab; // Prefab for response buttons
    public Transform responseContainer; // Where buttons will be spawned

    private List<Button> activeButtons = new List<Button>(); // Keep track of active buttons
    private Queue<string> sentences; // Stores multiple lines of NPC dialogue
    private bool awaitingResponse = false; // Prevents skipping over responses

    void Start()
    {
        dialogueBox.SetActive(false); // Hide dialogue UI at the beginning
        responsePanel.SetActive(false); // Hide response panel at the beginning
        sentences = new Queue<string>(); // Initialize the queue
    }

    public void ShowDialogue(string npcName, string[] npcDialogue, string[] responses)
    {
        Time.timeScale = 0f; // Pause the game when dialogue starts
        dialogueBox.SetActive(true);
        sentences.Clear();

        // Add all NPC dialogue lines to the queue
        foreach (string line in npcDialogue)
        {
            sentences.Enqueue(line);
        }

        StartCoroutine(DisplayDialogue(responses ?? new string[0])); // Ensure a valid array
    }

    IEnumerator DisplayDialogue(string[] responses)
    {
        awaitingResponse = false; // Ensure no response selection yet

        while (sentences.Count > 0)
        {
            dialogueText.text = sentences.Dequeue();

            // Wait for Space key to be pressed, ensuring only one press is counted
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space)); // Wait for release
        }

        // After all dialogue lines, show response options (if any)
        if (responses.Length > 0)
        {
            ShowResponses(responses);
        }
        else
        {
            EndDialogue(); // If no responses, end dialogue
        }
    }

    void ShowResponses(string[] responses)
    {
        awaitingResponse = true;
        responsePanel.SetActive(true);
        ClearPreviousResponses();

        foreach (string response in responses)
        {
            Button newButton = Instantiate(responseButtonPrefab, responseContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = response;
            newButton.onClick.AddListener(() => SelectResponse(response));
            activeButtons.Add(newButton);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(responseContainer.GetComponent<RectTransform>());
    }

    void SelectResponse(string response)
    {
        if (!awaitingResponse) return; // Prevent interaction if not expecting response

        Debug.Log("Player selected: " + response);

        // Save the player's choice for future use
        PlayerPrefs.SetString("LastChoice", response);
        PlayerPrefs.Save();

        EndDialogue();
    }

    void EndDialogue()
    {
        Time.timeScale = 1f; // Resume the game when dialogue ends
        dialogueBox.SetActive(false);
        responsePanel.SetActive(false);
        awaitingResponse = false;
        ClearPreviousResponses();
    }

    void ClearPreviousResponses()
    {
        foreach (Button button in activeButtons)
        {
            Destroy(button.gameObject);
        }
        activeButtons.Clear();
    }
}
