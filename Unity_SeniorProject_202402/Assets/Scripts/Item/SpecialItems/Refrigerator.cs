using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : InteractableItem
{
    public bool TakePhoto;

    public Animator anime;
    public override void Interact()
    {
        ToggleRefrigerator();
        if(!BagManager.instance.GetItemInBag("pudding"))
        {
            this.SetDialogue("one", 0);
        }
        else
        {
            if(BagManager.instance.GetItemInBag("PhotoFragments7"))
            {
                this.SetDialogue("three", 2);
            }
            else
            {
                if (GameManager.Instance.IsMiniGameCompleted("Piano"))
                {
                    if(TakePhoto)
                    {
                        this.SetDialogue("three", 2);
                    }
                    else
                    {
                        this.SetDialogue("two", 1);
                    }
                }
                else
                {
                    this.SetDialogue("three", 2);
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

    public void GetPhoto()
    {
        TakePhoto = true;
    }
    public void ToggleRefrigerator()
    {
        anime.SetTrigger("Toggle");
    }
}
