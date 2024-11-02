using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soap : InteractableItem
{
    public override void Interact()
    {
        if (BagManager.instance.GetItemInBag("DirtyPhotoFragments2"))
        {
            if (!BagManager.instance.GetItemInBag("Soap"))
            {
                this.SetDialogue("two", 1);
            }
            else
            {
                this.SetDialogue("one", 0);
            }
        }
        else
        {
            this.SetDialogue("one", 0);
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
