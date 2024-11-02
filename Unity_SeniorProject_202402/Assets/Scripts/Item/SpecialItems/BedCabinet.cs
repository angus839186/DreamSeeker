using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedCabinet : InteractableItem
{
    public bool takekey;

    public Animator anime;
    public override void Interact()
    {
        ToggleBedCabinet();
        if(takekey)
        {
            this.SetDialogue("two", 1);
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
    public void GetKey()
    {
        takekey = true;
    }

    public void ToggleBedCabinet()
    {
        anime.SetTrigger("Toggle");
    }
}
