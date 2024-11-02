using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue", fileName = "NewDialogue", order = 1)]
public class Dialogues : ScriptableObject
{
    public PartOfDialogue[] _partOfDialogue;
}

[System.Serializable]
public class PartOfDialogue
{
    public DialogueText _dialogues;
}

[System.Serializable]
public class DialogueText
{
    public string[] _dialogueText;
    public Sprite[] _images;
}
