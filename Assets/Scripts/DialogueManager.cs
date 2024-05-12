using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    Dictionary<int, string[]> dialogue;
    void Awake()
    {
        dialogue = new Dictionary<int, string[]>();
        GenerateDialogue();
    }

    void GenerateDialogue()
    {
        dialogue.Add(0, new string[] {"안녕?", "이곳에 처음 왔구나?"});
    }
}
