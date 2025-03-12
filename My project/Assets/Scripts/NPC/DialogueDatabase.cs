using System.Collections.Generic;
using UnityEngine;

public static class DialogueDatabase
{
    public static DialogueData GetDialogue(string npcName)
    {
        switch (npcName)
        {
            case "Fairy":
                return new DialogueData(
                    "Fairy",
                    new List<DialogueStep>
                    {
                        new DialogueStep(
                            "A new traveler! Have you come to share your story?",
                            new Dictionary<string, string>
                            {
                                { "I'd be happy to! Where should I begin?", "(You share your story to the keenly listening fairy)\nThat was lovely! I do hope you enjoy your time here too!" },
                                { "Maybe another time. I'm just passing through.", "(The fairy nods understandingly)\nIn that case, I hope you enjoy your time here!" },
                                { "(Avoid eye contact and move past)", "(The fairy seems a bit disappointed, but doesn't push you)\nTake care on your adventure here!" }
                            }
                        )
                    }
                );

            case "Shop":
                return new DialogueData(
                    "Shop",
                    new List<DialogueStep>
                    {
                        new DialogueStep(
                            "Hey! I'm Lando, you'll see me around a few times while you're here.",
                            new Dictionary<string, string>()
                        ),
                        new DialogueStep(
                            "Hope we can get to know each other well!",
                            new Dictionary<string, string>()
                        ),
                        new DialogueStep(
                            "Over the past two weeks, how often have you had little interest or pleasure in doing things?",
                            new Dictionary<string, string>
                            {
                                { "Not at all", "" },
                                { "Several days", "" },
                                { "More than half the days", "" },
                                { "Nearly every day", "" }
                            }
                        ),
                        new DialogueStep(
                            "I see. Over the same too weeks, how often have you been feeling down, depressed or hopeless?",
                            new Dictionary<string, string>
                            {
                                { "Not at all", "Thanks for sharing! I hope your time here improves your mood!" },
                                { "Several days", "That’s rough. If you ever need to talk, I’m around!" },
                                { "More than half the days", "That’s a lot to carry. Please take care of yourself!" },
                                { "Nearly every day", "That sounds really tough. You’re not alone, okay?" }
                            }
                        )
                    }
                );

            case "PiggyMan":
                return new DialogueData(
                    "PiggyMan",
                    new List<DialogueStep>
                    {
                        new DialogueStep(
                            "(You come across two people. They seem to be arguing about something.)",
                            new Dictionary<string, string>() // No responses
                        ),
                        new DialogueStep(
                            "Its mine! I found it here, buried under a bunch of stones!",
                            new Dictionary<string, string>() // No responses
                        ),
                        new DialogueStep(
                            "Liar! I pulled it out from behind those trees!",
                            new Dictionary<string, string>() // No responses
                        ),
                        new DialogueStep(
                            "(They turn to you expectantly)\n Who do you believe?",
                            new Dictionary<string, string>
                            {
                                { "How about you guys share it? Looks like you can't carry everything back by yourself anyway.", 
                                "(They calm down, not expecting such an answer)\nOn second thought, he does make sense..." },
                                { "I don't know, you guys will have to figure it out yourselves.", 
                                "Bah!\n(The argument continues, each trying to convince the other they're right)" },
                                {"Well, only one of you can be right, and I think its...\n (You point at one of them)", 
                                "Ha! I told you!\n(The other person looks dejected, but accepts the decision)" }
                            }
                        )
                    }
                );

            default:
                return new DialogueData(npcName, new List<DialogueStep>());
        }
    }
}

// Updated to support multiple Dialogue Steps per NPC
public class DialogueData
{
    public string npcName;
    public List<DialogueStep> dialogueSteps;

    public DialogueData(string name, List<DialogueStep> steps)
    {
        npcName = name;
        dialogueSteps = steps;
    }
}

public class DialogueStep
{
    public string npcDialogue; // NPC's question or statement
    public Dictionary<string, string> responseMap; // Player choice → Next dialogue

    public DialogueStep(string dialogue, Dictionary<string, string> responses)
    {
        npcDialogue = dialogue;
        responseMap = responses;
    }
}
