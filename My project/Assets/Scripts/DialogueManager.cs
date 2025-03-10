using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Displays NPC's dialogue
    public GameObject responsePanel; // Holds response buttons
    public Button responseButtonPrefab; // Prefab for response buttons
    public Transform responseContainer; // Where buttons will be spawned

    private List<Button> activeButtons = new List<Button>(); // Keep track of active buttons

    void Start()
    {
        // Example NPC dialogue and choices
        ShowDialogue("Hello, traveler! What would you like to do?", 
                     new string[] { "Trade", "Ask about the town", "Leave" });
    }

public void ShowDialogue(string question, string[] responses)
{
    dialogueText.text = question; // Show the NPC question

    ClearPreviousResponses(); // Remove old buttons before adding new ones

    foreach (string response in responses)
    {
        Button newButton = Instantiate(responseButtonPrefab, responseContainer);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = response;
        newButton.onClick.AddListener(() => SelectResponse(response));

        activeButtons.Add(newButton);
    }

    responsePanel.SetActive(true); // Show response options

    // Force layout update to apply spacing immediately
    LayoutRebuilder.ForceRebuildLayoutImmediate(responseContainer.GetComponent<RectTransform>());
}

    void SelectResponse(string response)
    {
        Debug.Log("Player selected: " + response);

        // TODO: Save choice for future use
        PlayerPrefs.SetString("LastChoice", response);
        PlayerPrefs.Save();

        EndDialogue();
    }

    void EndDialogue()
    {
        responsePanel.SetActive(false);
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
