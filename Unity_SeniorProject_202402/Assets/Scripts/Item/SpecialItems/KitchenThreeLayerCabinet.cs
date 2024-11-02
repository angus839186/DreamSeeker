using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenThreeLayerCabinet : InteractableItem
{
    public override void Interact()
    {
        if (!GameManager.Instance.IsMiniGameCompleted("Sokobanbox"))
        {
            this.SetDialogue("one", 0);
        }
        else
        {
            if(BagManager.instance.GetItemInBag("PhotoFragments2"))
            {
                this.SetDialogue("three", 2);
            }
            else
            {
                if (BagManager.instance.GetItemInBag("cornmeal") ||
                BagManager.instance.GetItemInBag("taibaipowder") ||
                BagManager.instance.GetItemInBag("flour"))
                {
                    this.SetDialogue("one", 1);
                }
                else
                {
                    this.SetDialogue("two", 1);
                }
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
}
