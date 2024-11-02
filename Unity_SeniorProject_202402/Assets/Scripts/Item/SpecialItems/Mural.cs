using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mural : InteractableItem
{
    public override void Interact()
    {
        if (!GameManager.Instance.IsMiniGameCompleted("Puzzle"))
        {
            this.SetDialogue("one", 0);
        }
        else
        {
            this.SetDialogue("two", 1);
        }
        var OptionPool = GetOptionPoolByName(_OptionPoolName);
        DialogueManager.instance.StartDialogue(dialogue, _partIndex, OptionPool.InteractionOptionPool);
    }
    public void SetDialogue(string OptionName, int dialogueIndex)
    {
        this._partIndex = dialogueIndex;
        this._OptionPoolName = OptionName;
    }
}
