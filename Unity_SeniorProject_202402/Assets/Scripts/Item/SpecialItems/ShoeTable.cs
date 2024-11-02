using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeTable : InteractableItem
{
    public bool TakePhoto;
    public override void Interact()
    {
        if (!GameManager.Instance.IsMiniGameCompleted("Sokobanbox"))
        {
            this.SetDialogue("one", 0);
        }
        else
        {
            if (TakePhoto)
            {
                this.SetDialogue("one", 0);
            }
            else
            {
                this.SetDialogue("two", 1);
            }
        }
        var OptionPool = GetOptionPoolByName(_OptionPoolName);
        DialogueManager.instance.StartDialogue(dialogue, _partIndex, OptionPool.InteractionOptionPool);
    }
    public void SetDialogue(string OptionName, int dialogueIndex)
    {
        this._partIndex = dialogueIndex;
        this._OptionPoolName = OptionName;
    }
    public void GetPhoto()
    {
        TakePhoto = true;
    }
}
