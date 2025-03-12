using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public GameObject responsePanel;
    public Button responseButtonPrefab;
    public Transform responseContainer;

    private List<Button> activeButtons = new List<Button>();
    private List<DialogueStep> currentDialogueSteps;
    private int currentStepIndex = 0; // Track which question is active
    private bool awaitingResponse = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        responsePanel.SetActive(false);
    }

    public void StartDialogue(string npcName, List<DialogueStep> dialogueSteps)
    {
        Time.timeScale = 0f; // Pause game when dialogue starts
        dialogueBox.SetActive(true);
        currentDialogueSteps = dialogueSteps;
        currentStepIndex = 0;

        ShowNextDialogueStep();
    }

    void ShowNextDialogueStep()
    {
        if (currentStepIndex < currentDialogueSteps.Count)
        {
            DialogueStep step = currentDialogueSteps[currentStepIndex];
            dialogueText.text = step.npcDialogue;

            if (step.responseMap.Count > 0) // If there are responses, show them
            {
                ShowResponses(step.responseMap);
            }
            else // No responses, wait for player to press Space to continue
            {
                StartCoroutine(WaitForNextQuestion());
            }
        }
        else
        {
            EndDialogue(); // Properly exit when no more dialogue steps exist
        }
    }

    void ShowResponses(Dictionary<string, string> responseMap)
    {
        awaitingResponse = true;
        responsePanel.SetActive(true);
        ClearPreviousResponses();

        foreach (var response in responseMap)
        {
            Button newButton = Instantiate(responseButtonPrefab, responseContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = response.Key;
            newButton.onClick.AddListener(() => SelectResponse(response.Value));
            activeButtons.Add(newButton);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(responseContainer.GetComponent<RectTransform>());
    }

    void SelectResponse(string nextNpcDialogue)
    {
        if (!awaitingResponse) return;

        awaitingResponse = false;
        responsePanel.SetActive(false);

        if (!string.IsNullOrEmpty(nextNpcDialogue))
        {
            dialogueText.text = nextNpcDialogue; // NPC replies based on player choice
            StartCoroutine(WaitForNextQuestion());
        }
        else
        {
            currentStepIndex++; // Move to the next question
            ShowNextDialogueStep();
        }
    }

    IEnumerator WaitForNextQuestion()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));

        currentStepIndex++;

        if (currentStepIndex >= currentDialogueSteps.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowNextDialogueStep();
        }
    }

    void EndDialogue()
    {
        Time.timeScale = 1f;
        dialogueBox.SetActive(false);
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