using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redbox : InteractableItem
{
    public bool opened;

    public Animator anime;
    public override void Interact()
    {
        if (!opened)
        {
            if (BagManager.instance.GetItemInBag("RedboxKey"))
            {
                BagManager.instance.RemoveByManager(BagManager.instance.GetItemInBag("RedboxKey"));
                opened = true;
                ToggleCase();
                this.SetDialogue("two", 1);
            }
            else
            {
                this.SetDialogue("one", 0);
            }
        }
        else
        {
            ToggleCase();
            if (!BagManager.instance.GetItemInBag("BubbleGum"))
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
    public void ToggleCase()
    {
        anime.SetTrigger("Toggle");
    }
}
