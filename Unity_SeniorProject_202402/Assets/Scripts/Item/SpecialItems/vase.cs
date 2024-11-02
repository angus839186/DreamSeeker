using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vase : InteractableItem
{
    public bool firstInteract;
    public bool TakeKey;
    public GameObject hint;
    public override void Interact()
    {
        if(!firstInteract)
        {
            this.SetDialogue("one", 0);
            firstInteract = true;
        }
        else
        {
            if(TakeKey)
            {
                this.SetDialogue("three", 2);
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
    public void GetKey()
    {
        TakeKey = true;
    }
}
